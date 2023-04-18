using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable<T> : MonoBehaviour where T : MonoBehaviour
{
    public float onScreenDelay = 3f;

    private void Start()
    {
        Destroy(this.gameObject, onScreenDelay);
    }
}
