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
    private PLAYER_MODE mode;

    //The input of the player, what direction they want to roll in
    private float angularAcceleration;
    private float horizontalVelocity;

    private Tilemap tilemap;
    public GameObject tilemapGameObject;

    void Start()
    {
        //defaults to square when player is created
        mode = PLAYER_MODE.SQUARE;
        angularAcceleration = 0;
        tilemap = tilemapGameObject.GetComponent<Tilemap>();
    }

    //FixedUpdate is called once every physics calculation
    void FixedUpdate()
    {
        handleControls();
        updateSprite();
        updateColliders();

        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.AddTorque(angularAcceleration);
        horizontalVelocity = ((Vector2)rigidBody.velocity).x;
    }

    /**
     * Changes the mode of the player and the angular acceleration based on player input
     */
    private void handleControls()
    {
        Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

        if (Input.GetButtonDown("Transform"))
        {
            if(mode == PLAYER_MODE.SQUARE)
            {
                bool allPistonsRetracted = true;
                foreach (Transform child in pistons.transform)
                {
                    if (child.gameObject.GetComponent<Piston_Script>().isExtended())
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
                    child.gameObject.GetComponent<Rigidbody2D>().rotation = 0;
                }
                rigidBody.rotation = 0;
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

    void OnCollisionEnter2D(Collision2D collision)
    {

        Vector3 hitPosition = Vector3.zero;
        if (tilemap != null && collision.transform.tag == "Wall" && (horizontalVelocity >= 3 || horizontalVelocity <= -3))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPosition), null);
            }
        }
        /*
        if (col.transform.tag == "Wall" && (horizontalVelocity >= 3 || horizontalVelocity <= -3))
        {
            col.gameObject.GetComponent<Wall>().DamageWall(1);
        }
        */
    }

}
