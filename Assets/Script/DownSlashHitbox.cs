using UnityEngine;

public class DownSlashHitbox : MonoBehaviour
{
    private PlayerAttack player;
    private PlayerHealth playerHealth;
    private Collider hitboxCollider;

    void Start() 
    { 
        player = GetComponentInParent<PlayerAttack>();
        playerHealth = GetComponentInParent<PlayerHealth>();
        hitboxCollider = GetComponent<Collider>();
        hitboxCollider.enabled = false;
    }

    public void ActivateHitbox()
    {
        hitboxCollider.enabled = true;
    }

    public void DeactivateHitbox()
    {
        hitboxCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pogoable"))
        {
            // PRIMERO notificar al sistema de salud para activar invulnerabilidad
            if (playerHealth != null)
            {
                playerHealth.OnPogoPerformed();
            }
            
            // LUEGO hacer el bounce
            player.PogoBounce();
        }
    }
}