using UnityEngine;

public class PlayTarget : MonoBehaviour
{
    [SerializeField] private Transform pointOne;
    [SerializeField] private Transform pointTwo;
    private Transform currentPoint;

    //Variable
    [SerializeField] private float moveSpeed = 5;

    void Start()
    {
        currentPoint = pointOne;
        Debug.Log(currentPoint);
    }

    void Update()
    {
        //Move targets towards point one and two
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.position, moveSpeed * Time.deltaTime);

        if (transform.position == currentPoint.position)
        {
            if (currentPoint == pointOne)
            {
                currentPoint = pointTwo;
            }
            else
            {
                currentPoint = pointOne;
            }
            
        }

    }
}
