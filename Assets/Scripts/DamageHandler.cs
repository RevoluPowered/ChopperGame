using UnityEngine;
using System.Collections;

[System.Obsolete("No longer in use, Damage Base replaces this script!",true)]
public class DamageHandler : MonoBehaviour {
    public AudioSource mExplosion;
    bool mDead = false;
    public ChopperRotor mRotor;
    void OnCollisionEnter(Collision col)
    {
        if (mDead) return;
        // Destroy(gameObject);
        mExplosion.Play();
        mRotor.mSpeed = 0.0f;
        mDead = true;
    }
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
