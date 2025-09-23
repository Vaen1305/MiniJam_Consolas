using UnityEngine;

public class DownSlashHitbox : MonoBehaviour
{
    private PlayerAttack player;
    private Collider hitboxCollider;

    void Start() 
    { 
        player = GetComponentInParent<PlayerAttack>();
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
            player.PogoBounce();
        }
    }
}