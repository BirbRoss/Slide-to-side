using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverment : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5.0f;
    public float jumpHeight = -5.0f;
    Rigidbody rb;

    [Header("Ground check")]
    [SerializeField] bool grounded = true;
    [SerializeField] bool safeGround = true;
    public Vector3 groundOffset = new Vector3(0, -0.5f, 0);
    public Vector3 groundDistance = new Vector3(0.5f, 0.1f, 0.5f);
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public Vector3 lastCoords;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundCheck();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        transform.position += ((transform.right * x + transform.forward * z) * speed) * Time.deltaTime;
        

        if (!grounded)
        {
            groundCheck();
        }
        else if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            grounded = false;
        }

        //Checks if grounded and resets velocity or applies gravity
        if (grounded)
        {
            groundCheck();

            if (safeGround)
            {
                lastCoords = transform.position;
            }
        }
    }

    void groundCheck()
    {
        grounded = Physics.CheckBox(transform.position + groundOffset, groundDistance, transform.rotation, groundMask);
        safeGround = Physics.CheckBox(transform.position + groundOffset, new Vector3(0.4f, 0.1f, 0.4f), transform.rotation, groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + groundOffset, groundDistance*2);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + groundOffset, new Vector3(0.4f, 0.1f, 0.4f) * 2);
    }
}
