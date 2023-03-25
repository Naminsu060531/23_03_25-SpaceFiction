using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float downSpeed;
    public Vector3 StartPos;

    private void Start()
    {
        StartPos = transform.position;
        print("시작 위치 : " + StartPos.y); // 36.3 //-234.6
    }

    void Update()
    {
        transform.position += Vector3.down * downSpeed * Time.deltaTime;

        if (transform.position.y < -234.6)
        {
            transform.position = StartPos;
            print("돌아감");
        }
    }
}
