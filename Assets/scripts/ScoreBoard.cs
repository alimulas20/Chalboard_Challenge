using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text Score;
    public Text Level;
    public Text Correct;
    public Text Accuracy;
    void Start()
    {
        Score.text = timerCont.Score.ToString();
        Level.text = timerCont.mult.ToString();
        Correct.text = timerCont.QuesCount - timerCont.wrongCount + " of " + timerCont.QuesCount;
        Accuracy.text = (100*(timerCont.QuesCount - timerCont.wrongCount) / timerCont.QuesCount).ToString();

    }
    public void next()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
   
}
