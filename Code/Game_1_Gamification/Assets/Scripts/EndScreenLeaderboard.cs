using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenLeaderboard : MonoBehaviour
{
    public Text difficultyText;

    public Text name1;
    public Text score1;

    public Text name2;
    public Text score2;

    public Text name3;
    public Text score3;

    public Text name4;
    public Text score4;

    public Text name5;
    public Text score5;


    // Start is called before the first frame update
    void Start()
    {
        int difficulty = SessionData.getDifficulty();
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

        name1.text = SessionData.getLeaderboard(difficulty, 1, "name");
        score1.text = SessionData.getLeaderboard(difficulty, 1, "score");

        name2.text = SessionData.getLeaderboard(difficulty, 2, "name");
        score2.text = SessionData.getLeaderboard(difficulty, 2, "score");

        name3.text = SessionData.getLeaderboard(difficulty, 3, "name");
        score3.text = SessionData.getLeaderboard(difficulty, 3, "score");

        name4.text = SessionData.getLeaderboard(difficulty, 4, "name");
        score4.text = SessionData.getLeaderboard(difficulty, 4, "score");

        name5.text = SessionData.getLeaderboard(difficulty, 5, "name");
        score5.text = SessionData.getLeaderboard(difficulty, 5, "score");

    }

    // Update is called once per frame
    void Update()
    {
        int difficulty = SessionData.getDifficulty();
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

        name1.text = SessionData.getLeaderboard(difficulty, 1, "name");
        score1.text = SessionData.getLeaderboard(difficulty, 1, "score");

        name2.text = SessionData.getLeaderboard(difficulty, 2, "name");
        score2.text = SessionData.getLeaderboard(difficulty, 2, "score");

        name3.text = SessionData.getLeaderboard(difficulty, 3, "name");
        score3.text = SessionData.getLeaderboard(difficulty, 3, "score");

        name4.text = SessionData.getLeaderboard(difficulty, 4, "name");
        score4.text = SessionData.getLeaderboard(difficulty, 4, "score");

        name5.text = SessionData.getLeaderboard(difficulty, 5, "name");
        score5.text = SessionData.getLeaderboard(difficulty, 5, "score");
    }
}
