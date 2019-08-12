using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Script : MonoBehaviour
{
    public GameObject checkpoints;
    public GameObject pistons;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        BoxCollider2D playerBoxCollider = player.GetComponent<BoxCollider2D>();
        CircleCollider2D playerCircleCollider = player.GetComponent<CircleCollider2D>();
        if (playerCircleCollider.enabled)
        {
            if (collider.IsTouching(playerCircleCollider))
            {
                kill();
            }
        }
        else
        {
            if (collider.IsTouching(playerBoxCollider))
            {
                kill();
            }
        }
    }

    private void kill()
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        GameObject checkpoint = null;
        foreach (Transform child in checkpoints.transform)
        {
            if (child.gameObject.GetComponent<Checkpoint_Script>().activated)
            {
                checkpoint = child.gameObject;
            }
        }
        player.transform.position = new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.y);
        playerRigidbody.angularVelocity = 0;
        playerRigidbody.velocity = new Vector2(0, 0);
        foreach (Transform child in pistons.transform)
        {
            child.position = new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.y);
        }
    }
}
