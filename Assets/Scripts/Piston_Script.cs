using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston_Script : MonoBehaviour
{
    [SerializeField]
    private GameObject player = null;

    [SerializeField]
    private string direction;    //up,down,left,right

    [SerializeField]
    private float extendSpeed = 0, extendForce = 0, retractSpeed = 0, retractForce = 0;

    private bool extended = false;   //true if partially or fully extended
    private enum State {RETRACTED, RETRACTING, EXTENDED, EXTENDING}
    private State state;
    private Vector2 centerOfMass;
    private bool buttonHeld;

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
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        centerOfMass = rigidbody.centerOfMass;
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
        if (!playerCircleCollider.enabled)
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.centerOfMass = new Vector2(centerOfMass.x, centerOfMass.y);

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
        else
        {
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.centerOfMass = new Vector2(0,0);

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
        extended = false;
    }

    //* returns true if all the pistons are extended */
    public bool isExtended()
    {
        return extended;
    }
}
