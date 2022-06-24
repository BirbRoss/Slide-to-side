using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverment : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5.0f;
    public float jumpHeight = 2.0f;
    public float jumpSpeed = -5.0f;
    Vector3 velocity;
    CharacterController controller;

    [Header("Ground check")]
    [SerializeReference] bool grounded = true;
    public Vector3 groundOffset = new Vector3(0, -0.5f, 0);
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float gravity = -9.81f;

    // Start is called before the first frame update
    void Start()
    {
        groundCheck();
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (!grounded && velocity.y < 0)
        {
            groundCheck();
        }
        else if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * jumpSpeed * gravity);
            grounded = false;
        }

        velocity.y += gravity * Time.deltaTime;

        //Checks if grounded and resets velocity or applies gravity
        if (grounded)
        {
            velocity.y = 0;
            groundCheck();
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void groundCheck()
    {
        grounded = Physics.CheckBox(transform.position + groundOffset, new Vector3(0.5f, groundDistance, 0.5f), transform.rotation, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + groundOffset, new Vector3(0.5f*2, groundDistance*2, 0.5f*2));
    }
}
