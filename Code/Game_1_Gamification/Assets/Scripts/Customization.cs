using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    public Text userName;
    public Text userId;
    public Text userCoins;
    public Text XP;
    public Text LVL;

    public GameObject buyField1;
    public GameObject buyField2;
    public GameObject buyField3;
    public GameObject buyField4;
    public GameObject buyField5;
    public GameObject buyField6;

    public void buyCharacter(int character)
    {
        Dictionary<int, bool> skins = SessionData.getSkins();
        int coins = SessionData.getCoins();
        int price;

        switch (character)
        {
            case 1:
                price = 2;
                if(price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    skins[character] = !skins[character];
                    SessionData.setSkins(skins);
                    buyField1.SetActive(false);
                }
                break;
            case 2:
                price = 3;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    skins[character] = !skins[character];
                    SessionData.setSkins(skins);
                    buyField2.SetActive(false);
                }
                break;
            case 3:
                price = 3;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    skins[character] = !skins[character];
                    SessionData.setSkins(skins);
                    buyField3.SetActive(false);
                }
                break;
            case 4:
                price = 2;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    skins[character] = !skins[character];
                    SessionData.setSkins(skins);
                    buyField4.SetActive(false);
                }
                break;
            case 5:
                price = 4;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    skins[character] = !skins[character];
                    SessionData.setSkins(skins);
                    buyField5.SetActive(false);
                }
                break;
            case 6:
                price = 7;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    skins[character] = !skins[character];
                    SessionData.setSkins(skins);
                    buyField6.SetActive(false);
                }
                break;
        }

        SessionData.saveSessionFile();
    }

    // Start is called before the first frame update
    void Start()
    {
        buyField1.SetActive(!SessionData.getSkins()[1]);
        buyField2.SetActive(!SessionData.getSkins()[2]);
        buyField3.SetActive(!SessionData.getSkins()[3]);
        buyField4.SetActive(!SessionData.getSkins()[4]);
        buyField5.SetActive(!SessionData.getSkins()[5]);
        buyField6.SetActive(!SessionData.getSkins()[6]);

        XP.text = SessionData.getTotalUserScore().ToString() + " XP";
        LVL.text = "LVL " + SessionData.getLevel().ToString();
        userName.text = SessionData.getUserName();
        userId.text = SessionData.getUserId().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        userCoins.text = SessionData.getCoins().ToString();
    }
}
