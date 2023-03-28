using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    //1
    [SerializeField]private float moveSpeed = 10f;
    [SerializeField]private float rotateSpeed = 75f;

    //2
    private float verticalInput;
    private float horizontalInput;

    private void Update()
    {
        verticalInput = Input.GetAxis("Vertical") * moveSpeed;
        horizontalInput = Input.GetAxis("Horizontal") * moveSpeed;

        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime);
        transform.Rotate(Vector3.up * horizontalInput * Time.deltaTime);
    }
}
