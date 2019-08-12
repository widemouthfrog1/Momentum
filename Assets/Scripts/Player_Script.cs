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

    public GameObject piston1;

    //private variables
    private PLAYER_MODE mode;


    private float rotation;

    

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
        rigidBody.AddTorque(rotation);
    }

    private void handleControls()
    {
        if (Input.GetButtonDown("Transform"))
        {
            if(mode == PLAYER_MODE.SQUARE)
            {
                if (!piston1.GetComponent<Piston_Script>().extended)
                {
                    mode = PLAYER_MODE.CIRCLE;
                }
            }
            else
            {
                mode = PLAYER_MODE.SQUARE;
            }
            
        }
        rotation = -Input.GetAxis("Horizontal");
    }

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
        rotation = 0;
    }
}
