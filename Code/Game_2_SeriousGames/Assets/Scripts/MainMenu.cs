using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    public Text userNameText;
    public Text lvlText;
    public Text xpText;

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public GameObject level4;

    public Sprite level1Sprite;
    public Sprite level2Sprite;
    public Sprite level3Sprite;
    public Sprite level4Sprite;

    public Sprite locked;

    // Start is called before the first frame update
    void Start()
    {
        userNameText.text = SessionData.getUserName();
        lvlText.text = "LVL " + SessionData.getLevel();
        xpText.text = SessionData.getTotalUserScore() + " XP";

        level1.GetComponent<Image>().sprite = level1Sprite;
        level2.GetComponent<Image>().sprite = level2Sprite;
        level3.GetComponent<Image>().sprite = level3Sprite;
        level4.GetComponent<Image>().sprite = level4Sprite;

        switch (SessionData.getNumberOfGamesPlayed())
        {
            case 0:
                level2.GetComponent<Image>().sprite = locked;
                level3.GetComponent<Image>().sprite = locked;
                level4.GetComponent<Image>().sprite = locked;
                break;
            case 1:
                level3.GetComponent<Image>().sprite = locked;
                level4.GetComponent<Image>().sprite = locked;
                break;
            case 2:
                level4.GetComponent<Image>().sprite = locked;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
