using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using UnityEngine.UI;
using System;
using Image = UnityEngine.UI.Image;
using UnityEngine.SceneManagement;

public class CrossHair : MonoBehaviour
{
    AudioSource gameMusic;
    public GameObject soundEffectsObject;
    AudioSource soundEffectsSource;

    #region atack + tracking
    public Transform attackPoint;
    public Transform akimboPoint1;
    public Transform akimboPoint2;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public int amountOfAmmo = 6;
    public int amountOfAmmoMAX = 6;
    public Text amountOfAmmoText;
    #endregion

    Vector2 tempPos;
    public Controller controller;

    public Text enemiesKilledCounterText;
    public Text enemiesKilledScoreText;
    int enemiesKilledScore = 0;
    int enemiesKilledScoreMAX;

    //Sprites
    public Sprite buff1Sprite;
    public Sprite buff2Sprite;
    public Sprite buff3Sprite;
    public Sprite buff4Sprite;

    public Sprite weapon1Sprite;
    public Sprite weapon2Sprite;
    public Sprite weapon3Sprite;

    public GameObject selectedBuffObject;
    public GameObject selectedWeaponObject;

    //gameObject
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;
    public GameObject enemy5;
    public GameObject enemy6;
    public GameObject enemy7;
    public GameObject enemy8;
    public GameObject enemy9;
    public GameObject enemy10;

    List<GameObject> allEnemies = new List<GameObject>();

    //exercises
    bool handIsClosed = false;

    public Text exerciseCounterText;
    private const float EXERCISE_TIMER_DEFAULT = 0.1f;
    private float exerciseTimer = EXERCISE_TIMER_DEFAULT;

    //start countdown
    private float startCountdown = 9f;
    public Text startCountdownText;
    bool startCountdownActive = true;

    //timer
    private const float TIMER_DEFAULT = 60f;
    private float timer = TIMER_DEFAULT;
    public Text timerText;
    bool timerActive = false;

    //transition
    private float transition = 3f;
    bool transitionActive = false;

    //reload
    bool reloadInProgress = false;
    public Text reloadText;

    void Start()
    {
        MusicHandler.Instance.gameObject.GetComponent<AudioSource>().Pause();
        //get music
        gameMusic = GetComponent<AudioSource>();
        gameMusic.clip = Resources.Load<AudioClip>("drum");

        //get Sprites
        setSprites();

        //reset game
        SessionData.resetGameData();

        //destroy enemys
        fillAllEnemiesArray();
        killAllEnemies();
        enemiesKilledCounterText.text = "0";
        enemiesKilledScoreText.text = enemiesKilledScore.ToString();
        getCrosshairAndHitbox();
        setAttackDamage();
        setAmountOfAmmo();
        tempPos = transform.position;
        controller = new Controller();
    }

    void Update()
    {
        enemiesKilledCounterText.text = SessionData.getEnemiesKilled().ToString();
        enemiesKilledScoreMAX = SessionData.calcEnemiesKilledScore();
        if(enemiesKilledScore < enemiesKilledScoreMAX)
        {
            enemiesKilledScore += 10;
        }

        enemiesKilledScoreText.text = enemiesKilledScore.ToString();
        exerciseCounterText.text = SessionData.getExercisesTotal().ToString();
        amountOfAmmoText.text = amountOfAmmo + " / " + amountOfAmmoMAX;

        changePosition();
        refreshStartCountdown();
        refreshTimer();
        refreshTransition();
        checkReload();

    }

    void fillAllEnemiesArray()
    {
        if (enemy1 != null) allEnemies.Add(enemy1);
        if (enemy2 != null) allEnemies.Add(enemy2);
        if (enemy3 != null) allEnemies.Add(enemy3);
        if (enemy4 != null) allEnemies.Add(enemy4);
        if (enemy5 != null) allEnemies.Add(enemy5);
        if (enemy6 != null) allEnemies.Add(enemy6);
        if (enemy7 != null) allEnemies.Add(enemy7);
        if (enemy8 != null) allEnemies.Add(enemy8);
        if (enemy9 != null) allEnemies.Add(enemy9);
        if (enemy10 != null) allEnemies.Add(enemy10);
    }

    void killAllEnemies()
    {
        foreach(GameObject enemy in allEnemies)
        {
            enemy.GetComponent<Enemy>().KillAndRespawn();
        }
    }

