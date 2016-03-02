using UnityEngine;
using System.Collections;

public class DamageHandler : MonoBehaviour {
    public AudioSource mExplosion;
    bool mDead = false;
    public rotortest mRotor;
    public ConstantForce mConstantForce;
    void OnCollisionEnter(Collision col)
    {
        if (mDead) return;
        // Destroy(gameObject);
        mExplosion.Play();
        mRotor.speed = 0.0f;
        mDead = true;
        Destroy(mConstantForce);
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
