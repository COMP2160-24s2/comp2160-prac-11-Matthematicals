using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    private MarbleMove marble;
    void Start()
    {
        cam = Camera.main;
        marble = FindObjectOfType<MarbleMove>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = marble.transform.position;
    }

    void OnDrawGizmo(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10);
    }
}
