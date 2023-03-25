using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public Vector2 Direction = new Vector2(0, 1);
    public Vector2 velocity;
    public GameObject EnemyTrigger;

    public AudioClip ShootSFX;
    public AudioClip TriggerSFX;
    void Start()
    {
        try
        {
            SoundManager.instance.SFXPlay("ShootSFX", ShootSFX);
        }
        catch
        {

        }
    }


    void Update()
    {
        velocity = Direction * speed;
    }

    private void FixedUpdate()
    {
        Vector2 Pos = transform.position;

        Pos += velocity * Time.fixedDeltaTime;

        transform.position = Pos;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            SoundManager.instance.SFXPlay("Trigger", TriggerSFX);

            Instantiate(EnemyTrigger, transform.position, Quaternion.identity);
            DataManager.CurScore += 3;
            Destroy(gameObject);
        }

        if (collision.tag == "Boss")
        {


            Instantiate(EnemyTrigger, transform.position, Quaternion.identity);
            DataManager.CurScore += 5;
            Destroy(gameObject);
        }
    }

}
