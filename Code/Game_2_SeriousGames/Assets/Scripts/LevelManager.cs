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

    public void changeToCharacterSelect(int worldSel)
    {
        if ((worldSel - 1) <= SessionData.getNumberOfGamesPlayed()) {
            SessionData.setWorld(worldSel);
            SceneManager.LoadScene("CharacterSelect");
        }
    }

    public void changeToGame()
    {
        SceneManager.LoadScene("Game" + SessionData.getWorld());
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