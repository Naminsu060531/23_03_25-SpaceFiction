using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance = null;

    public float MoveSpeed;
    public float ShotDelayMax;
    public float ShotDelay;

    public Slider HP;
    public Slider Engine;

    SpriteRenderer spriteRenderer;
    Color OrigColor;

    Guns[] gun;

    public Slider Skill1Slider;
    public Slider Skill2Slider;
    public float Skill1Delay;
    public float Skill1DelayMax;
    public float Skill2Delay;
    public float Skill2DelayMax;
    public int Skill1Use;
    public int SKill2Use;
    public int Skill1UseMax;
    public int Skill2UseMax;
    public Text Skill1UseText;
    public Text Skill2UseText;
    public GameObject Skill1Icon;
    public GameObject Skill2Icon;

    public GameObject Boom;

    public static int PlayerLevel = 1;

    public GameObject PlayerTirgger;

    public GameObject PlayerLevel4Turret;

    public GameObject PlayerLevelObj;
    public Text PlayerLevelText;

    public AudioClip NoColSFX;
    public AudioClip NoUseSFX;
    public GameObject NoUseObj;
    public GameObject NoColObj;

    public AudioClip BoomSFX;
    public AudioClip Skill1SFX;

    public GameObject TurretLevel3_1, TurretLevel3_2, TurretLevel4_1, TurretLevel4_2;

    public GameObject PlayerHPLowObj;

    public GameObject PausePanel;
    public int PauseCount;

    public Text PauseScoreText;
    public Text PauseTimeText;


    private void Awake()
    {
        PlayerLevel = 1;
        GameManager.instance.StageValue = 1;
        DataManager.CurScore = 0;
        DataManager.CurTime = 0;

        TurretLevel3_1.SetActive(false);
        TurretLevel3_2.SetActive(false);
        TurretLevel4_1.SetActive(false);
        TurretLevel4_2.SetActive(false);

        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        HP.value = HP.maxValue;
        Engine.value = Engine.maxValue;
        InvokeRepeating("MinusEngine", 0f, 3f);

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        OrigColor = spriteRenderer.material.color;

        gun = transform.GetComponentsInChildren<Guns>();

    }


    void Update()
    {
        if(HP.value <= 10)
        {
            PlayerHPLowObj.SetActive(true);
        }
        else
        {
            PlayerHPLowObj.SetActive(false);
        }

        ShotDelay += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            if(PauseCount >= 1)
            {
                PausePanel.SetActive(false);
                PauseCount = 0;
                Time.timeScale = 1;
                return;
            }

            PausePanel.SetActive(true);
            PauseCount += 1;
            Time.timeScale = 0;
        }

        PauseScoreText.text = DataManager.CurScore.ToString();
        PauseTimeText.text = DataManager.CurTime.ToString();


        //움직이기
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 Pos = transform.position;
        Vector3 NextPos = new Vector3(h, v, 0) * MoveSpeed * Time.deltaTime;

        transform.position = Pos + NextPos;

        //움직임 제한
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -10.02f, 10.02f),
            Mathf.Clamp(transform.position.y, -5.42f, 5.42f)
            );

        //공격
        if(Input.GetKey(KeyCode.Space))
        {
            if(ShotDelay > ShotDelayMax)
            {
                foreach(Guns Gun in gun)
                {
                    Gun.Shoot();
                }

                ShotDelay = 0;
            }

        }

        //스킬
        Skill1Delay += Time.deltaTime;
        Skill2Delay += Time.deltaTime;

        Skill1Slider.value = Skill1Delay;
        Skill2Slider.value = Skill2Delay;

        if(GameManager.instance.StageValue == 1)
        {
            Skill1UseMax = 10;
            Skill2UseMax = 7;
        }
        else if (GameManager.instance.StageValue == 2)
        {
            Skill1UseMax = 7;
            Skill2UseMax = 4;
        }
        else if (GameManager.instance.StageValue == 3)
        {
            Skill1UseMax = 3;
            Skill2UseMax = 2;
        }

        if(Skill1Use <= 0)
        {
            Skill1UseMax = 0;
        }
        if (SKill2Use <= 0)
        {
            Skill2UseMax = 0;
        }

        if(Skill1Slider.value >= Skill1DelayMax)
        {
            Skill1Icon.SetActive(true);
        }
        else
        {
            Skill1Icon.SetActive(false);
        }

        if (Skill2Slider.value >= Skill2DelayMax)
        {
            Skill2Icon.SetActive(true);
        }
        else
        {
            Skill2Icon.SetActive(false);
        }

        //스킬 1
        if (Input.GetKey(KeyCode.Z))
        {
            if (Skill1Delay > Skill1DelayMax)
            {
                Skill1Delay = 0;

                SoundManager.instance.SFXPlay("Skill1", Skill1SFX);

                HP.value += 15;

                Skill1Use -= 1;
            }
            else
            {
                if (Skill1Use <= 0)
                {
                    print("사용 횟수 초과");
                    SoundManager.instance.SFXPlay("NoUse", NoUseSFX);
                    //StartCoroutine(ShowNoCol());
                    StartCoroutine(ShowNoUse());
                    return;
                }

                print("스킬이 준비되지 않음");
                SoundManager.instance.SFXPlay("NoCol", NoColSFX);
                StartCoroutine(ShowNoCol());
                //StartCoroutine(ShowNoUse());
            }
        }

        //스킬 2
        if (Input.GetKey(KeyCode.X))
        {
            if (Skill2Delay > Skill2DelayMax)
            {
                Skill2Delay = 0;

                SoundManager.instance.SFXPlay("Boom", BoomSFX);

                StartCoroutine(BoomAction());

                SKill2Use -= 1;
            }
            else
            {
                if (SKill2Use <= 0)
                {
                    print("사용 횟수 초과");
                    SoundManager.instance.SFXPlay("NoUse", NoUseSFX);
                    //StartCoroutine(ShowNoCol());
                    StartCoroutine(ShowNoUse());
                    return;
                }

                print("스킬이 준비되지 않음");
                SoundManager.instance.SFXPlay("NoCol", NoColSFX);
                StartCoroutine(ShowNoCol());
                //StartCoroutine(ShowNoUse());
            }
        }

        Skill1UseText.text = Skill1Use.ToString();
        Skill2UseText.text = SKill2Use.ToString();

        if (HP.value <= 0 || Engine.value <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        ///// 플레이어 레벨
        ///
        if(Input.GetKeyDown(KeyCode.K))
        {
            PlayerLevel += 1;
        }

        if (PlayerLevel >= 4)
        {
            PlayerLevel = 4;
        }

        if (PlayerLevel == 1)
        {
            ShotDelayMax = 0.1f;
            TurretLevel3_1.SetActive(false);
            TurretLevel3_2.SetActive(false);
            TurretLevel4_1.SetActive(false);
            TurretLevel4_2.SetActive(false);
        }
        if (PlayerLevel == 2)
        {
            ShotDelayMax = 0.07f;
            TurretLevel3_1.SetActive(false);
            TurretLevel3_2.SetActive(false);
            TurretLevel4_1.SetActive(false);
            TurretLevel4_2.SetActive(false);
            PlayerLevelText.text = "Player Level " + PlayerLevel + "";
        }
        if (PlayerLevel == 3)
        {
            ShotDelayMax = 0.05f;
            TurretLevel3_1.SetActive(true);
            TurretLevel3_2.SetActive(true);
            TurretLevel4_1.SetActive(false);
            TurretLevel4_2.SetActive(false);
            PlayerLevel4Turret.SetActive(true);
            PlayerLevelText.text = "Player Level " + PlayerLevel + "";

        }
        if (PlayerLevel == 4)
        {
            ShotDelayMax = 0.025f;
            TurretLevel3_1.SetActive(true);
            TurretLevel3_2.SetActive(true);
            TurretLevel4_1.SetActive(true);
            TurretLevel4_2.SetActive(true);
            PlayerLevelText.text = "Level Max";
        }

        //Cheat
        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("Enemy");
            foreach(var obj in go)
            {
                GameObject.Destroy(obj);
            }
        }

        if (Input.GetKeyDown(KeyCode.F2))
        {
            PlayerLevel = 4;
        }

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Skill1Use = Skill1UseMax;
            SKill2Use = Skill2UseMax;
            Skill1Delay = Skill1DelayMax;
            Skill2Delay = Skill2DelayMax;
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            HP.value = HP.maxValue;
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            Engine.value = Engine.maxValue;
        }

        if (Input.GetKeyDown(KeyCode.F6))
        {
            GameManager.instance.StageValue += 1;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" || collision.tag == "EnemyShot" || collision.tag == "Boss" || collision.tag == "Laser")
        {

            if(collision.tag == "Laser")
            {
                HP.value -= 5;
                return;
            }

            HP.value -= 1;

            Instantiate(PlayerTirgger, transform.position, Quaternion.identity);

            StartCoroutine(Flash());
        }
    }

    void MinusEngine()
    {
        Engine.value -= 10;
    }

    IEnumerator BoomAction()
    {
        Boom.SetActive(true);
        yield return new WaitForSeconds(2);
        Boom.SetActive(false);
    }

    IEnumerator ShowNoCol()
    {
        NoColObj.SetActive(true);
        yield return new WaitForSeconds(1);
        NoColObj.SetActive(false);
    }

    IEnumerator ShowNoUse()
    {
        NoUseObj.SetActive(true);
        yield return new WaitForSeconds(1);
        NoUseObj.SetActive(false);
    }

    public IEnumerator ShowMaxLevelAction()
    {
        PlayerLevelObj.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        PlayerLevelObj.SetActive(false);
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
