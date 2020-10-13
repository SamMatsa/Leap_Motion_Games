using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public Text difficulty;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int difficultyInt = SessionData.getDifficulty();
        string text = "";

        switch (difficultyInt)
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

        difficulty.text = text;
    }
}
