using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;
using Leap;

public class PlayerCharacterInGame : MonoBehaviour
{
    public Controller controller;

    public Sprite characterImageOpen1;
    public Sprite characterImageOpen2;
    public Sprite characterImageOpen3;
    public Sprite characterImageOpen4;
    public Sprite characterImageOpen5;
    public Sprite characterImageOpen6;

    public Sprite characterImageClosed1;
    public Sprite characterImageClosed2;
    public Sprite characterImageClosed3;
    public Sprite characterImageClosed4;
    public Sprite characterImageClosed5;
    public Sprite characterImageClosed6;

    public Sprite open;
    public Sprite closed;

    private Image img;
    int selectedCharacter;

    void changeSprite(bool isOpen)
    {
        if (isOpen)
        {
            GetComponent<Image>().sprite = open;
        }
        else
        {
            GetComponent<Image>().sprite = closed;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedCharacter = SessionData.getSelectedCharacter();
        img = GetComponent<Image>();
        switch (selectedCharacter)
        {
            case 1:
                open = characterImageOpen1;
                closed = characterImageClosed1;
                break;
            case 2:
                open = characterImageOpen2;
                closed = characterImageClosed2;
                break;
            case 3:
                open = characterImageOpen3;
                closed = characterImageClosed3;
                break;
            case 4:
                open = characterImageOpen4;
                closed = characterImageClosed4;
                break; ;
            case 5:
                open = characterImageOpen5;
                closed = characterImageClosed5;
                break;
            case 6:
                open = characterImageOpen6;
                closed = characterImageClosed6;
                break;
        }

        controller = new Controller();
    }

    // Update is called once per frame
    void Update()
    {

        Frame frame = controller.Frame();

        if (frame.Hands.Count > 0)
        {
            List<Hand> allHands = frame.Hands;
            Hand hand = allHands[0];
            Vector handPosition = hand.PalmPosition;

            List<Finger> allFingers = hand.Fingers;
            Finger thumb = allFingers[0];
            Vector thumbDirection = thumb.Direction;

            Debug.Log(img.tag);

            if (hand.IsRight)
            {
                if (thumbDirection.x > 0.2)
                {
                    changeSprite(false);
                }
                else
                {
                    changeSprite(true);
                }
            }
            else
            {
                if (thumbDirection.x < (-0.2))
                {
                    changeSprite(false);
                }
                else
                {
                    changeSprite(true);
                }
            }
        }


    }
}
