using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 direction;
    float _speed = 4;
    [SerializeField] Rigidbody myRBD;
    [SerializeField] LayerMask layerName;
    [SerializeField] float jumpForce = 3;
    float jumpCount = 2;
    float currentJump = 0;
    [SerializeField] bool canJump = false;
    [SerializeField] float lineSize = 0.5f;
    [SerializeField] Animator playerAnimator;
    void Start()
    {
        canJump = true;
    }
    public void OnMovement(InputAction.CallbackContext move)
    {
        direction = move.ReadValue<Vector2>();
        playerAnimator.SetBool("isWalking", true);
        if (move.canceled)
        {
            playerAnimator.SetBool("isWalking", false);
        }
    }
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(direction.x, 0f, direction.y) * _speed;
        myRBD.linearVelocity = new Vector3(move.x, myRBD.linearVelocity.y, move.z);
        CheckGround();
    }
    public void OnJump(InputAction.CallbackContext jump)
    {
        if (jump.performed)
        {
            if(currentJump < jumpCount)
            {
                myRBD.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnimator.SetBool("isJumping", true);
                currentJump++;
            }   
        }
        else if (currentJump >= jumpCount)
        {
            canJump = false;
        }

        if (jump.canceled)
        {
            playerAnimator.SetBool("isJumping", false);
        }
        Debug.Log("a: " + canJump + "b: " + currentJump);
    }
    public void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, lineSize, layerName))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);

            
                // canJump = true;
                
         
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            currentJump = 0;
        }
    }

}
