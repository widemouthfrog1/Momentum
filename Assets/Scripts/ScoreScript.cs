using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * IMPORTANT
 * When using this prefab it must be placed inside a CANVAS.
 */
public class ScoreScript : MonoBehaviour
{
    public GameObject player;

    private Player_Script playerScr;

    public int scoreValue = 0;
    Text score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<Text>();
        player = GameObject.FindWithTag("Player");
        playerScr = player.GetComponent<Player_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreValue = playerScr.GetScore();
        score.text = "Score: " + scoreValue;
    }
}
