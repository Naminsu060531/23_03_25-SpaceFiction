using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float ShotDelayMax;
    public float ShotDelay;
    public GameObject Bullet;
    public AudioClip Shoot;
    
    void Update()
    {
        ShotDelay += Time.deltaTime;

        if (ShotDelay > ShotDelayMax)
        {
            ShotDelay = 0;
            SoundManager.instance.SFXPlay("ss", Shoot);
            Instantiate(Bullet, transform.position, Quaternion.identity);
        }

    }
}
