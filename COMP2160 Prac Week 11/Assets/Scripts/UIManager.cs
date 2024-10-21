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
    private Camera camera;
    #endregion

    #region Actions
    private Actions actions;
    private InputAction mouseAction;
    private InputAction deltaAction;
    private InputAction selectAction;

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
        SelectTarget();
    }

    private void MoveCrosshair(Vector3 point)
    {
        // Vector2 mousePos = mouseAction.ReadValue<Vector2>();
        // // Vector3 t = new Vector3(mousePos.x, mousePos.y, 10);
        // Ray mouse = camera.ScreenPointToRay(mousePos);
        // Debug.Log(x + " " + y);
        // crosshair.position = point;
        Vector3 delta = new Vector3(deltaAction.ReadValue<Vector2>().x, deltaAction.ReadValue<Vector2>().y, 0f);
        Debug.Log(delta/10);
        crosshair.position += delta;
        // Debug.DrawRay(mouse.origin, mouse.direction, Color.red);
        // Debug.Log(camera.ScreenToWorldPoint(t));
        // crosshair.position = camera.ScreenToWorldPoint(t);


    }
    private void MoveCrosshairDelta(Vector3 point)
    {
        // Vector2 mousePos = mouseAction.ReadValue<Vector2>();
        // // Vector3 t = new Vector3(mousePos.x, mousePos.y, 10);
        // Ray mouse = camera.ScreenPointToRay(mousePos);
        // Debug.Log(x + " " + y);
        Debug.Log(point);
        crosshair.Translate(point * Time.fixedDeltaTime);
        // Debug.DrawRay(mouse.origin, mouse.direction, Color.red);
        // Debug.Log(camera.ScreenToWorldPoint(t));
        // crosshair.position = camera.ScreenToWorldPoint(t);


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

    private void FixedUpdate()
    {
        MoveCrosshair(Vector3.zero);

        // Vector3 mousePos = deltaAction.ReadValue<Vector2>();
        // Ray ray = camera.ScreenPointToRay(mousePos);
        // crosshair.Translate(mousePos*Time.deltaTime);
        // crosshair.Translate(mousePos * mousePos.magnitude);
        
        /*
        // MoveCrosshairDelta(Vector3.up);
        Vector2 mousePos = mouseAction.ReadValue<Vector2>();
        Ray mouse = camera.ScreenPointToRay(mousePos);
        RaycastHit hit;
        LayerMask layer = LayerMask.GetMask(LayerMask.LayerToName(6));

        // Debug.Log(LayerMask.LayerToName(layer));
        // Debug.Log(deltaAction.ReadValue<Vector2>());
        if (Physics.Raycast(mouse, out hit, Mathf.Infinity, layer))
        {
            // if (hit.collider.gameObject.layer == 6)
            // {
            hit.point = new Vector3(hit.point.x, hit.point.y + 0.01f, hit.point.z); // to remove z-fighting
            
            MoveCrosshair(hit.point);
            // }

        }

        */
    }

    #endregion Update

}
