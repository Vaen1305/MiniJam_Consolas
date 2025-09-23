using UnityEngine;

public class CollisionSaw : MonoBehaviour
{
    [SerializeField] private GameIntEvent playerDamageEvent;
    [SerializeField] private int damage = 1;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerDamageEvent.Raise(damage);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDamageEvent.Raise(damage);
        }
    }
}
