using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public Text lives,totalHighScore,livesbonusval,perfectbonusval,ninjasdodgedval,totalhighscoreval,accumulatedHSval;

    public GameObject GameOver,LevelFinished;




    GameManager gm;

    void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        GameOver.SetActive(false);
        LevelFinished.SetActive(false);
    }

    public void UpdateUI()
    {

        //for gameover

        totalHighScore.text = gm.GS.accumulatedScore.ToString();

        //for UI
        //accumulatedHS.text = "Score  "+ gm.GS.accumulatedScore.ToString();
        lives.text = "Lives  " + gm.GS.lives;


        

    }


    public void ShowGameOver()
    {
        totalHighScore.text = ( gm.HSmanager.thisLevelScore + gm.GS.accumulatedScore + gm.HSmanager.Nperfects*6).ToString();
        iTween.ShakeScale(totalHighScore.gameObject, iTween.Hash("x", 4, "y", 4, "time",0.5));
        //UpdateUI();
        GameOver.SetActive(true);
        iTween.MoveFrom(GameOver.gameObject, iTween.Hash("islocal", true, "y", 20));
    }

    IEnumerator ShowLevelFinished()
    {

        float pause = 0.5f;
        //gm.camHandler.PlayBump();

        AudioClip hit1 = AudioManager.Instance.swing;

        LevelFinished.SetActive(true);

        livesbonusval.gameObject.SetActive(false);
        perfectbonusval.gameObject.SetActive(false);
        ninjasdodgedval.gameObject.SetActive(false);
        totalhighscoreval.gameObject.SetActive(false);
        accumulatedHSval.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1f);
        AudioManager.PlayClip(hit1);
        ninjasdodgedval.gameObject.SetActive(true);

        yield return new WaitForSeconds(pause);
        AudioManager.PlayClip(hit1);
        perfectbonusval.gameObject.SetActive(true);

        yield return new WaitForSeconds(pause);
        AudioManager.PlayClip(hit1);
        livesbonusval.gameObject.SetActive(true);

        yield return new WaitForSeconds(pause);
        AudioManager.PlayClip(hit1);
        accumulatedHSval.gameObject.SetActive(true);

        yield return new WaitForSeconds(pause);
        AudioManager.PlayClip(AudioManager.Instance.coin);
        totalhighscoreval.gameObject.SetActive(true);

        


    }
    public void ShowLevelFinishedPanel()
    {
        UpdateUI();

        //for win
        livesbonusval.text = gm.HSmanager.BonusLives.ToString();
        ninjasdodgedval.text = gm.HSmanager.thisLevelScore.ToString();
        perfectbonusval.text = gm.HSmanager.BonusPerfects.ToString();
        accumulatedHSval.text = gm.HSmanager.accumulatedScore.ToString();
        totalhighscoreval.text = gm.HSmanager.totalScore.ToString();

        StartCoroutine(ShowLevelFinished());
    }

}
