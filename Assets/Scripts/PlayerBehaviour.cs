using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 100f;
    private bool isShooting;
    private Vector3 bulletOffset = new Vector3(0, 0, 1);


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
    private GameBehaviour gameManager;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehaviour>();
    }

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical") * moveSpeed;
        horizontalInput = Input.GetAxis("Horizontal") * rotateSpeed;

        /*transform.Translate(Vector3.forward * verticalInput * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime);*/

        isJumping |= Input.GetKeyDown(KeyCode.Space);
        isShooting |= Input.GetKeyDown(KeyCode.Mouse0);


    }

    private void FixedUpdate()
    {
        Vector3 rotation = Vector3.up * horizontalInput;
        Quaternion angleRotation = Quaternion.Euler(rotation * Time.fixedDeltaTime);

        rb.MovePosition(transform.position + transform.forward * verticalInput * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * angleRotation);

        

        if (IsGrounded() && isJumping)
        {
            rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        isJumping = false;

        if (isShooting)
        {
            GameObject newBullet = Instantiate(bulletPrefab, this.transform.position + transform.forward, this.transform.rotation);

            Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
            bulletRb.velocity = this.transform.forward * bulletSpeed;
        }

        isShooting = false;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemy")
        {
            gameManager.HP -= 1;
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);

        return grounded;
    }
}
