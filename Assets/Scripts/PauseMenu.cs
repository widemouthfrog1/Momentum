using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * IMPORTANT
 * When using this prefab it must be placed inside a CANVAS.
 * The scene must also have an EVENTSYSTEM in it or the buttons won't work.
 * You will need to disable the PauseMenu (one layer inside the PauseMenuObject) otherwise the pause menu will be visable at all times
 */
public class PauseMenu : MonoBehaviour
{
    
    public static bool GameIsPaused = false;

    [SerializeField]
    private GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Use Esc or P buttons to pause
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    // Used on key input and clicking on resume button
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    // Used only on key input
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    // Used when clicking on menu button
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    // Used when clicking on select level button
    public void SLMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_Select");
    }

    // Used when clicking on menu button
    // Note: This takes you to the main options menu and will exit the current level
    public void OptionsMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Options");
    }

    // Used when clicking on quit button
    public void QuitGame()
    {
        Application.Quit();
    }

}

