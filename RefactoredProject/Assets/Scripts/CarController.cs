using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController :  StateMachine
{
    public Rigidbody _rigidbody;

    public float acceleration = 6f, backAccel = 4f, maxSpeed = 40f, turnStrength = 160f, gravityForce = 10f, dragOnGround = 3;

    public float speedInput, turnInput;

    private bool grounded;
    [SerializeField] private TrackCheckpoints victoryChecker;

    [SerializeField] private Quaternion baseRotation;
    private Quaternion JumpRotation;
    private bool CanSetRotate = true;

    public LayerMask whatIsGround;
    public float groundRayLength = .5f;
    public Transform groundRayPoint;

    [SerializeField] private Transform leftFrontWheel, rightFrontWheel;
    [SerializeField] private Transform leftBackWheel, rightBackWheel;
    [SerializeField] private float maxWheelTurn = 25f;
    private void Start()
    {
        victoryChecker = victoryChecker.GetComponent<TrackCheckpoints>();
        _rigidbody.transform.parent = null;
        SetState(new NormalState(this));
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SetState(new SlowState(this));
        } 
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            SetState(new NormalState(this));
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            SetState(new FastState(this));
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            SetState(new SuperFastState(this));
        }
        //checks if the game is over yet.
        if (!victoryChecker.Victory)
        {
            speedInput = 0;
            //checks if you have input forward or backwards, and accalerates you towards that direction.
            if (Input.GetAxis("Vertical") > 0)
            {
                StartCoroutine(state.Drive());
            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                StartCoroutine(state.Break());
            }

            turnInput = Input.GetAxis("Horizontal");
            //if the object is grounded, it wwill allow steering to rotate the object into the right direction.
            if (grounded)
            {
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0f));
            }
            //will rotate the frontwheels anr backwheels to give the illusion of turning the car/driving.
            leftFrontWheel.localRotation = Quaternion.Euler(leftFrontWheel.localRotation.eulerAngles.x, (turnInput * maxWheelTurn) - 180, leftFrontWheel.localRotation.eulerAngles.z);
            rightFrontWheel.localRotation = Quaternion.Euler(rightFrontWheel.localRotation.eulerAngles.x, turnInput * maxWheelTurn, rightFrontWheel.localRotation.eulerAngles.z);

            transform.position = _rigidbody.transform.position;
        }
    }

    private void FixedUpdate()
    {
        //checks if the game is won yet.
        if (!victoryChecker.Victory)
        {
            grounded = false;

            RaycastHit hit;
            //checks if the object isnt lifted from the ground.
            if (Physics.Raycast(groundRayPoint.position, -transform.up, out hit, groundRayLength, whatIsGround))
            {
                grounded = true;
                CanSetRotate = true;
                //if the object is lifted from the ground. it will slightly tilt as if you have air time.
                transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            }

            if (grounded)
            {
                _rigidbody.drag = dragOnGround;
                if (Mathf.Abs(speedInput) > 0)
                {
                    _rigidbody.AddForce(transform.forward * speedInput);
                    baseRotation.x = gameObject.transform.rotation.x;
                }
            }
            else
            {
                _rigidbody.drag = 0.1f;

                _rigidbody.AddForce(Vector3.up * -gravityForce * 100);
            }
        }
    }
}
