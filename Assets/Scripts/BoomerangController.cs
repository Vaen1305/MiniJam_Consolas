using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    [SerializeField] private BoomerangManager boomerang;
    [SerializeField] private GameObject player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject prefab;
    public BoomerangControls controls;
    float holdTime;
    Vector2 mousePosition;

    void Awake()
    {
        controls = new BoomerangControls();

        controls.Boomerang.Throw.started += ctx => holdTime = Time.time;
        controls.Boomerang.Throw.performed += ctx => LaunchBoomerang(Time.time - holdTime);

        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        mousePosition = controls.Boomerang.Aim.ReadValue<Vector2>();
    }

    void LaunchBoomerang(float charge)
    {
        if (charge == 0) return;

        Ray cameraRay = playerCamera.ScreenPointToRay(mousePosition);
        Plane playerHeightPlane = new Plane(Vector3.up, new Vector3(0, player.transform.position.y, 0));
        
        if (playerHeightPlane.Raycast(cameraRay, out float distance))
        {
            Vector3 worldPosition = cameraRay.GetPoint(distance);
            Vector3 direction3D = worldPosition - player.transform.position;
            Vector2 direction2D = new Vector2(direction3D.x, direction3D.z).normalized;
            
            boomerang.Throw(charge, direction2D);
        }
    }
}