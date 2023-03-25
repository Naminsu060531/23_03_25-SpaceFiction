using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEnemyBullet : MonoBehaviour
{
    public Vector3 TargetPos;

    public float Speed = 5f;

    void Start()
    {
        
    }


    void Update()
    {
        TargetPos = GameObject.FindWithTag("Enemy").transform.position;

        if(TargetPos == null)
        {
            TargetPos = GameObject.FindWithTag("Enemy").transform.position;
        }

        Vector3 StartPos = transform.position;

        Vector3 Pos = TargetPos - StartPos;

        transform.position += Pos * Speed * Time.deltaTime;
    }
}
