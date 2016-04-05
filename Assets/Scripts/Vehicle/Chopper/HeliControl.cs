﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HeliControl : MonoBehaviour {
    /// <summary>
    /// Local rigidbody
    /// </summary>
    Rigidbody mRigidbody;

    /// <summary>
    /// The slider on the UI for the throttle.
    /// </summary>
    public Scrollbar mThrottleSlider;

	// Use this for initialization
	void Start () {
        mRigidbody = GetComponent<Rigidbody>();
	}
    float mThrottle = 1.0f;

    /// <summary>
    /// Acceleration / Control middling.
    /// </summary>
    float accumulatedPanningX;
    float accumulatedPanningY;

    /// <summary>
    /// Pitch value used for QuaternionMath.CalculateInLimits
    /// </summary>
    private float pitchQ = 0.0f;
    /// <summary>
    /// Yaw value used for QuaternionMath.CalculateInLimits.
    /// </summary>
    private float yawQ = 0.0f;


    /// <summary>
    /// Format: PYR - this adjusts the sensitivity of the controls
    /// </summary>
    public Vector3 mControlMultiplier = new Vector3(2.5f, 2.5f, 2.5f);

    /// <summary>
    /// Update / Unity Callback.
    /// </summary>
    void Update () {

        // Pitch accumulator and axis handling from inputs.
        float vert = Input.GetAxis("Vertical") * mControlMultiplier.x;
        accumulatedPanningY = vert; // * Time.deltaTime;        

        // Yaw accumulator and axis handling from inputs.
        float yaw = Input.GetAxis("RollControl") * mControlMultiplier.y;
        accumulatedPanningX = yaw; // * Time.deltaTime;

        // Roll handling
        float horiz = Input.GetAxis("Horizontal") * mControlMultiplier.z;

        // Throttle
        float throttle = Input.GetAxis("Throttle");
       // Console.Log("Vert: " + vert);
       
        // Throttle clamp
        mThrottle = Mathf.Clamp((throttle * 1.5f) + 1.0f, 0.0f, 2.0f);

        // Transform rotation
        // transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * (Quaternion.Euler(-horiz, 0, 0) * Quaternion.Euler(0, -accumulatedPanningX, 0) * Quaternion.Euler(0,0,accumulatedPanningY)), Time.deltaTime);

        // Pitch Angle - limited due to the way chopper's work.
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0,0, accumulatedPanningY * 10.0f), Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(accumulatedPanningX * 10.0f, 0.0f,0.0f), Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0.0f, horiz * 10.0f, 0.0f), Time.deltaTime);
        // Add force relative to the orientation to emulate gravity offset, this should be relatively stable flight.
        mThrottleSlider.size = (throttle +1) * 0.5f ;

        /*if(transform.position.y <= 24.0f)
        {
            mRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            transform.position = transform.position + Vector3.up * Time.deltaTime;
        }
        else
        {
            mRigidbody.constraints = RigidbodyConstraints.None;
        }*/
    }

    /// <summary>
    /// This helps stablize the flight of the objects.
    /// </summary>
    public float mMaxVelocity = 100.0f;

    /// <summary>
    /// Fixed update / Unity Callback.
    /// </summary>
    void FixedUpdate()
    {
        // Force
        mRigidbody.AddForce((transform.up * mThrottle) * mRigidbody.mass * Physics.gravity.magnitude);
        
        /*// Stabilize velocity
        if( mRigidbody.velocity.magnitude > mMaxVelocity)
        {
            mRigidbody.velocity = mRigidbody.velocity - mRigidbody.velocity * Time.deltaTime;
            Console.Log("Clamping velocity");            
        }*/
    }
    
}
