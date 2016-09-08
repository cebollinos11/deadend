using UnityEngine;
using System.Collections;

public class HighScoreManager : MonoBehaviour {

    [HideInInspector]
    public int thisLevelScore,accumulatedScore,Nperfects,LivesLeft,BonusPerfects,BonusLives,totalScore;

    GameManager gm;

    void Awake()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    public void CalculateScoresAfterWinningLevel()
    {
        LivesLeft = gm.GS.lives;
        BonusLives = gm.GS.lives * 10;
        BonusPerfects = Nperfects * 5;
        accumulatedScore = gm.GS.accumulatedScore;

        totalScore = accumulatedScore + thisLevelScore + BonusPerfects + BonusLives;

        gm.GS.accumulatedScore = totalScore;
    }

    public void AddScore()
    {
        thisLevelScore++;
    }

    public void AddPerfect()
    {
        Nperfects++;
    }


}
