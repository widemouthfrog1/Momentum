using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_Segment_Script : MonoBehaviour
{

    private float wallWidth;
    private float wallHeight;
    private float wallXPos;
    private float wallYPos;
    private float buffer = 0.3f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.DrawLine(new Vector3(wallXPos - buffer, wallYPos - buffer, 0), new Vector3(wallXPos + wallWidth + buffer, wallYPos + wallHeight + buffer, 0), Color.green, 100, false);
        if(transform.position.x < wallXPos - buffer || transform.position.x > wallXPos + wallWidth + buffer || transform.position.y < wallYPos - buffer || transform.position.y > wallYPos + wallHeight + buffer)
        {
            gameObject.layer = 10; //Layer 10 is Non-Interactable, meaning the player can't touch it
        }
    }

    public void SetWallWidth(float wallWidth)
    {
        this.wallWidth = wallWidth;
    }

    public void SetWallHeight(float wallHeight)
    {
        this.wallHeight = wallHeight;
    }

    public void SetWallXPos(float wallXPos)
    {
        this.wallXPos = wallXPos;
    }

    public void SetWallYPos(float wallYPos)
    {
        this.wallYPos = wallYPos;
    }
}
