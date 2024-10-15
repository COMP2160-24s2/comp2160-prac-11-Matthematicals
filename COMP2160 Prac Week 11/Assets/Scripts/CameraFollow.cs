using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera cam;
    private MarbleMove marble;
    private Vector3 marblePos;
    void Start()
    {
        cam = Camera.main;
        marble = GetComponent<MarbleMove>();
    }

    // Update is called once per frame
    void Update()
    {
        marblePos = marble.transform.position;
    }
}
