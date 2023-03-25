using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public int Num;
    public float ShotDelayMax;
    public float ShotDelay;
    public float Speed;
    public GameObject Bullet;
    [SerializeField] public float radius = 5;

    Vector2 StartPos;


    void Update()
    {
        StartPos = transform.position;
        ShotDelay += Time.deltaTime;

        if(ShotDelay > ShotDelayMax)
        {
            WaveAction(Num);
        }
    
    }

    void WaveAction(int Num)
    {
        float angleStep = 360 / Num;
        float angle = 0;

        for (int i = 0; i < Num; i++)
        {

            float waveX = StartPos.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float waveY = StartPos.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 WaveVec = new Vector2(waveX, waveY);
            Vector2 WaveMove = (WaveVec - StartPos).normalized * Speed;

            var wave = Instantiate(Bullet, StartPos, Quaternion.identity);
            wave.GetComponent<Rigidbody2D>().velocity = new Vector2(WaveMove.x, WaveMove.y);

            angle += angleStep;
        }

        ShotDelay = 0;
    }


}
