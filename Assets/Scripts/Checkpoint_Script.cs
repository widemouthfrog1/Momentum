using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    //if a checkpoint is activated, the player will teleport to this checkpoint when they die/make a mistake
    private bool activated;
    private bool activatedBefore;
    
    void Start()
    {
        activated = false;
        activatedBefore = false;
    }
    
    void Update()
    {
        //if this checkpoint is touching the player, attempt to activate this checkpoint.
        if (TouchingPlayer())
        {
            Activate();
        }
    }

    //Sets all other checkpoints in inactive and sets this checkpoint to active. Does nothing if this checkpoint has ever been activated since the start of the level.
    private void Activate()
    {
        //This can be removed for non-linear checkpoints.
        if (activatedBefore) {
            return;
        }

        foreach (Transform child in transform.parent)
        {
            child.gameObject.GetComponent<Checkpoint_Script>().activated = false;
        }
        activated = true;
        activatedBefore = true;
    }

    //Returns true if the player's active collider is touching this checkpoint.
    private bool TouchingPlayer()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        BoxCollider2D playerBoxCollider = player.GetComponent<BoxCollider2D>();
        CircleCollider2D playerCircleCollider = player.GetComponent<CircleCollider2D>();
        if (playerCircleCollider.enabled)
        {
            return collider.IsTouching(playerCircleCollider);
        }
        else
        {
            return collider.IsTouching(playerBoxCollider);
        }
    }

    //Returns true if this checkpoint is currently active
    public bool IsActive() {
        return activated;
    }
}
