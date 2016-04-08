using UnityEngine;
using System.Collections;

/// <summary>
/// Trying out a new coding style.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class Drag : MonoBehaviour {

    /// <summary>
    /// Is our dynamic drag calculation system enabled?
    /// </summary>
    [Header("Drag Equation")]
    public bool DynamicDragEnabled = false;
    /// <summary>
    /// The Drag Coefficient
    /// </summary>
    public float Coefficient = 0.045f; // Cd Drag Coefficient
    /// <summary>
    /// The gas density
    /// </summary>
    public float GasDensity = 1.225f; // D (Density of Air / Gas you're flying in ) 1.225 kg/m3 @ 20 *(C)
    /// <summary>
    /// The area of the wing
    /// </summary>
    public float AreaOfWing = 3 * 2; // Example value until proper value is found.

    
    /// <summary>
    /// Member rigidbody reference to original.
    /// </summary>
    private Rigidbody mRigidbody;
	
    /// <summary>
    /// Unity Start / Callback
    /// </summary>
	void Start () {
        // set up rigidbody
        mRigidbody = GetComponent<Rigidbody>();
        // constant drag
        mRigidbody.drag = 0.16666655f; 

	}
	
	/// <summary>
    /// Unity FixedUpdate / Callback
    /// </summary>
	void FixedUpdate () {
        if (!DynamicDragEnabled) return;
        // Calculate our magnitude of our chopper. squared.
        float magnitude = Mathf.Pow(mRigidbody.velocity.magnitude, 2);

        // Drag Equation = Drag = Co d (V2/2) A
        mRigidbody.drag = Coefficient * GasDensity * (magnitude / 2) * AreaOfWing;

        // Drag value
        Debug.Log("Drag: " + mRigidbody.drag);
	}
}
