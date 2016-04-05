using UnityEngine;
using System.Collections;

/// <summary>
/// The rotation script used to rotate the Mini-Gun.
/// </summary>
public class GunRotator : MonoBehaviour {
    /// <summary>
    /// What is the rotation axis?
    /// </summary>
    public Vector3 mRotationAxis = Vector3.zero;

    /// <summary>
    /// Specify the RPM of the Rotor.
    /// </summary>
    public float mSpeed = 4000.0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate((mRotationAxis * mSpeed) * Time.deltaTime);
    }
}
