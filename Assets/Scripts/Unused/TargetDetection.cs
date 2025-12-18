using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Profiling;
using UnityEngine;


//-------------------------------------------------------------------------------------------------Original code for target detection (Moved to moveinput script instead)//-------------------------------------------------------------------------------------------------
// public class TargetDetection : MonoBehaviour
// {
//     private MouseInput _mouseInput; //Reference to the MouseInput script

//     // Start is called before the first frame update
//     void Start()
//     {
//         _mouseInput = FindObjectOfType<MouseInput>(); //Find the Game Objects that has the mouse input script attached to it
//         // Destroy(_mouseInput); Debugging purposes
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         //Fire a raycast that will detect the targets by tag
//         RaycastHit2D hit = Physics2D.Raycast(new Vector2(mouseWorldPos.x, mouseWorldPos.y), Vector2.zero, 0);
//         Debug.DrawRay(MouseInput.mouseWorldPos, Vector2.up * 0.1f, Color.red, 10f);

//          if (hit && gameObject.tag == "BasicTarget")
//          {
//              Debug.Log("Basic Target Detected");
//          }
//     }
// }
