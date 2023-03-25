using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] Enemy;
    public Transform[] Pos;
    public int EnemySpawnPosValue;
    public int EnemyValue;
    public float SpawnDelay;
    public float SpawnDelayMax;

    void Start()
    {
           
    }


    void Update()
    {
        SpawnDelay += Time.deltaTime;

        if(GameManager.instance.StageValue == 1)
        {
            SpawnDelayMax = 1.75f;
        }
        if (GameManager.instance.StageValue == 2)
        {
            SpawnDelayMax = 1;
        }
        if (GameManager.instance.StageValue == 3)
        {
            SpawnDelayMax = 0.5f;
        }

        if (SpawnDelay > SpawnDelayMax)
        {
            EnemySpawnPosValue = Random.Range(0, Pos.Length);
            EnemyValue = Random.Range(0, Enemy.Length);

            Instantiate(Enemy[EnemyValue], Pos[EnemySpawnPosValue].position, Quaternion.identity);

            SpawnDelay = 0;
        }
    }
}
