using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{
    public void PlayLevel(int index)
    {
        SceneManager.LoadScene("Level_"+index);
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu");
    }
}
