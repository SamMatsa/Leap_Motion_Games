using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class Character : MonoBehaviour
{

    public int character;
    public Sprite characterUnlocked;
    public Sprite characterLocked;
    private Image img;


    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        bool value = SessionData.isCharacterUnlocked(character);

        if (value)
        {
            GetComponent<Image>().sprite = characterUnlocked;
        }
        else
        {
            GetComponent<Image>().sprite = characterLocked;
        }
    }
}