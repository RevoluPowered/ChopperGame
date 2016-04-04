using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class HeliControl : MonoBehaviour {
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
    /// Update / Unity Callback.
    /// </summary>
	void Update () {

        // Pitch accumulator and axis handling from inputs.
        float vert = Input.GetAxis("Mouse Y");
        accumulatedPanningY += vert; // Append val on axis.        

        // Yaw accumulator and axis handling from inputs.
        float yaw = -Input.GetAxis("Mouse X");
        accumulatedPanningX += yaw;

        // Roll handling
        float horiz = Input.GetAxis("Horizontal") * 40.0f;

        // Throttle
        float throttle = Input.GetAxis("Throttle");
        Debug.Log("Vert: " + vert);
       
        // Throttle clamp
        mThrottle = Mathf.Clamp((throttle * 1.5f) + 1.0f, 0.0f, 2.0f);
        
        // Transform rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(-horiz, 0, 0) * Quaternion.Euler(0, -accumulatedPanningX, 0) * Quaternion.Euler(0,0,accumulatedPanningY), Time.deltaTime);
        
        // Add force relative to the orientation to emulate gravity offset, this should be relatively stable flight.
        mThrottleSlider.size = (throttle +1) * 0.5f ;

        if(transform.position.y <= 24.0f)
        {
            mRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
            transform.position = transform.position + Vector3.up * Time.deltaTime;
        }
        else
        {
            mRigidbody.constraints = RigidbodyConstraints.None;
        }
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
        mRigidbody.AddForce((transform.up * mThrottle) * mRigidbody.mass * Physics.gravity.magnitude);
        
        // Stabilize velocity
        if( mRigidbody.velocity.magnitude > mMaxVelocity)
        {
            mRigidbody.velocity = mRigidbody.velocity - (mRigidbody.velocity * Time.deltaTime);
            Debug.Log("Clamping velocity");
            
        }
    }
    
}
