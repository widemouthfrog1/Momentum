using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston_Script : MonoBehaviour
{
    public GameObject player;
    public string direction;//up,down,left,right
    public float extendSpeed;
    public float extendForce;
    public float retractSpeed;
    public float retractForce;

    public bool extended;//true if partially or fully extended

    private enum State {RETRACTED, RETRACTING, EXTENDED, EXTENDING}
    private State state;

    public bool buttonHeld;
    // Start is called before the first frame update
    void Start()
    {
        SliderJoint2D slider = GetComponent<SliderJoint2D>();
        
        //translate and rotate to correct position
        extended = false;
        state = State.RETRACTED;
        buttonHeld = false;
        //set limits to zero so pistons don't randomy slide out
        JointTranslationLimits2D limits = new JointTranslationLimits2D();
        limits.max = 0;
        limits.min = 0;
        slider.limits = limits;
        slider.useMotor = true;
        slider.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        handleControls();
    }

    void FixedUpdate()
    {
        
        SliderJoint2D slider = GetComponent<SliderJoint2D>();
        slider.enabled = true;
        CircleCollider2D playerCircleCollider = player.GetComponent<CircleCollider2D>();
        //Debug.Log(slider.angle);
        if (!playerCircleCollider.enabled)
        {
            //Finite State Machine:
            if (state == State.RETRACTED)
            {
                if (buttonHeld)
                {
                    JointTranslationLimits2D limits = new JointTranslationLimits2D();
                    limits.max = 0.35f;
                    limits.min = 0;
                    slider.limits = limits;
                    state = State.EXTENDING;
                }
            }
            else if (state == State.EXTENDED)
            {
                if (!buttonHeld)
                {
                    FixedJoint2D joint = GetComponent<FixedJoint2D>();
                    joint.enabled = false;
                    slider.useMotor = true;
                    state = State.RETRACTING;
                }
            }
            else if (state == State.EXTENDING)
            {
                // Debug.Log(slider.jointTranslation);
                if (slider.jointTranslation >= 0.35f) //assuming 0.3 is max
                {
                    FixedJoint2D joint = GetComponent<FixedJoint2D>();
                    joint.enabled = true;
                    state = State.EXTENDED;
                    slider.useMotor = false;
                }
                else if (!buttonHeld)
                {
                    state = State.RETRACTING;
                }
                else
                {
                    extendPiston();
                }
            }
            else if (state == State.RETRACTING)
            {
                if (slider.jointTranslation <= 0) //assuming 0 is min
                {
                    JointTranslationLimits2D limits = new JointTranslationLimits2D();
                    limits.max = 0;
                    limits.min = 0;
                    slider.limits = limits;
                    state = State.RETRACTED;
                }
                else if (buttonHeld)
                {
                    state = State.EXTENDING;
                }
                else
                {
                    retractPiston();
                }
            }
        }
    }

    private void handleControls()
    {
        if (Input.GetButtonDown("Jump"))
        {
            buttonHeld = true;
            extended = true;
        }
        if (Input.GetButtonUp("Jump"))
        {
            buttonHeld = false;
        }
    }

    private void extendPiston()
    {
        SliderJoint2D slider = GetComponent<SliderJoint2D>();
        JointMotor2D motor = new JointMotor2D();
        motor.maxMotorTorque = extendForce;
        motor.motorSpeed = extendSpeed;
        slider.motor = motor;
    }

    private void retractPiston()
    {
        SliderJoint2D slider = GetComponent<SliderJoint2D>();
        JointMotor2D motor = new JointMotor2D();
        motor.maxMotorTorque = retractForce;
        motor.motorSpeed = -retractSpeed;//needs to be negative as going in other direction
        slider.motor = motor;
    }
}
