using UnityEngine;
using System.Collections;

/// <summary>
/// AngleofAttackRotorBlade script
/// This means the individual blade provides lift based upon its angle of attack and its rotational speed.
/// </summary>
public class AngleOfAttackChopperBlade : MonoBehaviour {

    public Rigidbody mRigidbody;

	// Use this for initialization
	void Start () {
        // Retrieve the chopper rigidbody
        mRigidbody = transform.root.GetComponent<Rigidbody>();
	}

    /// <summary>
    /// Speed
    /// </summary>
    public float mSpeed = 4000.0f;
    public float mAoa = 5.0f;
    public float mBladeLength = 2.0f;
    public float mBladeWidth = 0.25f;
	/// <summary>
    /// FixedUpdate / relational to fixed update.
    /// </summary>
	void FixedUpdate() {
        Debug.Log("AOA: " + mAoa);
        // AOA * SPEED * AREA * MASS
        mRigidbody.AddForce( Vector3.up * ((mAoa * mSpeed * (mBladeLength * mBladeWidth)) * mRigidbody.mass)  );
	}
}
