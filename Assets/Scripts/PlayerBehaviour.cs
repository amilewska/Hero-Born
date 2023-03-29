using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]private float moveSpeed = 10f;
    [SerializeField]private float rotateSpeed = 75f;

    private float verticalInput;
    private float horizontalInput;

    private Rigidbody rb;

    [SerializeField] private float jumpVelocity = 5f;
    private bool isJumping;

    [SerializeField] float distanceToGround = 0.1f;
    [SerializeField] private LayerMask groundLayer;
    private CapsuleCollider col;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical") * moveSpeed;
        horizontalInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /*transform.Translate(Vector3.forward * verticalInput * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime);*/

        isJumping |= Input.GetKeyDown(KeyCode.Space);


    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * horizontalInput;
        Quaternion angleRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        rb.MovePosition(transform.position + transform.forward * verticalInput * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * angleRotation);

        if(IsGrounded() && isJumping)
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }


    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
