using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Leap;
using UnityEngine.SceneManagement;
using Image = UnityEngine.UI.Image;

public class TrackingHandler : MonoBehaviour
{
    Controller controller;
    bool handIsClosed = false;

    public GameObject playerCharacter;
    public GameObject bossCharacter;
    public GameObject alertSound;

    //player
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

    //boss
    public Sprite bossIdle;
    public Sprite bossSuspense;
    public Sprite bossAlert;

    public Sprite open;
    public Sprite closed;

    AudioSource gameMusic;


    private const float TIMER_DEFAULT = 60f;
    private float timer = TIMER_DEFAULT;
    public Text timerText;
    bool timerActive = false;

    private int exerciseCounter = 0;
    public Text exerciseText;

    private int bustedCounter = 0;
    public Text bustedText;

    private float countdown = 9f;
    public Text countdownText;
    bool countdownActive = true;

    private float transition = 3f;
    bool transitionActive = false;

    private const float EXERCISE_TIMER_DEFAULT = 0.2f;
    private float exerciseTimer = EXERCISE_TIMER_DEFAULT;

    //boss
    float bossCooldownTime = 0f;
    bool bossCooldownTimeNoted = false;

    bool playerIsBusted = false;

    bool idleTimerActive = false;
    private float idleTimer = 0f;
    public Text idleTimerText;

    bool suspenseTimerActive = false;
    private float suspenseTimer = 0f;
    public Text suspenseTimerText;

    bool alertTimerActive = false;
    private float alertTimer = 0f;
    public Text alertTimerText;

    public float randomNumber()
    {
        return Random.Range(3, 7);
    }

    void checkBossTimers()
    {
        if (idleTimerActive)
        {
            idleTimer -= Time.deltaTime;
            if (idleTimer <= 0)
            {
                idleTimerActive = false;
                suspenseTimerActive = true;
            }
            idleTimerText.text = (idleTimer.ToString("F2"));
            return;
        }

        if (suspenseTimerActive)
        {
            suspenseTimer -= Time.deltaTime;
            if (suspenseTimer <= 0)
            {
                suspenseTimerActive = false;
                alertTimerActive = true;
            }
            suspenseTimerText.text = (suspenseTimer.ToString("F2"));
            return;
        }

        if (alertTimerActive)
        {
            if (!bossCooldownTimeNoted)
            {
                bossCooldownTime += alertTimer;
                bossCooldownTimeNoted = true;
            }

            if (!playerIsBusted && handIsClosed)
            {
                alertSound.GetComponent<AudioSource>().Play();
                bustedCounter++;
                bustedText.text = bustedCounter.ToString();
                playerIsBusted = true;
            }

            alertTimer -= Time.deltaTime;

            if (alertTimer <= 0)
            {
                alertTimerActive = false;
                calculateBossTimers();
                idleTimerActive = true;
                bossCooldownTimeNoted = false;
                playerIsBusted = false;
            }
            alertTimerText.text = (alertTimer.ToString("F2"));
            return;
        }
    }

    void calculateBossTimers()
    {
        int difficulty = SessionData.getDifficulty();
        switch (difficulty)
        {
            case 1:
                idleTimer = (5f + randomNumber());
                suspenseTimer = 2f;
                alertTimer = 1.5f;
                break;
            case 2:
                idleTimer = (3f + randomNumber());
                suspenseTimer = 1.5f;
                alertTimer = 1.5f;
                break;
            case 3:
                idleTimer = (1f + randomNumber());
                suspenseTimer = 0.8f;
                alertTimer = 1.5f;
                break;
        }

    }
    //

    void refreshExerciseCounter()
    {
        exerciseText.text = exerciseCounter.ToString();
    }

    void refreshTransition()
    {
        if (transitionActive)
        {
            transition -= Time.deltaTime;
            if (transition <= 0)
            {
                finishGame();
            }
        }
    }

