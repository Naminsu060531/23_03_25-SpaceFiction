using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int EnemyHP;
    public int EnemySpeed;
    public int EnemyShot;
    public int EnemyShotMax;

    public int EnemyType;
    public int EnemyMoveType;
    public int EnemyAttackType;

    public float ShotDelay;
    public float ShotDelayMax;

    SpriteRenderer spriteRenderer;
    Color OrigColor;

    public GameObject Bullet;

    public GameObject[] Item;
    public int ItemDrop;
    public int ItemValue;
    public int ChoiceItemValue;

    public AudioClip AseteroidSfX;

    Player playerScript;

    void Start()
    {
        switch(EnemyType)
        {
            case 0: // Asteroid
                try
                {
                    SoundManager.instance.SFXPlay("Asteroid", AseteroidSfX);
                }
                catch
                {

                }

                if(GameManager.instance.StageValue == 1)
                {
                    EnemyHP = 1;
                    EnemySpeed = 10;
                    EnemyMoveType = 0;
                }
                else if (GameManager.instance.StageValue == 2)
                {
                    EnemyHP = 6;
                    EnemySpeed = 10;
                    EnemyMoveType = 0;
                }
                else if (GameManager.instance.StageValue == 3)
                {
                    EnemyHP = 10;
                    EnemySpeed = 10;
                    EnemyMoveType = 0;
                }


                break;
            case 1:

                if (GameManager.instance.StageValue == 1)
                {
                    EnemyHP = 4;
                    EnemySpeed = 3;
                    EnemyMoveType = 1;
                }
                else if (GameManager.instance.StageValue == 2)
                {
                    EnemyHP = 8;
                    EnemySpeed = 3;
                    EnemyMoveType = 1;
                }
                else if (GameManager.instance.StageValue == 3)
                {
                    EnemyHP = 12;
                    EnemySpeed = 3;
                    EnemyMoveType = 1;
                }

                break;
            case 2:

                if (GameManager.instance.StageValue == 1)
                {
                    EnemyHP = 4;
                    EnemySpeed = 3;
                    EnemyMoveType = 2;
                }
                else if (GameManager.instance.StageValue == 2)
                {
                    EnemyHP = 8;
                    EnemySpeed = 3;
                    EnemyMoveType = 2;
                }
                else if (GameManager.instance.StageValue == 3)
                {
                    EnemyHP = 12;
                    EnemySpeed = 3;
                    EnemyMoveType = 2;
                }

                break;
            case 3:

                if(GameManager.instance.StageValue == 1)
                {
                    EnemyHP = 4;
                    EnemySpeed = 2;
                    EnemyMoveType = 2;
                }
                else if (GameManager.instance.StageValue == 2)
                {
                    EnemyHP = 8;
                    EnemySpeed = 2;
                    EnemyMoveType = 2;
                }
                else if (GameManager.instance.StageValue == 3)
                {
                    EnemyHP = 12;
                    EnemySpeed = 2;
                    EnemyMoveType = 2;
                }

                break;
            case 4: // Turret
                EnemyHP = 100;
                EnemyMoveType = 3;
                break;
            case 5: // Turret2
                EnemyHP = 100;
                EnemyMoveType = 3;
                EnemyAttackType = 1;
                break;

        }

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        OrigColor = spriteRenderer.material.color;
    }

    void Update()
    {
        ShotDelay += Time.deltaTime;

        switch (EnemyMoveType)
        {
            case 0: // Asteroid
                transform.position += Vector3.down * EnemySpeed *Time.deltaTime;
                transform.Rotate(0, 0, 30);
                break;
            case 1:
                Vector3 StartPos = transform.position;

                Vector3 Target = GameObject.Find("Player").transform.position;

                Vector3 Pos = Target - StartPos;

                float angle = Mathf.Atan2(Pos.y, Pos.x) * Mathf.Rad2Deg;

                transform.position += Pos * EnemySpeed * Time.deltaTime;

                transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

                break;
            case 2:
                StartCoroutine(Move2());


                break;
            case 3:
                
                break;
        }

        switch(EnemyAttackType)
        {

            case 1:
                
                if(ShotDelay > ShotDelayMax)
                {

                    Instantiate(Bullet, transform.position, Quaternion.identity);
                    ShotDelay = 0;

                }

                break;

            

        }


        //destroy
        if (EnemyHP <= 0)
        {
            DataManager.CurScore += 10;

            int ItemValueIn = Random.Range(1, 11);

            int Drop = Random.Range(0, 2);

            switch(Drop)
            {

                case 0:
                    if (1 <= ItemValueIn && ItemValueIn < 3)
                    {
                        Instantiate(Item[0], transform.position, Quaternion.identity);
                    }
                    else if (3 <= ItemValueIn && ItemValueIn < 5)
                    {
                        Instantiate(Item[1], transform.position, Quaternion.identity);
                    }
                    else if (5 <= ItemValueIn && ItemValueIn < 7)
                    {
                        Instantiate(Item[2], transform.position, Quaternion.identity);
                    }
                    else if (7 <= ItemValueIn && ItemValueIn <= 10)
                    {
                        Instantiate(Item[3], transform.position, Quaternion.identity);
                    }
                    break;
                case 1:
                    DataManager.CurScore += 30;
                    break;
            }

            Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerScript = collision.GetComponent<Player>();

            if (EnemyType == 0)
            {
                playerScript.HP.value -= 10;
            }
        }

        if (collision.tag == "PlayerShot")
        {
            StartCoroutine(Flash());
            DataManager.CurScore += 5;
            EnemyHP -= 1;
        }

        if(collision.tag == "Boom")
        {
            StartCoroutine(Flash());
            DataManager.CurScore += 10;
            EnemyHP -= 20;
        }
    }

    IEnumerator Move()
    {
        transform.position += Vector3.down * EnemySpeed * Time.deltaTime;

        yield return new WaitForSeconds(2f);

        Vector3 EnemyPos = transform.position;

        float Xrange = 0.5f;

        EnemyPos.x = Xrange * Mathf.Sin(Time.time * EnemySpeed);

        transform.position = EnemyPos;
    }

    IEnumerator Move2()
    {
        transform.position += Vector3.down * EnemySpeed * Time.deltaTime;

        yield return new WaitForSeconds(2f);

        transform.position += Vector3.left * EnemySpeed * Time.deltaTime;

        yield return new WaitForSeconds(2f);

        transform.position += Vector3.right * EnemySpeed * Time.deltaTime;

        yield return new WaitForSeconds(2f);

        transform.position += Vector3.up * EnemySpeed * Time.deltaTime;

        yield return new WaitForSeconds(2f);
    }

    IEnumerator Flash()
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material.color = OrigColor;
    }
}
