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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical") * moveSpeed;
        horizontalInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /*transform.Translate(Vector3.forward * verticalInput * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime);*/


    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * horizontalInput;
        Quaternion angleRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        rb.MovePosition(transform.position + transform.forward * verticalInput * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * angleRotation);
    }
}
