using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardElement : MonoBehaviour
{
    public string name;
    public int score;

    public LeaderBoardElement(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