    void setSprites()
    {
        Sprite currentBuffSprite = null;
        Sprite currentWeaponSprite = null;

        switch (SessionData.getSelectedBuff())
        {
            case 1:
                currentBuffSprite = buff1Sprite;
                break;
            case 2:
                currentBuffSprite = buff2Sprite;
                break;
            case 3:
                currentBuffSprite = buff3Sprite;
                break;
            case 4:
                currentBuffSprite = buff4Sprite;
                break; ;
        }

        switch (SessionData.getSelectedWeapon())
        {
            case 1:
                currentWeaponSprite = weapon1Sprite;
                break;
            case 2:
                currentWeaponSprite = weapon2Sprite;
                break;
            case 3:
                currentWeaponSprite = weapon3Sprite;
                break;
        }

        selectedBuffObject.GetComponent<Image>().sprite = currentBuffSprite;
        selectedWeaponObject.GetComponent<Image>().sprite = currentWeaponSprite;
    }

    void refreshStartCountdown()
    {
        if (startCountdownActive)
        {
            startCountdown -= Time.deltaTime;

            if (startCountdown > 6)
            {
                startCountdownText.text = "Mach dich bereit!";
                return;
            }

            if (startCountdown > 5)
            {
                startCountdownText.text = "3";
                return;
            }

            if (startCountdown > 4)
            {
                startCountdownText.text = "2";
                return;
            }

            if (startCountdown > 3)
            {
                startCountdownText.text = "1";
                gameMusic.Play();
                return;
            }

            if (startCountdown > 2)
            {
                startCountdownText.text = "LOS!";
                return;
            }

            if (startCountdown <= 0)
            {
                //
                string sound = "";
                switch (SessionData.getWorld())
                {
                    case 1:
                        sound = "solid";
                        break;
                    case 2:
                        sound = "yakuza";
                        break;
                    case 3:
                        sound = "france";
                        break;
                    case 4:
                        sound = "ace";
                        break;
                }
                gameMusic.clip = Resources.Load<AudioClip>(sound);
                startGame();
                startCountdownText.text = "";
                startCountdownActive = false;
            }
        }
    }

    void startGame()
    {
        if (!timerActive)
        {
            timerActive = true;
            gameMusic.Play();
        }
    }

    void refreshTimer()
    {
        if (timerActive)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
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
        SessionData.calcEndCard();
        SessionData.createLevelFile();
        SessionData.saveSessionFile();
        SceneManager.LoadScene("EndScreen");
    }

    #region crosshair and damage
    private void getCrosshairAndHitbox()
    {
        if(SessionData.getSelectedBuff() == 4)
        {
            transform.Find("attackPoint_solo").gameObject.SetActive(false);
            transform.Find("crosshair_solo").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("attackPoint_akimbo1").gameObject.SetActive(false);
            transform.Find("attackPoint_akimbo2").gameObject.SetActive(false);
            transform.Find("crosshair_akimbo1").gameObject.SetActive(false);
            transform.Find("crosshair_akimbo2").gameObject.SetActive(false);
        }
    }

    void calcNewPosition(Vector palmPos)
    {
        tempPos.x = (palmPos.x / 14);
        tempPos.y = ((palmPos.y - 250) / 18);
        transform.position = tempPos;
    }

    public void checkReload()
    {
        if (amountOfAmmo <= 0 && !reloadInProgress)
        {
            //play reload sound

            reloadInProgress = true;
            Invoke("PlayReloadSound", 1f);
            Invoke("Reload", 1.5f);
            SessionData.incrementCooldownBy(1.5f);
        }

        if (reloadInProgress)
        {
            reloadText.text = "LADE\nNACH...";
        }
        else
        {
            reloadText.text = "";
        }
    }

    void Reload()
    {
        amountOfAmmo = amountOfAmmoMAX;
        reloadInProgress = false;
    }

    void PlayWeaponSound()
    {
        string sound = "weapon1";
        switch (SessionData.getSelectedWeapon())
        {
            case 1:
                sound = "weapon1";
                break;
            case 2:
                sound = "weapon2";
                break;
            case 3:
                sound = "weapon3";
                break;
        }

        soundEffectsSource = soundEffectsObject.GetComponent<AudioSource>();
        soundEffectsSource.clip = Resources.Load<AudioClip>(sound);
        soundEffectsSource.Play();
    }

