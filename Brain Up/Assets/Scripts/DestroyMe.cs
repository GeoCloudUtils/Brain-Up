using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    public float delay = 0.1f;
    private void Start()
    {
        Destroy(gameObject, delay);
    }
}
