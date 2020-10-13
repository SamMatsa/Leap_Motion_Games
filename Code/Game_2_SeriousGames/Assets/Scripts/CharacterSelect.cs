using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject background;

    public Sprite background1;
    public Sprite background2;
    public Sprite background3;
    public Sprite background4;

    public GameObject buff1;
    public GameObject buff2;
    public GameObject buff3;
    public GameObject buff4;

    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject weapon3;

    public GameObject selectedBuffObject;
    public GameObject selectedWeaponObject;

    public Sprite buff1Sprite;
    public Sprite buff2Sprite;
    public Sprite buff3Sprite;
    public Sprite buff4Sprite;

    public Sprite weapon1Sprite;
    public Sprite weapon2Sprite;
    public Sprite weapon3Sprite;

    public Text selectedBuffText;
    public Text selectedWeaponText;

    public Sprite lockedSprite;


    // Start is called before the first frame update
    void Start()
    {
        //background
        Sprite currentBackgroundSprite = null;

        switch (SessionData.getWorld())
        {
            case 1:
                currentBackgroundSprite = background1;
                break;
            case 2:
                currentBackgroundSprite = background2;
                break;
            case 3:
                currentBackgroundSprite = background3;
                break;
            case 4:
                currentBackgroundSprite = background4;
                break;
        }

        background.GetComponent<Image>().sprite = currentBackgroundSprite;

        //buffs
        if (SessionData.isBuffUnlocked(1))
        {
            buff1.GetComponent<Image>().sprite = buff1Sprite;
        }
        else
        {
            buff1.GetComponent<Image>().sprite = lockedSprite;
        }

        if (SessionData.isBuffUnlocked(2))
        {
            buff2.GetComponent<Image>().sprite = buff2Sprite;
        }
        else
        {
            buff2.GetComponent<Image>().sprite = lockedSprite;
        }

        if (SessionData.isBuffUnlocked(3))
        {
            buff3.GetComponent<Image>().sprite = buff3Sprite;
        }
        else
        {
            buff3.GetComponent<Image>().sprite = lockedSprite;
        }

        if (SessionData.isBuffUnlocked(4))
        {
            buff4.GetComponent<Image>().sprite = buff4Sprite;
        }
        else
        {
            buff4.GetComponent<Image>().sprite = lockedSprite;
        }

        //weapons
        if (SessionData.isWeaponUnlocked(1))
        {
            weapon1.GetComponent<Image>().sprite = weapon1Sprite;
        }
        else
        {
            weapon1.GetComponent<Image>().sprite = lockedSprite;
        }

        if (SessionData.isWeaponUnlocked(2))
        {
            weapon2.GetComponent<Image>().sprite = weapon2Sprite;
        }
        else
        {
            weapon2.GetComponent<Image>().sprite = lockedSprite;
        }

        if (SessionData.isWeaponUnlocked(3))
        {
            weapon3.GetComponent<Image>().sprite = weapon3Sprite;
        }
        else
        {
            weapon3.GetComponent<Image>().sprite = lockedSprite;
        }


    }

    // Update is called once per frame
    void Update()
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
                selectedBuffText.text = "MEHR SCHADEN";
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
    }
}
