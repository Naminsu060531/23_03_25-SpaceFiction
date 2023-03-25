using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int StageValue = 1;
    public int BossCount;
    public Text TimeText;
    public Text StageText;
    public Text ScoreText;
    public GameObject enemyManagerObj;
    public GameObject[] BossObj;

    public GameObject StageStartObj;
    public Text StageStartText;
    public GameObject StageClearObj;
    public Text StageClearText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        InvokeRepeating("AddTime", 0f, 1f);
        StageStartTextShow();
    }

 
    void Update()
    {
        if(StageValue >= 3)
        {
            StageValue = 3;
        }

        TimeText.text = DataManager.CurTime.ToString();
        StageText.text = StageValue.ToString();
        ScoreText.text = DataManager.CurScore.ToString();

        if(BossCount >= 60 && StageValue == 1)
        {
            BossCount = 0;
            BossObj[StageValue - 1].SetActive(true);
        }

        else if (BossCount >= 60 && StageValue == 2)
        {
            BossCount = 0;
            BossObj[StageValue - 1].SetActive(true);
        }

        else if (BossCount >= 60 && StageValue == 3)
        {
            BossCount = 0;
            BossObj[StageValue - 1].SetActive(true);
        }


    }

    void AddTime()
    {
        BossCount += 1;
        DataManager.CurTime += 1;
    }

    public void StageStartTextShow()
    {
        StageStartObj.SetActive(true);
        StageStartText.text = "Stage " + StageValue + " Start";
        Invoke("UnStageShowText", 1.5f);
        
    }

    public void UnStageShowText()
    {
        StageStartObj.SetActive(false);
        enemyManagerObj.SetActive(true);
    }

    public void StageClearTextShow()
    {
        StageClearObj.SetActive(true);
        StageClearText.text = "Stage " + StageValue + " Clear";
        Invoke("UnStageClearShowText", 1.5f);
    }

    public void UnStageClearShowText()
    {
        StageClearObj.SetActive(false);
        StageValue += 1;
        Invoke("StageStartTextShow", 1.5f);
    }

}
