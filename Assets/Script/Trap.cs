using UnityEngine;

public class Trap : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 5f;
    
    [Header("Axis Settings")]
    public bool moveInX = false; // Permitir movimiento en X
    public bool moveInY = false; // Permitir movimiento en Y
    public bool moveInZ = true;  // Permitir movimiento en Z
    
    [Header("Points")]
    public Transform pointA; // Primer punto
    public Transform pointB; // Segundo punto
    
    private Rigidbody rb;
    private Transform currentTarget;
    private bool movingToB = true; // true = va hacia B, false = va hacia A
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        rb.freezeRotation = true;
        
        // Establecer el objetivo inicial
        if (pointB != null)
        {
            currentTarget = pointB;
        }
    }

    void Update()
    {
        MoveTrap();
        CheckIfReachedTarget();
    }
    
    void MoveTrap()
    {
        if (currentTarget == null) return;
        
        // Calcular dirección hacia el objetivo
        Vector3 direction = (currentTarget.position - transform.position).normalized;
        
        // Aplicar restricciones de ejes
        float velocityX = moveInX ? direction.x * speed : rb.linearVelocity.x;
        float velocityY = moveInY ? direction.y * speed : rb.linearVelocity.y;
        float velocityZ = moveInZ ? direction.z * speed : rb.linearVelocity.z;
        
        rb.linearVelocity = new Vector3(velocityX, velocityY, velocityZ);
    }
    
    void CheckIfReachedTarget()
    {
        if (currentTarget == null) return;
        
        // Calcular distancia considerando solo los ejes activos
        Vector3 currentPos = transform.position;
        Vector3 targetPos = currentTarget.position;
        
        float distance = 0f;
        
        if (moveInX)
            distance += Mathf.Abs(currentPos.x - targetPos.x);
        if (moveInY)
            distance += Mathf.Abs(currentPos.y - targetPos.y);
        if (moveInZ)
            distance += Mathf.Abs(currentPos.z - targetPos.z);
        
        if (distance <= 0.2f)
        {
            // Cambiar de objetivo
            SwitchTarget();
        }
    }
    
    void SwitchTarget()
    {
        if (movingToB)
        {
            // Estaba yendo a B, ahora va a A
            currentTarget = pointA;
            movingToB = false;
            Debug.Log("Cambiando objetivo a Punto A");
        }
        else
        {
            // Estaba yendo a A, ahora va a B
            currentTarget = pointB;
            movingToB = true;
            Debug.Log("Cambiando objetivo a Punto B");
        }
    }
    
    void OnDrawGizmosSelected()
    {
        // Dibujar los puntos
        if (pointA != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(pointA.position, 0.3f);
            Gizmos.DrawWireCube(pointA.position, Vector3.one * 0.2f);
        }
        
        if (pointB != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(pointB.position, 0.3f);
            Gizmos.DrawWireCube(pointB.position, Vector3.one * 0.2f);
        }
        
        // Dibujar línea entre los puntos
        if (pointA != null && pointB != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(pointA.position, pointB.position);
        }
        
        // Dibujar línea hacia el objetivo actual
        if (currentTarget != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, currentTarget.position);
        }
        
        // Mostrar información de ejes activos
        if (Application.isPlaying)
        {
            string axisInfo = "Ejes activos: ";
            if (moveInX) axisInfo += "X ";
            if (moveInY) axisInfo += "Y ";
            if (moveInZ) axisInfo += "Z ";
            
            Gizmos.color = Color.white;
            // Nota: Para mostrar texto necesitarías usar Handles.Label en lugar de Gizmos
        }
    }
}