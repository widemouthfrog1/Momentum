using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer_Script : MonoBehaviour
{
    public GameObject starter;
    public GameObject stopper;

    private bool timerActive = false;
    private float time = 0;
    private int minutes = 0;
    private int seconds = 0;
    private int milliseconds = 0;
    // Start is called before the first frame update
    void Start()
    {
        Text textBox = GetComponent<Text>();
        textBox.text = minutes + ":" + seconds + ":" + milliseconds;
    }

    // Update is called once per frame
    void Update()
    {
        Checkpoint_Script starterScript = starter.GetComponent<Checkpoint_Script>();
        Checkpoint_Script stopperScript = stopper.GetComponent<Checkpoint_Script>();
        if (starterScript.activated)
        {
            timerActive = true;
        }
        if (stopperScript.activated)
        {
            timerActive = false;
        }
        Text textBox = GetComponent<Text>();
        if (timerActive)
        {
            time += Time.deltaTime;
            if (time > 60)
            {
                time -= 60;
                minutes += 1;
            }
            seconds = (int)time;
            milliseconds = (int)((time - seconds) * 1000);
            textBox.text = minutes + ":" + seconds + ":" + milliseconds;
        }
    }
}
