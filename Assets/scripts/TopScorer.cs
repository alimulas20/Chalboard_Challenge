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
        /*PlayerPrefs.SetString("1", "21800,23/07/2022");
        PlayerPrefs.SetString("2", "21600,21/07/2022");
        PlayerPrefs.SetString("3", "21400,22/07/2022");
        PlayerPrefs.SetString("4", "20600,20/07/2022");
        PlayerPrefs.SetString("5", "20400,25/07/2022");*/
        string[] SystemDate = System.DateTime.Now.ToString().Split(' ');
        string date = SystemDate[0].Replace('.', '/');
   
        string[] pointscore;
        bool shift = false;
        for(int i = 0; i < 5; i++)
        {
            pointscore = PlayerPrefs.GetString((i + 1).ToString()).Split(',');
            if (!shift&&String.Compare(timerCont.Score.ToString(), pointscore[0]) > 0)
            {
                shift = true;
                Points[i].text = timerCont.Score.ToString();
                Dates[i].text = date;
                
                LPIText.text = (1290/(i+1) + 1500).ToString();
            }
            if (shift&&i<5)
            {
                Points[i+1].text = pointscore[0];
                Dates[i+1].text = pointscore[1];
            }
            else
            {
                Points[i].text = pointscore[0];
                Dates[i].text = pointscore[1];
            }
            
        }
        shift=false;


        
    }

    public void next()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-2);
    }

}
