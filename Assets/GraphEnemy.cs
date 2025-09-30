using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphEnemy : MonoBehaviour
{
    public GameObject objective;
    public Vector3 speedReference;
    [SerializeField] Rigidbody rb;

    public Vector3 velocity;
    public float smoothTime = 0.8f;
    void Update()
    {
           // transform.position = Vector3.SmoothDamp(transform.position, objective.transform.position, ref speedReference, 0.5f);

    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Node")
        {
            objective = other.gameObject.GetComponent<NodeController>().SelecRandomAdjancent().gameObject;
            Debug.Log("xd");
        }
    }
    void FixedUpdate()
    {
      //  if (objective == null) return;

        Vector3 targetPos = objective.transform.position;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);

        rb.MovePosition(newPos);
    }
}
