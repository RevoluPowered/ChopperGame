using UnityEngine;
using System.Collections;

public class MachineGun : MonoBehaviour {

    public AudioSource mGunStartSound;
    public AudioSource mGunFireSound;
    public ParticleSystem mGunParticleSystem;
    public rotortest mGunBarrelRotator;
	// Use this for initialization
	void Start() {
        
	}
    bool mStartingFireSequence = false;
    float mGunStartTime = 3.0f;
    float mGunStartCounter = 0.0f;
    bool mFiringWeapon = false;
    // Update is called once per frame
    void Update () {
        bool Fire = Input.GetButton("Fire1");
        if(Fire)
        {
            if (!mStartingFireSequence)
            {
                mGunStartSound.Play();
                mStartingFireSequence = true;
                mGunBarrelRotator.speed = 5.0f;
            }
            mGunStartCounter += Time.deltaTime;
            mGunBarrelRotator.speed = Mathf.Clamp(mGunBarrelRotator.speed + (1.0f * Time.deltaTime), 0.0f, 100.0f);

            if(mGunStartCounter > mGunStartTime && !mFiringWeapon)
            {
                mGunFireSound.Play();
                mGunParticleSystem.Play();
                mFiringWeapon = true;
            }
        }
        else
        {
            mStartingFireSequence = false;
            mGunFireSound.Stop();
            mGunParticleSystem.Stop(true);
            mGunStartCounter = 0.0f;
            mFiringWeapon = false;
            mGunBarrelRotator.speed = Mathf.Clamp(mGunBarrelRotator.speed - (1.0f * Time.deltaTime), 0.0f, 100.0f);
        }
	}

}
