using UnityEngine;
using System.Collections;

public class helicontrol : MonoBehaviour {
    Rigidbody mRigidbody;
	// Use this for initialization
	void Start () {
        mRigidbody = GetComponent<Rigidbody>();
	}
    float mThrottle = 1.0f;
	// Update is called once per frame
	void Update () {
        float vert = Input.GetAxis("Vertical") * 20.0f;
        float horiz = Input.GetAxis("Horizontal") * 40.0f;
        float yaw = Input.GetAxis("YawAxis") * 40.0f;
        mThrottle = Mathf.Clamp((Input.GetAxis("Throttle")*1.5f) + 1.0f, 0.0f, 2.0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(-horiz, 0, 0) * Quaternion.Euler(0,-yaw, 0) * Quaternion.Euler(0,0,-vert), Time.deltaTime);
        //Debug.Log("Throttle: " + mThrottle);
        // Add force relative to the orientation to emulate gravity offset, this should be relatively stable flight.
        

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
