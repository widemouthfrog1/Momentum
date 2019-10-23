using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject checkpoints = null;

    [SerializeField]
    private GameObject pistons = null;

    [SerializeField]
    private GameObject player = null;

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

    //teleports the player to the last activated checkpoint
    private void kill()
    {
        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();
        GameObject checkpoint = null;
        foreach (Transform child in checkpoints.transform)
        {
            if (child.gameObject.GetComponent<Checkpoint_Script>().isActive())
            {
                checkpoint = child.gameObject;
            }
        }
        playerRigidbody.angularVelocity = 0;
        playerRigidbody.velocity = Vector2.zero;
        player.transform.position = new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.y);
        Debug.Log(checkpoint.transform.position);
        foreach (Transform child in pistons.transform)
        {
            Rigidbody2D childRigidbody = child.gameObject.GetComponent<Rigidbody2D>();
            childRigidbody.angularVelocity = 0;
            childRigidbody.velocity = Vector2.zero;
            child.position = new Vector2(checkpoint.transform.position.x, checkpoint.transform.position.y);
        }
    }
}
