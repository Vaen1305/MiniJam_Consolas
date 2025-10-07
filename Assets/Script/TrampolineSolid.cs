using UnityEngine;

public class TrampolineSolid : MonoBehaviour
{
    public float bounceForce = 20f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            
            if (playerRigidbody != null)
            {
                Vector3 velocity = playerRigidbody.velocity;
                velocity.y = 0;
                playerRigidbody.velocity = velocity;
                
                playerRigidbody.AddForce(Vector3.up * bounceForce, ForceMode.Impulse);
            }
        }
    }
}