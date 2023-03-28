using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    
    [SerializeField]private Vector3 cameraOffset = new Vector3(0f, 3f, -4.7f);
    private Transform target;
    private void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        transform.position = target.TransformPoint(cameraOffset);
        transform.LookAt(target);
    }
}
