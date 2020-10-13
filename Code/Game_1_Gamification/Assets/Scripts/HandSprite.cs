using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using Image = UnityEngine.UI.Image;

public class HandSprite : MonoBehaviour
{
    public Controller controller;
    public Sprite HandOpenRight;
    public Sprite HandClosedRight;
    public Sprite HandOpenLeft;
    public Sprite HandClosedLeft;
    private Image img;

    float timer = 1f;
    float delay = 1f;


    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
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
                    GetComponent<Image>().sprite = HandClosedRight;
                }
                else
                {
                    GetComponent<Image>().sprite = HandOpenRight;
                }
            }
            else
            {
                if (thumbDirection.x < (-0.2))
                {
                    GetComponent<Image>().sprite = HandClosedLeft;
                }
                else
                {
                    GetComponent<Image>().sprite = HandOpenLeft;
                }
            }
        }
    }
}
