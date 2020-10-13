using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    public GameObject buffField1;
    public GameObject buffField2;
    public GameObject buffField3;
    public GameObject buffField4;

    public GameObject weaponField1;
    public GameObject weaponField2;
    public GameObject weaponField3;

    public Text userCoins;

    // Start is called before the first frame update
    void Start()
    {
        buffField1.SetActive(!SessionData.getBuffs()[1]);
        buffField2.SetActive(!SessionData.getBuffs()[2]);
        buffField3.SetActive(!SessionData.getBuffs()[3]);
        buffField4.SetActive(!SessionData.getBuffs()[4]);

        weaponField1.SetActive(!SessionData.getWeapons()[1]);
        weaponField2.SetActive(!SessionData.getWeapons()[2]);
        weaponField3.SetActive(!SessionData.getWeapons()[3]);
    }

    // Update is called once per frame
    void Update()
    {
        userCoins.text = SessionData.getCoins().ToString();
    }

    public void buyBuff(int buff)
    {
        Dictionary<int, bool> buffs = SessionData.getBuffs();
        int coins = SessionData.getCoins();
        int price;

        switch (buff)
        {
            case 2:
                price = 3;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    buffs[buff] = !buffs[buff];
                    SessionData.setBuffs(buffs);
                    buffField2.SetActive(false);
                }
                break;
            case 3:
                price = 3;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    buffs[buff] = !buffs[buff];
                    SessionData.setBuffs(buffs);
                    buffField3.SetActive(false);
                }
                break;
            case 4:
                price = 5;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    buffs[buff] = !buffs[buff];
                    SessionData.setBuffs(buffs);
                    buffField4.SetActive(false);
                }
                break;
        }
        SessionData.saveSessionFile();
    }

    public void buyWeapon(int weapon)
    {
        Dictionary<int, bool> weapons = SessionData.getWeapons();
        int coins = SessionData.getCoins();
        int price;

        switch (weapon)
        {
            case 2:
                price = 2;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    weapons[weapon] = !weapons[weapon];
                    SessionData.setWeapons(weapons);
                    weaponField2.SetActive(false);
                }
                break;
            case 3:
                price = 4;
                if (price <= coins)
                {
                    coins = coins - price;
                    SessionData.setCoins(coins);
                    weapons[weapon] = !weapons[weapon];
                    SessionData.setWeapons(weapons);
                    weaponField3.SetActive(false);
                }
                break;
        }
        SessionData.saveSessionFile();
    }

}
