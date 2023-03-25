using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RankingManager : MonoBehaviour
{
    public Text[] NameText, ScoreText, TimeText;
    public InputField InputName;
    public GameObject RankingPanel;
    public Text ScoreTextGameOver;
    public Text TimeTextGameOver;
    public GameObject InRankingPanel;
    public Text ScoreTextInRanking;
    public Text TimeTextInRanking;
    public GameObject GameClearPanel;

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            print("GameOver");

            try
            {
                ScoreTextGameOver.text = DataManager.CurScore.ToString();
                TimeTextGameOver.text = DataManager.CurTime.ToString();
            }
            catch
            {
                print("Null");
            }

        }

        if (SceneManager.GetActiveScene().name == "GameEnd")
        {
            print("GameClear");

            try
            {
                ScoreTextInRanking.text = DataManager.CurScore.ToString();
                TimeTextInRanking.text = DataManager.CurTime.ToString();
            }
            catch
            {
                print("Null");
            }

        }
    }

    public void RankingUpdate()
    {
        string name = InputName.text;

        DataManager.instance.ScoreLoad();
        DataManager.instance.ScoreInput(name);

        SetRanking();

        RankingPanel.SetActive(true);

        if(SceneManager.GetActiveScene().name == "GameClear")
        {
            GameClearPanel.SetActive(false);
        }
    }

    public void SetRanking()
    { 
        for (int i = 0; i < NameText.Length; i++)
        {
            NameText[i].text = DataManager.ScoreList[i].Name;
            ScoreText[i].text = DataManager.ScoreList[i].Score.ToString();
            TimeText[i].text = DataManager.ScoreList[i].Time.ToString();
        }

    }

    public void UnRankingPanel()
    {
        RankingPanel.SetActive(false);
    }

    public void Title()
    {
        SceneManager.LoadScene("GameTitle");
    }

}
