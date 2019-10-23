using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Level : MonoBehaviour
{
    public static bool levelComplete = false;

    [SerializeField]
    private GameObject endMenuUI = null;

    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private GameObject platform = null;

    void Start()
    {
        endMenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Use Esc or P buttons to pause
        if (player.GetComponent<BoxCollider2D>().IsTouching(platform.GetComponent<BoxCollider2D>()) ||
            player.GetComponent<CircleCollider2D>().IsTouching(platform.GetComponent<BoxCollider2D>()))
        {
            EndLevel();
        }
    }

    // Displays the endLevel screen when player reaches the end
    public void EndLevel()
    {
        endMenuUI.SetActive(true);
        Time.timeScale = 0f;
        levelComplete = true;
    }

    // Used when clicking on menu button
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    // Used when clicking on the Next Level Button
    public void LoadNextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        int sceneNumber = int.Parse(scene.name.Substring(6)) + 1;
        string nextLevel = "Level_" + sceneNumber;
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevel);
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
