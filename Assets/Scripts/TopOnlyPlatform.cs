using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopOnlyPlatform : MonoBehaviour
{

    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private GameObject pistons = null;

    private BoxCollider2D platform = null;

    private float platformTop = 0;      // Location of the top of the platform

    // Start is called before the first frame update
    void Start()
    {
        platform = GetComponent<BoxCollider2D>();
        platform.enabled = false;
        platformTop = transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y / 2 - 0.15f;       // the 0.15 is gives leeway if the calculation is slightly inaccurate
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        BoxCollider2D playerBox = player.gameObject.GetComponent<BoxCollider2D>();
        CircleCollider2D playerCircle = player.gameObject.GetComponent<CircleCollider2D>();
        Rigidbody2D playerRigid = player.gameObject.GetComponent<Rigidbody2D>();

        if (playerBox.enabled)
        {
            if (playerRigid.position.y - boxBottom() > platformTop)     // checks if the bottom of the box is higher than the top of the platform, with some leeway
            {
                // Do nothing if the platform has already been activated
                if (platform.enabled)
                {
                    return;
                }
                foreach (Transform child in pistons.transform)
                {
                    // All pistons must be retracted before the platform activates
                    if (!child.gameObject.GetComponent<Piston_Script>().isRetracted())
                    {
                        platform.enabled = false;
                        return;
                    }
                }
                platform.enabled = true;
            }
            else
            {
                platform.enabled = false;
            }
        }
        else if (playerCircle.enabled)
        {
            if (playerRigid.position.y - player.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2 > platformTop)
            {
                platform.enabled = true;
            }
            else
            {
                platform.enabled = false;
            }
        }
    }

    // Will return where the bottom of the square is relative to its center of rotation
    private float boxBottom()
    {
        float rotation = Mathf.Abs(player.gameObject.GetComponent<Rigidbody2D>().rotation);
        while (rotation >= 45)
        {
            rotation = rotation - 45;
        }
        // approximates any angle close to 45 degrees as 45
        if (Mathf.Abs(45 - rotation) < 0.5)
        {
            rotation = 0;
        }
        return (player.gameObject.GetComponent<SpriteRenderer>().bounds.size.y / 2) / Mathf.Abs(Mathf.Cos(rotation * Mathf.Deg2Rad));
    }
}
