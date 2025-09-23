using UnityEngine;

public class SawMovement : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float rotateSpeed = 360f;

    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.05f)
            target = (target == pointA.position) ? pointB.position : pointA.position;

        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime, Space.World);
    }
}
