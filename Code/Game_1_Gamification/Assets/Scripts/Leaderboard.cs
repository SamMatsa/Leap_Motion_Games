using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    //leadboard1
    public Text name11;
    public Text score11;

    public Text name12;
    public Text score12;

    public Text name13;
    public Text score13;

    public Text name14;
    public Text score14;

    public Text name15;
    public Text score15;

    //leaderboard2
    public Text name21;
    public Text score21;

    public Text name22;
    public Text score22;

    public Text name23;
    public Text score23;

    public Text name24;
    public Text score24;

    public Text name25;
    public Text score25;

    //leaderboard3
    public Text name31;
    public Text score31;

    public Text name32;
    public Text score32;

    public Text name33;
    public Text score33;

    public Text name34;
    public Text score34;

    public Text name35;
    public Text score35;

    // Start is called before the first frame update
    void Start()
    {
        //leadboard1
        name11.text = SessionData.getLeaderboard(1,1,"name");
        score11.text = SessionData.getLeaderboard(1, 1, "score");

        name12.text = SessionData.getLeaderboard(1, 2, "name");
        score12.text = SessionData.getLeaderboard(1, 2, "score");

        name13.text = SessionData.getLeaderboard(1, 3, "name");
        score13.text = SessionData.getLeaderboard(1, 3, "score");

        name14.text = SessionData.getLeaderboard(1, 4, "name");
        score14.text = SessionData.getLeaderboard(1, 4, "score");

        name15.text = SessionData.getLeaderboard(1, 5, "name");
        score15.text = SessionData.getLeaderboard(1, 5, "score");

        //leaderboard2
        name21.text = SessionData.getLeaderboard(2, 1, "name");
        score21.text = SessionData.getLeaderboard(2, 1, "score");

        name22.text = SessionData.getLeaderboard(2, 2, "name");
        score22.text = SessionData.getLeaderboard(2, 2, "score");

        name23.text = SessionData.getLeaderboard(2, 3, "name");
        score23.text = SessionData.getLeaderboard(2, 3, "score");

        name24.text = SessionData.getLeaderboard(2, 4, "name");
        score24.text = SessionData.getLeaderboard(2, 4, "score");

        name25.text = SessionData.getLeaderboard(2, 5, "name");
        score25.text = SessionData.getLeaderboard(2, 5, "score");

        //leaderboard3
        name31.text = SessionData.getLeaderboard(3, 1, "name");
        score31.text = SessionData.getLeaderboard(3, 1, "score");

        name32.text = SessionData.getLeaderboard(3, 2, "name");
        score32.text = SessionData.getLeaderboard(3, 2, "score");

        name33.text = SessionData.getLeaderboard(3, 3, "name");
        score33.text = SessionData.getLeaderboard(3, 3, "score");

        name34.text = SessionData.getLeaderboard(3, 4, "name");
        score34.text = SessionData.getLeaderboard(3, 4, "score");

        name35.text = SessionData.getLeaderboard(3, 5, "name");
        score35.text = SessionData.getLeaderboard(3, 5, "score");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
