using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    private MarbleMove marble;
    private Transform crosshair;
    void Start()
    {
        cam = Camera.main;
        crosshair = UIManager.Instance.Crosshair;
        marble = FindObjectOfType<MarbleMove>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 midpoint = (marble.transform.position+crosshair.position)*0.5f;
        transform.position = midpoint;
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2);
        // Debug.Log(Screen.height/4);
        // Rect rect = new Rect(Screen.height/4,Screen.width/4,Screen.height/2,Screen.width/2);
        // Debug.Log(rect.center);
        // Gizmos.DrawCube(rect.center,Vector3.one * 100);
    }

    //create rect 
    
}
