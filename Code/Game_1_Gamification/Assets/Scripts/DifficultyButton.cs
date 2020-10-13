using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class DifficultyButton : MonoBehaviour
{
    public int difficulty;
    public Sprite difficultyUnlocked;
    public Sprite difficultyLocked;
    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        int gamesPlayed = SessionData.getNumberOfGamesPlayed();

        if (gamesPlayed >= difficulty-1)
        {
            GetComponent<Image>().sprite = difficultyUnlocked;
        }
        else
        {
            GetComponent<Image>().sprite = difficultyLocked;
        }
    }
}
