using UnityEngine;
using System.Collections;

public class DamageHandler : MonoBehaviour {
    public AudioSource mExplosion;
    bool mDead = false;
    public rotortest mRotor;
    void OnCollisionEnter(Collision col)
    {
        if (mDead) return;
        // Destroy(gameObject);
        mExplosion.Play();
        mRotor.speed = 0.0f;
        mDead = true;
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
