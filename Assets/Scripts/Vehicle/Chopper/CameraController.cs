using UnityEngine;
using System.Collections;

/// <summary>
/// A camera controller for the helicopter
/// </summary>
public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    /// <summary>
    /// A multiplier for the camera rotation speed for the horizontal axis.
    /// </summary>
    public float horizontalRotationSpeed = 10.0f;

    /// <summary>
    /// A multiplier for the camera rotation speed for the vertical axis.
    /// </summary>
    public float verticalRotationSpeed = 10.0f;

	// Update is called once per frame
	void Update () {
        // The input handler for the chopper
        float horizontalAxis = Input.GetAxis("ViewHorizontal");
        float verticalAxis = Input.GetAxis("ViewVertical");

        // Debug the output of these controls.
        //Console.Log("Horizontal: " + horizontalAxis);
        //Console.Log("Verticald: " + verticalAxis);

        // Rotate around the horizontal camera axis. (node must be centered, camera must be off center from node for this to work)
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0, horizontalAxis * horizontalRotationSpeed, 0),Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * Quaternion.Euler(0,0,verticalAxis * horizontalRotationSpeed), Time.deltaTime);
    }
}
