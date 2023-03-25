using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down : MonoBehaviour
{

    void Update()
    {
        transform.position += Vector3.down * 3 * Time.deltaTime;
    }
}
