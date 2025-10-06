using UnityEngine;
using UnityEngine.Pool;

public class PortalManager : MonoBehaviour
{
    [SerializeField] private GameObject linkedPortal;
    private Transform elementTransform;
    private bool isElementOverlapping = false;

    void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void Update()
    {
        if (isElementOverlapping && elementTransform != null && linkedPortal != null)
        {
            linkedPortal.GetComponent<PortalManager>()?.DeactivePortalForSeconds(0.5f);
            Vector3 localPosition = transform.InverseTransformPoint(elementTransform.position);
            Vector3 newWorldPosition = linkedPortal.transform.TransformPoint(localPosition);
            elementTransform.position = newWorldPosition;

            Quaternion localRotation = Quaternion.Inverse(transform.rotation) * elementTransform.rotation;
            Quaternion newWorldRotation = linkedPortal.transform.rotation * localRotation;
            elementTransform.rotation = newWorldRotation;

            isElementOverlapping = false;
            elementTransform = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Boomerang"))
        {
            Debug.Log($"Element {other.name} entered portal {gameObject.name}");
            isElementOverlapping = true;
            elementTransform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Boomerang"))
        {
            Debug.Log($"Element {other.name} exited portal {gameObject.name}");
            isElementOverlapping = false;
            elementTransform = null;
        }
    }

    public void DeactivePortalForSeconds(float seconds)
    {
        StartCoroutine(DeactivatePortalTemporarily(gameObject, seconds));
    }

    private System.Collections.IEnumerator DeactivatePortalTemporarily(GameObject portal, float seconds)
    {
        Collider portalCollider = portal.GetComponent<Collider>();
        if (portalCollider != null)
        {
            portalCollider.enabled = false;
            yield return new WaitForSeconds(seconds);
            portalCollider.enabled = true;
        }
    }
}
