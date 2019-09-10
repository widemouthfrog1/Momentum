using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PLAYER_MODE {SQUARE, CIRCLE}

public class Player_Script : MonoBehaviour
{
    //public variables
    public Sprite squareSprite;
    public Color squareColour;
    public Sprite circleSprite;
    public Color circleColour;

    //The pistons this player is attached to
    public GameObject pistons;

    //private variables

    //Circle or square
    private PLAYER_MODE mode;

    //The input of the player, what direction they want to roll in
    private float angularAcceleration; 

    

    // Start is called before the first frame update
    void Start()
    {
        initialiseVariables();
    }


    // Update is called once per frame
    void Update()
    {

    }

    //FixedUpdate is called once every physics calculation
    void FixedUpdate()
    {
        handleControls();
        updateSprite();
        updateColliders();

        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddTorque(angularAcceleration);
    }

    /**
     * Changes the mode of the player and the angular acceleration based on player input
     */
    private void handleControls()
    {
        if (Input.GetButtonDown("Transform"))
        {
            if(mode == PLAYER_MODE.SQUARE)
            {
                bool allPistonsRetracted = true;
                foreach (Transform child in pistons.transform)
                {
                    if (child.gameObject.GetComponent<Piston_Script>().extended)
                    {
                        allPistonsRetracted = false;
                    }
                }
                if (allPistonsRetracted)
                {
                    mode = PLAYER_MODE.CIRCLE;
                }
            }
            else
            {
                mode = PLAYER_MODE.SQUARE;
            }
            
        }
        angularAcceleration = -Input.GetAxis("Horizontal");
    }

    /**
     * Changes the sprite of the player based on the mode
     */
    private void updateSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (mode == PLAYER_MODE.SQUARE)
        {
            if (!spriteRenderer.sprite.Equals(squareSprite))
            {
                spriteRenderer.sprite = squareSprite;
                spriteRenderer.color = squareColour;
            }
        }
        if (mode == PLAYER_MODE.CIRCLE)
        {
            if (!spriteRenderer.sprite.Equals(circleSprite))
            {
                spriteRenderer.sprite = circleSprite;
                spriteRenderer.color = circleColour;
            }
        }
    }

    /**
     * Changes the collider of the player based on the mode
     */
    private void updateColliders()
    {
        BoxCollider2D boxCollider = GetComponent<BoxCollider2D>();
        CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
        PolygonCollider2D polygonCollider = GetComponent<PolygonCollider2D>();
        if (mode == PLAYER_MODE.SQUARE)
        {
            if (boxCollider.enabled == false)
            {
                boxCollider.enabled = true;
            }
            circleCollider.enabled = false;
        }
        if (mode == PLAYER_MODE.CIRCLE)
        {
            if (circleCollider.enabled == false)
            {
                circleCollider.enabled = true;
            }
            boxCollider.enabled = false;
        }
    }

    private void initialiseVariables()
    {
        //defaults to square when player is created
        mode = PLAYER_MODE.SQUARE;
        angularAcceleration = 0;
    }
}
