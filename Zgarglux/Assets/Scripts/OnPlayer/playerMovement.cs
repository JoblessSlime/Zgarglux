using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller; 

    public float speed = 6f;              
    public float rotationSpeed = 180f;     
    public float jumpHeight = 3f;         
    public float gravity = -9.81f;         
    Vector3 velocity;                    

    bool isGrounded;                     

    public Transform groundCheck;        
    public float groundDistance = 0.4f;    
    public LayerMask groundMask;           

    void Update()
    {
        // Ground check
        

        // Reset Y-velocity if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); 
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 

        float vertical = Input.GetAxisRaw("Vertical"); 
        Vector3 moveDirection = transform.forward * vertical;

        if (moveDirection.magnitude >= 0.1f)
        {
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q)) // Rotate left
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D)) // Rotate right
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
