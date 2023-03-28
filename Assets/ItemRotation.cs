using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    public int rotationSpeed = 100;
    private Transform itemTranform;

    private void Start()
    {
        itemTranform = this.GetComponent<Transform>();
    }

    private void Update()
    {
        itemTranform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
