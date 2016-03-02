using UnityEngine;
using System.Collections;

public class helicontrol : MonoBehaviour {
    Rigidbody mRigidbody;
	// Use this for initialization
	void Start () {
        mRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float vert = Input.GetAxis("Vertical") * 20.0f;
        float horiz = Input.GetAxis("Horizontal") * 20.0f;
        float yaw = Input.GetAxis("YawAxis") * 10.0f;
        float throttle = Mathf.Clamp(Input.GetAxis("Throttle"), 0.5f, 0.9f);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(-horiz, 0, 0) * Quaternion.Euler(0,-yaw, 0) * Quaternion.Euler(0,0,-vert), Time.deltaTime);
        Debug.Log("Throttle: " + throttle);
        // Add force relative to the orientation to emulate gravity offset, this should be relatively stable flight.
        mRigidbody.AddForce((transform.rotation * Vector3.up) * (mRigidbody.mass * Physics.gravity.magnitude) * throttle );

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

    void FixedUpdate()
    {

    }
}
