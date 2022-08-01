using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class TopScorer : MonoBehaviour
{
    public Text Score;
    public Text LPIText;
    public Text[] Points;
    public Text[] Dates;
    int DPI;
    void Start()
    {
        Score.text = timerCont.Score.ToString();
        if (PlayerPrefs.GetString("6") != "1") {
            PlayerPrefs.SetString("1", "11800,23/07/2022");
            PlayerPrefs.SetString("2", "11600,21/07/2022");
            PlayerPrefs.SetString("3", "11400,22/07/2022");
            PlayerPrefs.SetString("4", "10600,20/07/2022");
            PlayerPrefs.SetString("5", "10400,25/07/2022");
            PlayerPrefs.SetString("6", "1");
        }
        System.DateTime theTime = System.DateTime.Now;
        string date =theTime.ToString("dd-MM-yyyy").Replace('-','/');

        string[] pointscore;
        bool shift = false;
        for(int i = 0; i < 5; i++)
        {
            pointscore = PlayerPrefs.GetString((i + 1).ToString()).Split(',');
            if (!shift&&timerCont.Score> System.Convert.ToInt32(pointscore[0]))
            {
                shift = true;
                Points[i].text = timerCont.Score.ToString();
                Dates[i].text = date;
                LPIText.text = (1290/(i+1) + 1500).ToString();
            }
            if (shift)
            {
                if (i < 4)
                {
                    Points[i + 1].text = pointscore[0];
                    Dates[i + 1].text = pointscore[1];                 
                }
            }
            else
            {
                Points[i].text = pointscore[0];
                Dates[i].text = pointscore[1];
            }
            
        }
        if (!shift)
            LPIText.text = "1500";
        shift=false;
        for(int i = 1; i < 6; i++)
        {
            PlayerPrefs.SetString((i).ToString(), Points[i-1].text + "," + Dates[i-1].text);
        }
   

        
    }

    public void next()
    {

        SceneManager.LoadScene(1);
    }
    public void exit()
    {
        Application.Quit();
    }

}
