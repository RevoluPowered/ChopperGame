using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Drag : MonoBehaviour {

    public float 

    private Rigidbody mRigidbody;
	// Use this for initialization
	void Start () {
        mRigidbody = GetComponent<Rigidbody>();
        mRigidbody.drag = 0.15555566f; 

        //

	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
