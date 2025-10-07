using UnityEngine;

public class CameraPlayer : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform player; // El jugador a seguir
    
    [Header("Camera Position")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -7f); // Distancia desde el jugador
    [SerializeField] private float height = 3f; // Altura adicional de la cámara
    
    [Header("Camera Smoothing")]
    [SerializeField] private float smoothSpeed = 0.125f; // Velocidad de suavizado (0-1)
    [SerializeField] private bool useSmoothFollow = true; // Activar/desactivar suavizado
    
    [Header("Look At Settings")]
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0f, 1f, 0f); // Offset del punto de enfoque
    [SerializeField] private bool alwaysLookAtPlayer = true; // Siempre mirar al jugador
    
    [Header("Distance Limits")]
    [SerializeField] private float minDistance = 5f; // Distancia mínima
    [SerializeField] private float maxDistance = 15f; // Distancia máxima
    
    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("No se encontró el jugador. Asigna el player manualmente.");
            }
        }
    }
    
    void LateUpdate()
    {
        if (player == null) return;
        
        FollowPlayer();
    }
    
    void FollowPlayer()
    {
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.y += height;
        
        float distance = Vector3.Distance(new Vector3(player.position.x, 0, player.position.z), 
                                         new Vector3(desiredPosition.x, 0, desiredPosition.z));
        
        if (distance < minDistance || distance > maxDistance)
        {
            Vector3 direction = (desiredPosition - player.position).normalized;
            float clampedDistance = Mathf.Clamp(distance, minDistance, maxDistance);
            desiredPosition = player.position + direction * clampedDistance;
            desiredPosition.y = player.position.y + height;
        }
        
        if (useSmoothFollow)
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
        else
        {
            transform.position = desiredPosition;
        }
        
        if (alwaysLookAtPlayer)
        {
            Vector3 lookAtPosition = player.position + lookAtOffset;
            transform.LookAt(lookAtPosition);
        }
    }
    
    public void SetOffset(Vector3 newOffset)
    {
        offset = newOffset;
    }
    
    public void SetHeight(float newHeight)
    {
        height = newHeight;
    }
    
    void OnDrawGizmosSelected()
    {
        if (player == null) return;
        
        Vector3 desiredPosition = player.position + offset;
        desiredPosition.y += height;
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(desiredPosition, 0.5f);
        
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, player.position + lookAtOffset);
        
        Gizmos.color = Color.green;
        Gizmos.DrawLine(player.position, desiredPosition);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.position, minDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player.position, maxDistance);
    }
}
