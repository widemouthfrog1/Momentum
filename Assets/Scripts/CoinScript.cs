using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    [SerializeField]
    private int scoreAddtion = 0;

    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private float Volume = 0.3f;


    void OnTriggerEnter2D(Collider2D other)
    {
        Player_Script player = other.gameObject.GetComponent<Player_Script>();

        if (player != null)
        {

            player.changeScore(scoreAddtion);
            AudioSource.PlayClipAtPoint(clip, transform.position, 1f);
            //Debug.Break();
            Destroy(gameObject);

        }
    }
}
