using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlatform : MonoBehaviour
{

    private Player_Script player;

    [SerializeField]
    private float accelerationMultiplier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Check if the player inside the collider
    void OnTriggerEnter2D(Collider2D other)
    {

        player = other.gameObject.GetComponent<Player_Script>();

        player.speedPaltform(1, accelerationMultiplier);

        Debug.Log("Player entered speed zone");
    }

    // Check if the player has left the collider
    void OnTriggerExit2D(Collider2D other)
    {

        player = other.gameObject.GetComponent<Player_Script>();

        player.speedPaltform(0);

        Debug.Log("Player left speed zone");
    }
}
