using UnityEngine;

public class RotateTramp : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float rotationSpeed = 90f; // Grados por segundo
    public Vector3 rotationAxis = Vector3.up; // Eje de rotación (Y por defecto)
    
    [Header("Direction Settings")]
    public bool clockwise = true; // Sentido de rotación inicial
    public bool useDirectionTimer = false; // Activar/desactivar el timer
    public float directionChangeTime = 3f; // Tiempo para cambiar dirección
    
    [Header("Optional Settings")]
    public bool startRandomRotation = false; // Empezar con rotación aleatoria
    
    private float timer = 0f;
    
    void Start()
    {
        // Opcional: empezar con una rotación aleatoria
        if (startRandomRotation)
        {
            transform.rotation = Random.rotation;
        }
        
        // Inicializar el timer
        timer = directionChangeTime;
    }

    void Update()
    {
        if (useDirectionTimer)
        {
            UpdateDirectionTimer();
        }
        
        RotateTrap();
    }
    
    void UpdateDirectionTimer()
    {
        timer -= Time.deltaTime;
        
        if (timer <= 0f)
        {
            clockwise = !clockwise;
            
            timer = directionChangeTime;
            
            Debug.Log("Dirección cambiada a: " + (clockwise ? "Clockwise" : "Counter-clockwise"));
        }
    }
    
    void RotateTrap()
    {
        float direction = clockwise ? 1f : -1f;
        
        transform.Rotate(rotationAxis * rotationSpeed * direction * Time.deltaTime);
    }
}