using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossScript : MonoBehaviour
{
    public Slider BossHpSlider;
    public int BossHp;
    public int BossType;
    SpriteRenderer spriteRenderer;
    Color OrigColor;
    public GameObject BossSpawnObj;

    void Start()
    {
        GameManager.instance.enemyManagerObj.SetActive(false);

        BossSpawnObj.SetActive(true);
        Invoke("UnShow", 1.5f);

        switch(BossType)
        {
            case 0:
                BossHp = 300;
                break;
            case 1:
                BossHp = 600;
                break;
            case 2:
                BossHp = 1000;
                break;
        }

        BossHpSlider.maxValue = BossHp;
        BossHpSlider.gameObject.SetActive(true);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        OrigColor = spriteRenderer.material.color;
    }


    void Update()
    {
        GameManager.instance.BossCount = 0;

        if(BossType == 2)
        {
            Vector3 Pos = transform.position;
            Vector3 Target = GameObject.Find("Player").transform.position;

            Vector3 BossPos = Target - Pos;

            float angle = Mathf.Atan2(BossPos.y, BossPos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);

        }

        BossHpSlider.value = BossHp;

        if(BossHp <= 0)
        {
            print("ÆÄ±«");
            BossHpSlider.gameObject.SetActive(false);
            DataManager.CurScore += 500 * (BossType + 1);
            Destroy(gameObject);

            if(BossType == 2)
            {
                SceneManager.LoadScene("GameEnd");
                Destroy(gameObject);
                return;
            }

            GameManager.instance.StageClearTextShow();


        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerShot")
        {
            BossHp -= 1;
            print("Boss HP : " + BossHp);
            StartCoroutine(Flash());
        }
    }

    IEnumerator Flash()
    {
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.material.color = OrigColor;
    }

    void UnShow()
    {
        BossSpawnObj.SetActive(false);
    }
}
