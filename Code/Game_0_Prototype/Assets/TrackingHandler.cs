using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Leap;

public class TrackingHandler : MonoBehaviour
{
    Controller controller;
    bool handIsClosed = false;

    private const float TIMER_DEFAULT = 30f;
    private float timer = TIMER_DEFAULT;
    public Text timerText;
    bool timerActive = false;

    //private int inputCounter = 0;
    //public Text inputText;

    private int exerciseCounter = 0;
    public Text exerciseText;

    private const float EXERCISE_TIMER_DEFAULT = 0.2f;
    private float exerciseTimer = EXERCISE_TIMER_DEFAULT;

    //void refreshCounter()
    //{
    //    inputText.text = "INPUT\n" + inputCounter.ToString();
    //}

    void refreshExerciseCounter()
    {
        exerciseText.text = "COUNT\n" + exerciseCounter.ToString();
    }

    void refreshTimer()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                timer = 0;
                timerActive = false;
            }
        }
        //timer ist nie null!
        timerText.text = ("TIME\n" + timer.ToString("F2"));
    }

    public void countDownExerciseTimer()
    {
        if (exerciseTimer < 0)
        {
            exerciseTimer = 0;
            exerciseCounter++;
            refreshExerciseCounter();
        }
        else if (exerciseTimer > 0)
        {
            exerciseTimer -= Time.deltaTime;
        }
    }


    public void buttonReset()
    {
        timerActive = false;
        resetGame();
    }

    public void buttonStart()
    {
        if (!timerActive)
        {
            resetGame();
            timerActive = true;
        }
    }

    public void resetGame()
    {
        exerciseCounter = 0;
        refreshExerciseCounter();

        //inputCounter = 0;
        //refreshCounter();

        timer = TIMER_DEFAULT;
        refreshTimer();

        exerciseTimer = EXERCISE_TIMER_DEFAULT;
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = new Controller();
        resetGame();
    }

    // Update is called once per frame
    void Update()
    {
        refreshTimer();
        Frame frame = controller.Frame();

        if (frame.Hands.Count > 0)
        {
            List<Hand> allHands = frame.Hands;
            Hand hand = allHands[0];
            Vector handPosition = hand.PalmPosition;

            List<Finger> allFingers = hand.Fingers;
            Finger thumb = allFingers[0];
            Vector thumbDirection = thumb.Direction;

            if (hand.IsRight)
            {
                if(thumbDirection.x > 0.2 && timerActive)
                {
                    Debug.Log("Daumen drinnen!!");
                    countDownExerciseTimer();
                    //inputCounter++;
                    //refreshCounter();
                    handIsClosed = true;
                }
                else
                {
                    exerciseTimer = EXERCISE_TIMER_DEFAULT;
                    Debug.Log("Daumen draußen!!");
                    handIsClosed = false;
                }
            }
            else
            {
                if (thumbDirection.x < (-0.2) && timerActive)
                {
                    Debug.Log("Daumen drinnen!!");
                    countDownExerciseTimer();
                    //inputCounter++;
                    //refreshCounter();
                    handIsClosed = true;
                }
                else
                {
                    //Setze ExerciseTimer zurück auf [ZAHL]
                    exerciseTimer = EXERCISE_TIMER_DEFAULT;
                    handIsClosed = false;
                    Debug.Log("Daumen draußen!!");
                }
            }

            //Debug.Log("Anzahl Hände:" + frame.Hands.Count);
            //Debug.Log("X: " + thumbDirection.x + "| Y: " + thumbDirection.y + "| Z:" + thumbDirection.z);
            //Debug.Log("X: " + handPosition.x + "| Y: " + handPosition.y + "| Z:" + handPosition.z);
            //Debug.Log("Linke Hand?:" + firstHand.IsLeft);
        }
    }
}
