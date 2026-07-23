using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour //Call this script whenever there's a need for camera shake
{
    //Singleton
    public static CameraShake Instance;


    //Variables
    //private Camera mainCam;

    void Awake()
    {
        //Initialise the main camera
        //mainCam = GetComponent<Camera>();


        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    //Use an Enumerator to handle the screen shake
    public IEnumerator BeginScreenShake(float intensity, float duration)
    {
        //1) Create a new vector 3 for the camera's staring / original position
        Vector3 originalCamPos = transform.localPosition;
        float elapsedTime = 0f;

        // 2) Use a while loop to shake the screen as long as elapsed time is less than the duration
        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-0.5f, 0.5f) * intensity;
            float offsetY = Random.Range(-0.5f, 0.5f) * intensity;
            //Vector3 camOffSet = new 

            //3) use the offsets above to set the local position of the camera
            transform.localPosition = new Vector3(offsetX, offsetY, originalCamPos.z);

            //4) Increment 'elapsedTime' over time (delta time in this case)
            elapsedTime += Time.deltaTime;

            //5) Wait for one frame before continuing
            yield return null;
            
        }

        //5)Set the camera's local position to the originalCamPos variable
        transform.localPosition = originalCamPos;
    }

    
}
