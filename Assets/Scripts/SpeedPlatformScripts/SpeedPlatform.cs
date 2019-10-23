using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPlatform : MonoBehaviour
{

    private Player_Script player;

    [SerializeField]
    private float velocityMultiplier = 1.1f;

    [SerializeField]
    private AudioSource Source;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private float Volume = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        Source.volume = Volume;
        Source.clip = clip;
    }

    // Check if the player inside the collider
    void OnTriggerEnter2D(Collider2D other)
    {

        player = other.gameObject.GetComponent<Player_Script>();

        if (player != null)
        {
            player.SpeedPaltform(1, velocityMultiplier);
            Source.Play();
        }

        //Debug.Log("Player entered speed zone");
    }

    // Check if the player has left the collider
    void OnTriggerExit2D(Collider2D other)
    {

        player = other.gameObject.GetComponent<Player_Script>();

        if (player != null)
        {
            player.SpeedPaltform(0);
        }

        //Debug.Log("Player left speed zone");
    }
}
