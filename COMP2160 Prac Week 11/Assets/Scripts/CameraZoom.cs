using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    private Actions playeractions;
    private InputAction input;
    private Camera cam;
    [SerializeField] float scaleRatio = 50f;
    [Range(0, 180)][SerializeField] float minfovRange = 50f;
    [Range(0, 180)][SerializeField] float maxfovRange = 100f;

    [Range(1, 10)][SerializeField] float minOrthoSize = 1f;
    [Range(1, 10)][SerializeField] float maxOrthoSize = 10f;
    void Awake()
    {
        playeractions = new Actions();
        input = playeractions.camera.zoom;
        cam = Camera.main;
    }
    void OnEnable()
    {
        input.Enable();
    }
    void OnDisable()
    {
        input.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        if (maxfovRange < minfovRange)
        { //if the values are reversed
            float temp = maxfovRange;
            maxfovRange = minfovRange;
            minfovRange = temp;
        }
        if (maxOrthoSize < minOrthoSize)
        { //if the values are reversed
            float temp = maxOrthoSize;
            maxOrthoSize = minOrthoSize;
            minOrthoSize = temp;
        }

        float delta = input.ReadValue<float>() / scaleRatio;
        if (delta != 0)
        {
            // Debug.Log(delta);
            if (cam.orthographic)
            {
                if(minOrthoSize <= cam.orthographicSize && maxOrthoSize >= cam.orthographicSize){
                    cam.orthographicSize -= delta;
                }
            }
            else
            {

                cam.fieldOfView -= delta;
            }

        }

    }
}
