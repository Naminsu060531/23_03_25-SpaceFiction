using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public static int CurScore;
    public static float CurTime;
    [SerializeField] public static bool LoadValue = false;
    [SerializeField] public static List<ScoreData> ScoreList;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else 
        {
            Destroy(gameObject);
        }

        ScoreLoad();

    }

    public void ScoreLoad()
    {
        if(LoadValue == true)
        {
            return;
        }

        ScoreList = new List<ScoreData>();

        for (int i = 5; i > 0; i--)
        {
            ScoreData newScore = new ScoreData("---", i * 600, 60);
            ScoreList.Add(newScore);
            print("Load");
        }

        LoadValue = true;
    }

    public void ScoreInput(string name)
    {
        ScoreData checkData = new ScoreData(name, CurScore, CurTime);

        print(checkData + "####");

        for (int i = 0; i < ScoreList.Count; i++)
        {
            if(checkData.Score > ScoreList[i].Score)
            {
                ScoreData tempData = ScoreList[i];
                ScoreList[i] = checkData;
                checkData = tempData;
            }

            else if (checkData.Score == ScoreList[i].Score)
            {
                if(checkData.Time < ScoreList[i].Time)
                {
                    ScoreData tempData = ScoreList[i];
                    ScoreList[i] = checkData;
                    checkData = tempData;
                }
            }

        }

        CurScore = 0;
        CurTime = 0;
    }


}
