/**
 * A singleton class to allow point-and-click movement of the marble.
 * 
 * It publishes a TargetSelected event which is invoked whenever a new target is selected.
 * 
 * Author: Malcolm Ryan
 * Version: 1.0
 * For Unity Version: 2022.3
 */

using UnityEngine;
using UnityEngine.InputSystem;

// note this has to run earlier than other classes which subscribe to the TargetSelected event
[DefaultExecutionOrder(-100)]
public class UIManager : MonoBehaviour
{
    #region UI Elements
    [SerializeField] private Transform crosshair;
    [SerializeField] private Transform target;
    #endregion

    #region Singleton
    static private UIManager instance;
    static public UIManager Instance
    {
        get { return instance; }
    }
    public Transform Crosshair
    {
        get
        {
            return crosshair;
        }
    }
    private Camera camera;
    #endregion

    #region Actions
    private Actions actions;
    private InputAction mouseAction;
    private InputAction deltaAction;
    private InputAction selectAction;
    [SerializeField] private float scale = 10f;
    [SerializeField] private bool useDelta = false;
    private LayerMask layer;

    #endregion

    #region Events
    public delegate void TargetSelectedEventHandler(Vector3 worldPosition);
    public event TargetSelectedEventHandler TargetSelected;
    #endregion

    #region Init & Destroy
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("There is more than one UIManager in the scene.");
        }

        instance = this;

        actions = new Actions();
        mouseAction = actions.mouse.position;
        deltaAction = actions.mouse.delta;
        selectAction = actions.mouse.select;
        camera = Camera.main;
        // Cursor.visible = false;
        target.gameObject.SetActive(false);
        crosshair.position = Vector3.zero; //init to middle of screen
        layer = LayerMask.GetMask(LayerMask.LayerToName(6));
    }

    void OnEnable()
    {
        actions.mouse.Enable();
    }

    void OnDisable()
    {
        actions.mouse.Disable();
    }
    #endregion Init

    #region Update
    void Update()
    {
        UpdatePos();
        SelectTarget();
    }

    private void MoveCrosshair(Vector3 point)
    {
        crosshair.position = point;
    }


    private void SelectTarget()
    {
        if (selectAction.WasPerformedThisFrame())
        {
            // set the target position and invoke 
            target.gameObject.SetActive(true);
            target.position = crosshair.position;
            TargetSelected?.Invoke(target.position);
        }
    }

    private void UpdatePos()
    {
        if (useDelta == false)
        {
            Vector2 mousePos = mouseAction.ReadValue<Vector2>();
            Ray mouse = camera.ScreenPointToRay(mousePos);
            RaycastHit hit;


            // Debug.Log(LayerMask.LayerToName(layer));
            // Debug.Log(deltaAction.ReadValue<Vector2>());
            if (Physics.Raycast(mouse, out hit, Mathf.Infinity, layer))
            {
                hit.point = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z); // to remove z-fighting          
                MoveCrosshair(hit.point);
            }
        }
        else
        {
            Vector2 delta = deltaAction.ReadValue<Vector2>();
            Vector2 crosshairScreen = camera.WorldToScreenPoint(crosshair.position);
            Ray deltaPos = camera.ScreenPointToRay((crosshairScreen + delta));

            // Debug.DrawRay(test.origin, test.direction);
            // Debug.Log(crosshairScreen + delta);
            RaycastHit hit;
            if (Physics.Raycast(deltaPos, out hit, Mathf.Infinity, layer))
            {
                // Debug.Log(hit.point);
                hit.point = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z); // to remove z-fighting 
                MoveCrosshair(hit.point);
            }
            // crosshair to WorldToScreenPoint, then ray back
            // crosshair.position += delta;
        }



    }

    #endregion Update

}
