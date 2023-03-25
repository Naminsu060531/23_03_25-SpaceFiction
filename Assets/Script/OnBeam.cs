using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBeam : MonoBehaviour
{
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
