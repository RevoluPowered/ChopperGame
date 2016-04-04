using UnityEngine;
using System.Collections;

/// <summary>
/// Rotor test script
/// </summary>
public class rotortest : MonoBehaviour {
    public Vector3 RotationAxis = Vector3.zero;
    public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate((RotationAxis * speed) * Time.deltaTime);
	}
}
