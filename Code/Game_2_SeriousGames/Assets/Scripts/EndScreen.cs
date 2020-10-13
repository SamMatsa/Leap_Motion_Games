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

    public Sprite buff1Sprite;
    public Sprite buff2Sprite;
    public Sprite buff3Sprite;
    public Sprite buff4Sprite;

    public Sprite weapon1Sprite;
    public Sprite weapon2Sprite;
    public Sprite weapon3Sprite;

    public GameObject selectedBuffObject;
    public GameObject selectedWeaponObject;

    public Text selectedBuffText;
    public Text selectedWeaponText;

    public Text scoreEnemiesKilledText;
    public Text scoreWorldText;
    public Text scoreFailedShotsText;
    public Text scoreTotalText;
    public Text winCoinsText;
    public Text newWorldText;


    // Start is called before the first frame update
    void Start()
    {
        Sprite currentBuffSprite = null;
        Sprite currentWeaponSprite = null;

        switch (SessionData.getSelectedBuff())
        {
            case 1:
                currentBuffSprite = buff1Sprite;
                selectedBuffText.text = "STETS BEMÜHT";
                break;
            case 2:
                currentBuffSprite = buff2Sprite;
                selectedBuffText.text = "MERH SCHADEN";
                break;
            case 3:
                currentBuffSprite = buff3Sprite;
                selectedBuffText.text = "DOPPELT MUNITION";
                break;
            case 4:
                currentBuffSprite = buff4Sprite;
                selectedBuffText.text = "AKIMBO";
                break; ;
        }

        switch (SessionData.getSelectedWeapon())
        {
            case 1:
                currentWeaponSprite = weapon1Sprite;
                selectedWeaponText.text = "PISTOLE";
                break;
            case 2:
                currentWeaponSprite = weapon2Sprite;
                selectedWeaponText.text = "SHOTGUN";
                break;
            case 3:
                currentWeaponSprite = weapon3Sprite;
                selectedWeaponText.text = "SNIPER";
                break;
        }

        selectedBuffObject.GetComponent<Image>().sprite = currentBuffSprite;
        selectedWeaponObject.GetComponent<Image>().sprite = currentWeaponSprite;
        LvlUp.SetActive(SessionData.getLevelUp());

        scoreEnemiesKilledText.text = SessionData.getEnemiesKilledScore().ToString();
        scoreWorldText.text = SessionData.getScoreWorld().ToString();
        scoreFailedShotsText.text = SessionData.getFailedShotsScore().ToString();
        scoreTotalText.text = SessionData.getScoreTotal().ToString();
        winCoinsText.text = SessionData.getWinCoins().ToString();

        switch (SessionData.getNewWorld())
        {
            case 1:
                newWorldText.text = "LEVEL 2\nFREIGESCHALTET";
                break;
            case 2:
                newWorldText.text = "LEVEL 3\nFREIGESCHALTET";
                break;
            case 3:
                newWorldText.text = "LEVEL 4\nFREIGESCHALTET";
                break;
            default:
                newWorldText.text = "";
                break;
        }

        
}

    // Update is called once per frame
    void Update()
    {
        
    }
}
