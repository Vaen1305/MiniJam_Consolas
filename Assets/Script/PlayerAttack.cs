using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject downSlashHitbox;
    [SerializeField] private float pogoBounceForce = 12f;
    private Rigidbody rb;
    private DownSlashHitbox hitboxComponent;
    private bool isDownSlashing = false;

    void Start() 
    { 
        rb = GetComponent<Rigidbody>();
        hitboxComponent = downSlashHitbox.GetComponent<DownSlashHitbox>();
        downSlashHitbox.SetActive(false);
    }
    public void OnDownSlash(InputAction.CallbackContext context)
    {
        if (context.performed && !isDownSlashing)
        {
            StartCoroutine(DoDownSlash());
        }
    }

    /*void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isDownSlashing)
            StartCoroutine(DoDownSlash());
    }*/

    IEnumerator DoDownSlash()
    {
        isDownSlashing = true;
        downSlashHitbox.SetActive(true);
        
        yield return new WaitForSeconds(0.05f);
        
        if (hitboxComponent != null)
            hitboxComponent.ActivateHitbox();
        
        yield return new WaitForSeconds(0.1f);
        
        if (hitboxComponent != null)
            hitboxComponent.DeactivateHitbox();
            
        downSlashHitbox.SetActive(false);
        isDownSlashing = false;
    }

    public void PogoBounce()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * pogoBounceForce, ForceMode.VelocityChange);
    }
}