using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void changeLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void changeToCharacterSelect()
    {
        if (SessionData.getToolTip())
        {
            SessionData.setToolTip(false);
            changeLevel("HowToPlay1");
        }
        else
        {
            SceneManager.LoadScene("CharacterSelect");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
