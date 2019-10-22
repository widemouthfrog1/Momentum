using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    public GameObject player;        //Public variable to store a reference to the player game object


    private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -40);

    }
}