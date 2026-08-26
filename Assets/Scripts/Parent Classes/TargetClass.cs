using UnityEngine;
using System.Collections;


public class TargetClass : MonoBehaviour //Parent class that all the target scripts will inherit from
{
    #region References
    [Header("References")]
    protected Transform[] lerp_Points; //Array to store the lerpPoints' transform (the lerp points are empty game Objects) - ACCESS MODIFIER: Protected (This will give the derived class access to this variable)
    private Vector3 initialSpawnPoint; //Private vector 3 storing the initial spawn position of the target from the spawner
    [SerializeField] private Transform offScreenPoint;
    [SerializeField] private Transform offScreenMidPoint;
    private int currentPointIndex = 0; //Variable that will be used to store the current point the target is moving to
    #endregion

    #region Movement Variables
    //Movement variables
    [Header("Movement Speed")]
    [SerializeField] private float moveSpeed; //To control the speed of the targets
    public float MoveSpeed
    {
        set { moveSpeed = value; }
    }
    #endregion

    #region Variables
    //General variables
    private GameObject prefabTarget;
    private float targetTimer = 10f; //How long targets are able to stay on screen for before returning to the pool
    private bool isMovingOffScreen = false;
    private Coroutine returnCoroutine;

    #endregion

    private void OnEnable()
    {
        moveSpeed = PoolManager.Instance.CurrentMoveSpeed;
        isMovingOffScreen = false; //Reset the bool
    }

    void Start()
    {
        //Grab a reference to the targets' initial spawn position on start 
        initialSpawnPoint = transform.position;

        //Debug.Log(gameObject.transform.position); //Used for debugging to check the targets' position (--18/5/26 Commented this debug out to debug other bugs--)

        //moveSpeed = Random.Range(10, 15); //3/8/26: This will go unused now to make way for incremental speed boosts
    }

    void Update()
    {
        if (isMovingOffScreen) return; //Check if bool is true, and if not, execute regular movement below

        if (currentPointIndex < lerp_Points.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, lerp_Points[currentPointIndex].position, moveSpeed * Time.deltaTime); //This current moves the target to point 1

            if (Vector3.Distance(transform.position, lerp_Points[currentPointIndex].position) < 0.5f)
            {
                currentPointIndex = Random.Range(0, lerp_Points.Length);
            }
        }
    }

    public void initialisePoints(Transform[] points) //As gameObjects can not be assigned to a prefab in the inspector, I will need to assign the lerp points to the targets during runtime
    { //So this method is used to Initialise the lerp points by taking in an array of the lerp points' transform

        lerp_Points = points; //Assigning the lerp points declared above to the points taken in by this method

        if (currentPointIndex < lerp_Points.Length)
        {
            currentPointIndex = Random.Range(0, lerp_Points.Length);
        }
    }

    public virtual void OnHit() //Child classes will override this method
    {
        Debug.Log("Target hit");
        PoolManager.Instance.IncreaseTargetMoveSpeed();
    }

    //Methods to start and stop the return object coroutine
    public void StartCoroutine() //Can be called in the pool manager
    {
        returnCoroutine = StartCoroutine(ReturnObjectAfterTime());
    }

    public void StopCoroutine() //Can also be called in the pool manager
    {
        if (returnCoroutine != null) //Checks if the coroutine is currently running
        {
            StopCoroutine(returnCoroutine); // Stops the coroutine 
            returnCoroutine = null; //Sets the return coroutine to null
        }
    }

    #region Return object Coroutine
    //6/8/26: Moved from the Pool manager to this script, as it has no relevant to the behaviour of that script
    public IEnumerator ReturnObjectAfterTime() //This will return targets to the pool after a certain amount of time. Here to prevent players from just waiting on "positive targets" the whole game
    {
        yield return new WaitForSeconds(targetTimer); //Wait for 10 seconds 

        isMovingOffScreen = true;

        //Disable the box collider
        gameObject.GetComponent<BoxCollider2D>().enabled = false;


        //Sprite Rendered to change the alpha channel
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(0.5f, 0.5f, 0.5f, 0.5f); //Sets the targets' colour to gray and alpha channel to 50%
    

        //Move the target to the middle point
        while (Vector3.Distance(transform.position, offScreenMidPoint.position) > 0.1f)
        {
            Debug.Log("Move targets off screen");
            transform.position = Vector3.MoveTowards(transform.position, offScreenMidPoint.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        //Afterwards move the target to the off screen position, where it will then return to the pool
        while (Vector3.Distance(transform.position, offScreenPoint.position) > 0.1f)
        {
            Debug.Log("Move targets off screen");
            transform.position = Vector3.MoveTowards(transform.position, offScreenPoint.position, moveSpeed * Time.deltaTime);

            yield return null;
        }

        //Re-enable the box collider
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        sr.color = Color.white;

        //Return the target to the object pool
        PoolManager.Instance.ReturnPooledObject(gameObject);
        //Debug.Log(gameObject + " Returned to the pool");
    }
    #endregion
}