    void finishGame()
    {
        //Game
        SessionData.increementNumberOfGamesPlayed();
        SessionData.setTime(TIMER_DEFAULT);
        SessionData.setCooldown(bossCooldownTime);
        SessionData.setTotalTime(TIMER_DEFAULT - bossCooldownTime);
        SessionData.setExercisesTotal(exerciseCounter);
        SessionData.setExercisesPerMinute((exerciseCounter*60)/(TIMER_DEFAULT - bossCooldownTime));
        SessionData.setBustedTotal(bustedCounter);

        //Endcard
        SessionData.calcEndCard();
        SessionData.createLevelFile();
        SessionData.saveSessionFile();

        SceneManager.LoadScene("EndScreen");
    }

    void refreshCountdown()
    {
        if (countdownActive)
        {
            countdown -= Time.deltaTime;

            if(countdown > 6)
            {
                countdownText.text = "Mach dich bereit!";
                return;
            }

            if (countdown > 5)
            {
                countdownText.text = "3";
                return;
            }

            if (countdown > 4)
            {
                countdownText.text = "2";
                return;
            }

            if (countdown > 3)
            {
                countdownText.text = "1";
                gameMusic.Play();
                return;
            }

            if (countdown > 2)
            {
                countdownText.text = "LOS!";
                return;
            }

            if (countdown <= 0)
            {
                gameMusic.clip = Resources.Load<AudioClip>("funk");
                buttonStart();
                countdownText.text = "";
                countdownActive = false;
            }
        }
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
                transitionActive = true;
                gameMusic.clip = Resources.Load<AudioClip>("whistle");
                gameMusic.Play();
            }
        }
        //timer ist nie null!
        timerText.text = (timer.ToString("F2"));
    }

    public void countDownExerciseTimer()
    {
        if (exerciseTimer < 0)
        {
            exerciseTimer = 0;
            exerciseCounter++;
            playerCharacter.GetComponent<Image>().sprite = closed;
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
            calculateBossTimers();
            idleTimerActive = true;
            gameMusic.Play();
        }
    }

    public void resetGame()
    {
        exerciseCounter = 0;
        refreshExerciseCounter();

        timer = TIMER_DEFAULT;
        refreshTimer();

        exerciseTimer = EXERCISE_TIMER_DEFAULT;
    }

    public void selectCharacter() {
        int selectedCharacter = SessionData.getSelectedCharacter();
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
        playerCharacter.GetComponent<Image>().sprite = open;
        bossCharacter.GetComponent<Image>().sprite = bossIdle;
    }


    // Start is called before the first frame update
    void Start()
    {
        gameMusic = GetComponent<AudioSource>();
        gameMusic.clip = Resources.Load<AudioClip>("whistle");
        controller = new Controller();
        resetGame();
        bustedText.text = bustedCounter.ToString();
        selectCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        refreshTimer();
        refreshCountdown();
        refreshTransition();
        checkBossTimers();
        Frame frame = controller.Frame();

        if (frame.Hands.Count > 0)
        {
            List<Hand> allHands = frame.Hands;
            Hand hand = allHands[0];
            Leap.Vector handPosition = hand.PalmPosition;

            List<Finger> allFingers = hand.Fingers;
            Finger thumb = allFingers[0];
            Leap.Vector thumbDirection = thumb.Direction;

            if (hand.IsRight)
            {
                if(thumbDirection.x > 0.2 && timerActive)
                {
                    Debug.Log("Daumen drinnen!!");
                    countDownExerciseTimer();
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

        if (!handIsClosed)
        {
            playerCharacter.GetComponent<Image>().sprite = open;
        }

        if (idleTimerActive)
        {
            bossCharacter.GetComponent<Image>().sprite = bossIdle;
        }

        if (suspenseTimerActive)
        {
            bossCharacter.GetComponent<Image>().sprite = bossSuspense;
        }

        if (alertTimerActive)
        {
            bossCharacter.GetComponent<Image>().sprite = bossAlert;
        }
    }
}
