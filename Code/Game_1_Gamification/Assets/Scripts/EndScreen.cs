using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public void startMusic()
    {
        MusicHandler.Instance.gameObject.GetComponent<AudioSource>().Play();
    }

    public GameObject LvlUp;

    static int scoreExercises = 0;
    public Text scoreExercisesText;

    static int scoreBusted = 0;
    public Text scoreBustedText;

    static int scoreDifficulty = 0;
    public Text scoreDifficultyText;

    static int scoreStealth = 0;
    public Text scoreStealthText;

    static int scoreTotal = 0;
    public Text scoreTotalText;

    static bool levelUp = false;

    static int winCoins = 1;
    public Text winCoinsText;

    static int newDifficulty = 0;
    public Text newDifficultyText;

    static int difficulty = 1;
    public Text difficultyText;


    // Start is called before the first frame update
    void Start()
    {
        //Fetch
        scoreExercisesText.text = SessionData.getScoreExercises().ToString();
        scoreBustedText.text = SessionData.getScoreBusted().ToString();
        scoreDifficultyText.text = SessionData.getScoreDifficulty().ToString();
        scoreStealthText.text = SessionData.getScoreStealth().ToString();
        scoreTotalText.text = SessionData.getScoreTotal().ToString();

        levelUp = SessionData.getLevelUp();
        LvlUp.SetActive(levelUp);

        winCoinsText.text = SessionData.getWinCoins().ToString();
        newDifficulty = SessionData.getNewDifficulty();

        switch (newDifficulty)
        {
            case 1:
                newDifficultyText.text = "\"MEDIUM\"\nFREIGESCHALTET";
                break;
            case 2:
                newDifficultyText.text = "\"SCHWER\"\nFREIGESCHALTET";
                break;
            default:
                newDifficultyText.text = "";
                break;
        }

        //difficulty
        difficulty = SessionData.getDifficulty();
        string text = "";

        switch (difficulty)
        {
            case 1:
                text = "LEICHT";
                break;
            case 2:
                text = "MEDIUM";
                break;
            case 3:
                text = "SCHWER";
                break;
        }

        difficultyText.text = text;
        //Mach
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
