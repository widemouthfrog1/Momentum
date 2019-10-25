using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

enum PLAYER_MODE {SQUARE, CIRCLE}

public class Player_Script : MonoBehaviour
{

    // The Sprites for the different shapes
    [SerializeField]
    private Sprite squareSprite = null, circleSprite = null;

    //The pistons this player is attached to
    [SerializeField]
    private GameObject pistons = null;


    //Circle or square
    [SerializeField]
    private PLAYER_MODE mode;

    //The input of the player, what direction they want to roll in
    private float angularAcceleration;

    public bool wasCircleLastTick = false;

    // For the speed platforms
    private bool overSpeedPlatform = false;
    private float velocityMultiplier = 1f;
    [SerializeField]
    private float maxVelocity = 18f; // Set max velocity so the player doesn't go too fast

    // For collectables
    private int score = 0;

    private int frames = 0;
    
    void Start()
    {
        //defaults to square when player is created
        mode = PLAYER_MODE.CIRCLE;
        angularAcceleration = 0;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        HandleControls();
    }

    //FixedUpdate is called once every physics calculation
    void FixedUpdate()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        frames++;
        UpdateSprite();
        UpdateColliders();
        if(overSpeedPlatform)
            ChangeVelocity();
        if (wasCircleLastTick)
        {
            
            
            wasCircleLastTick = false;
        }
        //Debug.Log(rigidBody.rotation);
        

        rigidBody.AddTorque(angularAcceleration);
    }

    /**
     * Changes the mode of the player and the angular acceleration based on player input
     */
    private void HandleControls()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        if (Input.GetButtonDown("Transform"))
        {
            if (frames == 1)
            {
                return;
            }
            frames = 0;
           
            if(mode == PLAYER_MODE.SQUARE)
            {
                bool allPistonsRetracted = true;
                foreach (Transform child in pistons.transform)
                {
                    if (child.gameObject.GetComponent<Piston_Script>().IsExtended())
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
                foreach (Transform child in pistons.transform)
                {
                    if(child.gameObject.name.Equals("Left"))
                    {
                        child.gameObject.GetComponent<Rigidbody2D>().rotation = -90;
                    }
                    if (child.gameObject.name.Equals("Right"))
                    {
                        child.gameObject.GetComponent<Rigidbody2D>().rotation = 90;
                    }
                    if (child.gameObject.name.Equals("Up"))
                    {
                        child.gameObject.GetComponent<Rigidbody2D>().rotation = 180;
                    }
                    if (child.gameObject.name.Equals("Down"))
                    {
                        child.gameObject.GetComponent<Rigidbody2D>().rotation = 0;
                    }
                    child.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
                }
                rigidBody.angularVelocity = 0;
                rigidBody.rotation = 0;
                wasCircleLastTick = true;
                
                mode = PLAYER_MODE.SQUARE;
            }
            
        }

        angularAcceleration = -Input.GetAxis("Horizontal");     
    }

    /**
     * Changes the sprite of the player based on the mode
     */
    private void UpdateSprite()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (mode == PLAYER_MODE.SQUARE)
        {
            if (!spriteRenderer.sprite.Equals(squareSprite))
            {
                spriteRenderer.sprite = squareSprite;
            }
        }
        if (mode == PLAYER_MODE.CIRCLE)
        {
            if (!spriteRenderer.sprite.Equals(circleSprite))
            {
                spriteRenderer.sprite = circleSprite;
            }
        }
    }

    /**
     * Changes the collider of the player based on the mode
     */
    private void UpdateColliders()
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

    /**
     * Handles the input from going over a speed platform
     * 
     * sw is a binary switch, 1: player has entered a speed platform
     *                        0: player has exited a speed platform
     *                        
     * velMlt is the multiplier aplied to the players velocity (Optional argument)
     */
    public void SpeedPaltform(int sw, float velMlt = 1)
    {

        if (sw == 1) // If player on a speed platform
        {
            overSpeedPlatform = true;
            velocityMultiplier = velMlt;
        }
        else // If player is exiting a speed platform reset everything
        {
            overSpeedPlatform = false;
            velocityMultiplier = 1f;
        }

    }

    /**
     * Get the players current score
     */
    public int GetScore() { return score; }

    /**
     * Updates the players score
     * Can incress or decress score 
     * Not negitive scores
     * 
     * change is the amount to change the score by
     */
     public void ChangeScore(int change)
    {
        int newScore = score + change;

        if (newScore < 0) // Score can not be less than zero
            score = 0;
        else
            score = newScore;

        //Debug.Log("Player score: " + score);
    }

    /**
     * Changes the velocity on the player
     */
     private void ChangeVelocity()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        // Change player velocity base on input from platform
        Vector3 v = rigidBody.velocity;
        v.x *= velocityMultiplier;     
        if (v.x < maxVelocity)
            rigidBody.velocity = v;
        else
        {
            v.x = maxVelocity;
            rigidBody.velocity = v;
        }
    }
}
