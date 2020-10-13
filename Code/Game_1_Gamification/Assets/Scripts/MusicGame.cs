using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicGame : MonoBehaviour
{

    AudioSource gameMusic;

    private const float TIMER_DEFAULT = 15f;
    private float timer = TIMER_DEFAULT;
    private bool musicStopped = false;

    // Start is called before the first frame update
    void Start()
    {


        gameMusic = GetComponent<AudioSource>();
        gameMusic.clip = Resources.Load<AudioClip>("funk");

        gameMusic.PlayDelayed(2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 5 && musicStopped == false)
        {
            gameMusic.clip = Resources.Load<AudioClip>("whistle");
            musicStopped = true;
            gameMusic.Play();
        }

        if(timer <= 0)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }
}
