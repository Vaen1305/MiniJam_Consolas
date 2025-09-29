using UnityEngine;

public class BoomerangManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Quaternion initialRotation;
    private int hitBeforeReturn = 0;
    private bool isThrown = false;

    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
        initialRotation = transform.rotation;
    }

    public void Throw(float charge, Vector2 aim)
    {
        if (charge == 0) return;
        if (isThrown) return;

        if (charge < 0.5f)      hitBeforeReturn = 1;
        else if (charge < 1.2f) hitBeforeReturn = 2;
        else if (charge < 2.5f) hitBeforeReturn = 3;
        else                    hitBeforeReturn = 4;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null) return;
        float launchForce = Mathf.Clamp(charge * 15f, 10f, 30f);
        Debug.Log($"Boomerang lancé, charge = {charge} s with {hitBeforeReturn} hits before return and launch force = {launchForce}");
        Vector3 newAim = new Vector3(aim.x, 0, aim.y);
        rb.AddForce((newAim + Vector3.forward) * launchForce, ForceMode.VelocityChange);
        isThrown = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hitBeforeReturn == 0)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null) return;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = initialRotation;
            hitBeforeReturn = 0;
            isThrown = false;
        }
        else if (other.CompareTag("Sticky"))
        {
            Debug.Log("Boomerang a touché un objet collant");

            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null) return;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.rotation = initialRotation;
            hitBeforeReturn = 0;
        }
        else
        {
            if (hitBeforeReturn == 0) return;
            hitBeforeReturn--;
            if (hitBeforeReturn > 0) return;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb == null) return;
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            directionToPlayer.y = 0;
            float returnForce = 15f;
            rb.linearVelocity = Vector3.zero;
            rb.AddForce(directionToPlayer * returnForce, ForceMode.VelocityChange);
        }
    }
}
