using UnityEngine;

public class BoomerangController : MonoBehaviour
{
    [SerializeField] private BoomerangManager boomerang;
    [SerializeField] private Camera playerCamera; // Add reference to camera
    public BoomerangControls controls; // généré par l'Input System
    float holdTime;
    Vector2 mousePosition;

    void Awake()
    {
        controls = new BoomerangControls();

        controls.Boomerang.Throw.started += ctx => holdTime = Time.time;
        controls.Boomerang.Throw.performed += ctx => LaunchBoomerang(Time.time - holdTime);
        
        // If no camera is assigned, use the main camera
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    void OnEnable() => controls.Enable();
    void OnDisable() => controls.Disable();

    void Update()
    {
        mousePosition = controls.Boomerang.Aim.ReadValue<Vector2>();
        // utiliser mousePosition pour tourner la visée / caméra
    }

    void LaunchBoomerang(float charge)
    {
        if (charge == 0) return;

        // Convert mouse screen position to world direction
        Vector3 mouseWorldPos = playerCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, playerCamera.transform.position.y));
        Vector3 aimDirection = (mouseWorldPos - boomerang.transform.position).normalized;
        
        // Convert to Vector2 for the boomerang throw method (removing Y component)
        Vector2 aim = new Vector2(aimDirection.x, aimDirection.z);

        boomerang.Throw(charge, aim.normalized);
    }
}