    void PlayReloadSound()
    {
        soundEffectsSource = soundEffectsObject.GetComponent<AudioSource>();
        soundEffectsSource.clip = Resources.Load<AudioClip>("reload");
        soundEffectsSource.Play();
    }

    public void checkAndAttack()
    {
        if (exerciseTimer < 0)
        {
            exerciseTimer = 0;
            if (timerActive)
            {
                //evtl cooldowntimer
                if (!reloadInProgress)
                {
                    PlayWeaponSound();
                    amountOfAmmo--;
                    SessionData.incrementExercisesTotal();
                    Attack();
                }
            }
        }
        else if (exerciseTimer > 0)
        {
            exerciseTimer -= Time.deltaTime;
        }
    }

    private void changePosition()
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

            calcNewPosition(hand.PalmPosition);

            if (hand.IsRight)
            {
                if (thumbDirection.x > 0.05)
                {
                    //Debug.Log("CLOSED");
                    //closedRight
                    checkAndAttack();
                    handIsClosed = true;
                }
                else
                {
                    //Debug.Log("OPEN");
                    //openRight
                    exerciseTimer = EXERCISE_TIMER_DEFAULT;
                    handIsClosed = false;
                }
            }
            else
            {
                if (thumbDirection.x < (-0.05))
                {
                    checkAndAttack();
                    handIsClosed = true;
                }
                else
                {
                    exerciseTimer = EXERCISE_TIMER_DEFAULT;
                    handIsClosed = false;
                }
            }


        }

        //Debug.Log(tempPos);
    }

    void Attack()
    {
        Collider2D[] hitEnemies = null;

        if (SessionData.getSelectedBuff() != 4)
        {
            hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            if(hitEnemies.Length == 0)
            {
                SessionData.incrementFailedShots();
            }
            ApplyDamage(hitEnemies);
        }

        if (SessionData.getSelectedBuff() == 4)
        {
            Collider2D[] hitEnemies1 = Physics2D.OverlapCircleAll(akimboPoint1.position, attackRange, enemyLayers);
            Collider2D[] hitEnemies2 = Physics2D.OverlapCircleAll(akimboPoint2.position, attackRange, enemyLayers);
            if (hitEnemies1.Length == 0 || hitEnemies2.Length == 0)
            {
                SessionData.incrementFailedShots();
            }
            ApplyDamage(hitEnemies1);
            ApplyDamage(hitEnemies2);
        }
    }

    void ApplyDamage(Collider2D[] enemies)
    {
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(getAttackDamage());
            Debug.Log("HIT" + enemy.name);
            Debug.Log("HEALTH" + enemy.GetComponent<Enemy>().GetHealth());
        }
    }

    int getAttackDamage()
    {
        if (SessionData.getSelectedBuff() == 2)
        {
            return (int)(attackDamage * 1.7f);
        }
        return attackDamage;
    }

    void setAttackDamage()
    {
        switch (SessionData.getSelectedWeapon())
        {
            case 1:
                attackDamage = 20;
                break;
            case 2:
                attackDamage = 40;
                break;
            case 3:
                attackDamage = 100;
                break;
        }
    }

    void setAmountOfAmmo()
    {
        switch (SessionData.getSelectedWeapon())
        {
            case 1:
                amountOfAmmoMAX = 15;
                break;
            case 2:
                amountOfAmmoMAX = 6;
                break;
            case 3:
                amountOfAmmoMAX = 2;
                break;
        }


        if (SessionData.getSelectedBuff() == 3)
        {
            amountOfAmmoMAX = amountOfAmmoMAX * 2;
        }

        amountOfAmmo = amountOfAmmoMAX;
    }

    public float GetCooldownScalePercent()
    {
        return (float) amountOfAmmo / amountOfAmmoMAX;
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
        {
            return;
        }
        Gizmos.DrawSphere(attackPoint.position, attackRange);

        if (akimboPoint1 == null)
        {
            return;
        }
        Gizmos.DrawSphere(akimboPoint1.position, attackRange);

        if (akimboPoint2 == null)
        {
            return;
        }
        Gizmos.DrawSphere(akimboPoint1.position, attackRange);
    }
        #endregion
    
}