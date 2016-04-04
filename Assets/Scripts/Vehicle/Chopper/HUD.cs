using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
        layermask = LayerMask.NameToLayer("Ground");
        mRigidbody = GetComponent<Rigidbody>();
	}

    /// <summary>
    /// Altitude UI Element
    /// </summary>
    public Text mAltimeter;
    /// <summary>
    /// Speed / Vel UI Element
    /// </summary>
    public Text mSpeed;

    /// <summary>
    /// Heading UI Element
    /// </summary>
    public Text mHDG;

    /// <summary>
    /// Layermask for ground.
    /// </summary>
    int layermask;

    /// <summary>
    /// The rigidbody
    /// </summary>
    private Rigidbody mRigidbody;

	// Update is called once per frame
	void FixedUpdate () {
        // Check our altitude using a raycast this doesn't use sea level.
        RaycastHit ray;
	    if(Physics.Raycast(transform.position,Vector3.down, out ray, 10000.0f, ~layermask, QueryTriggerInteraction.Collide))
        {
            // Output our altitude
            mAltimeter.text = "Alt: " + ray.distance.ToString("0.0") + " m";
        }

        // Check our speed
        mSpeed.text = "Vel: " + mRigidbody.velocity.magnitude.ToString("0.0") + " m/s";

        // Check our heading
        mHDG.text = "HDG: " + transform.rotation.eulerAngles.y.ToString("000") + " deg";


    }
}
