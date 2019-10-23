using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    //[SerializeField]
    //private string direction;    //up,down,left,right

    [SerializeField]
    private float extendSpeed = 0, extendForce = 0, retractSpeed = 0, retractForce = 0;

    //Uncomment to test states at runtime
    //[SerialzeField]
    private enum State {RETRACTED, RETRACTING, EXTENDED, EXTENDING}
    private State state;
    private Vector2 centerOfMass;
    private bool buttonHeld;
    private bool wasCircle;
    private float retractionTime;
    private float originalRetractForce;

    // Start is called before the first frame update
    void Start()
    {
        SliderJoint2D slider = GetComponent<SliderJoint2D>();
        state = State.RETRACTED;
        buttonHeld = false;
        //set limits to zero so pistons don't randomy slide out
        JointTranslationLimits2D limits = new JointTranslationLimits2D();
        limits.max = 0;
        limits.min = 0;
        slider.limits = limits;
        slider.useMotor = true;
        slider.enabled = true;
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        centerOfMass = rigidbody.centerOfMass;
        wasCircle = false;
        retractionTime = 0;
        originalRetractForce = retractForce;
    }

    // Update is called once per frame
    void Update()
    {
        handleControls();
    }

    void FixedUpdate()
    {
        BoxCollider2D pistonCollider = GetComponent<BoxCollider2D>();
        SliderJoint2D slider = GetComponent<SliderJoint2D>();
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();


        CircleCollider2D playerCircleCollider = player.GetComponent<CircleCollider2D>();
        if (!playerCircleCollider.enabled)
        {
            if (wasCircle)
            {
                rigidbody.gravityScale = 1.0f;
                rigidbody.position = player.GetComponent<Rigidbody2D>().position;
                pistonCollider.enabled = true;
                slider.enabled = true;
                spriteRenderer.enabled = true;
                wasCircle = false;

                /*
                 Change pistons' rigidbodies to be spinning at same speed as player
                */

                rigidbody.angularVelocity = 0;
                rigidbody.velocity = player.GetComponent<Rigidbody2D>().velocity;
            }
            
            //Finite State Machine:
            if (state == State.RETRACTED)
            {
                if (buttonHeld)
                {
                    JointTranslationLimits2D limits = new JointTranslationLimits2D();
                    limits.max = 0.34f;
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
                if (slider.jointTranslation >= 0.34f) //assuming 0.35 is max
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
                // TODO: make piston retraction speed stronger as time goes on

                if (slider.jointTranslation <= 0) //assuming 0 is min
                {
                    JointTranslationLimits2D limits = new JointTranslationLimits2D();
                    limits.max = 0;
                    limits.min = 0;
                    slider.limits = limits;
                    retractionTime = 0;
                    retractForce = originalRetractForce;
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
        else
        {
            rigidbody.gravityScale = 0.0f;
            pistonCollider.enabled = false;
            slider.enabled = false;
            spriteRenderer.enabled = false;
            wasCircle = true;
        }
    }

    private void handleControls()
    {
        if (Input.GetButtonDown("Jump"))
        {
            buttonHeld = true;
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
        retractionTime += Time.deltaTime;
        if(retractionTime > 0.01)
        {
            retractForce *= 2;
        }
        SliderJoint2D slider = GetComponent<SliderJoint2D>();
        JointMotor2D motor = new JointMotor2D();
        motor.maxMotorTorque = retractForce;
        motor.motorSpeed = -retractSpeed;//needs to be negative as going in other direction
        slider.motor = motor;
    }

    /** 
     * returns true if this piston is fully or partially extended 
     */
    public bool isExtended()
    {
        return state == State.EXTENDING || state == State.EXTENDED;
    }

    // Returns true if the piston is retracted
    public bool isRetracted()
    {
        return state == State.RETRACTED;
    }
}
