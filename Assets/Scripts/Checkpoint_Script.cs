using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Script : MonoBehaviour
{
    public GameObject player;
    public bool activated;
    
    // Start is called before the first frame update
    void Start()
    {
        activated = false;
    }

    // Update is called once per frame
    void Update()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        BoxCollider2D playerBoxCollider = player.GetComponent<BoxCollider2D>();
        CircleCollider2D playerCircleCollider = player.GetComponent<CircleCollider2D>();
        if (playerCircleCollider.enabled)
        {
            if (collider.IsTouching(playerCircleCollider) && !activated) //"!activated" is unnecessary but will improve performance for a very large number of checkpoints
            {
                activate();
            }
        }
        else
        {
            if (collider.IsTouching(playerBoxCollider) && !activated)
            {
                activate();
            }
        }
    }

    private void activate()
    {
        foreach (Transform child in transform.parent)
        {
            child.gameObject.GetComponent<Checkpoint_Script>().activated = false;
        }
        activated = true;
    }
}
