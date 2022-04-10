using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PauseMenu : MonoBehaviour
{
    public GameObject mappingPanel;
    public GameObject pausePanel;
    public static bool isGamePaused; //can be used for stop controller input when paused.
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
    }

    public void showMapping()
    {
        pausePanel.SetActive(false);
        mappingPanel.SetActive(true);
    }
    public void resumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
