using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool paused = false;
    public GameObject pauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    private void Pause()
    {
        
        if (paused)
        {
            pauseMenu.SetActive(!paused);
            Time.timeScale = 1;
        }
        else
        {            
            pauseMenu.SetActive(!paused);
            Time.timeScale = 0;
        }
        paused = !paused;
    }

    public void Resume()
    {
        Pause();
    }
}
