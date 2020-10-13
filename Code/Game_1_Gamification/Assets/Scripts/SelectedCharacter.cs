using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SelectedCharacter : MonoBehaviour
{
    public Sprite characterImage1;
    public Sprite characterImage2;
    public Sprite characterImage3;
    public Sprite characterImage4;
    public Sprite characterImage5;
    public Sprite characterImage6;

    private Image img;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        int selectedCharacter = SessionData.getSelectedCharacter();
        Sprite characterSprite = characterImage1;

        switch (selectedCharacter)
        {
            case 1:
                characterSprite = characterImage1;
                break;
            case 2:
                characterSprite = characterImage2;
                break;
            case 3:
                characterSprite = characterImage3;
                break;
            case 4:
                characterSprite = characterImage4;
                break;
            case 5:
                characterSprite = characterImage5;
                break;
            case 6:
                characterSprite = characterImage6;
                break;
        }

        GetComponent<Image>().sprite = characterSprite;
    }
}
