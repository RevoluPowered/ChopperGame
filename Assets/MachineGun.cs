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


    /// <summary>
    /// This lets you calculate the yaw or pitch of the player, this is mainly used when pitching normally, or when yawing inside a vehicle.
    /// </summary>
    /// <returns>The in limits.</returns>
    /// <param name="rotation">Rotation.</param>
    /// <param name="current">Current.</param>
    /// <param name="movement">Movement.</param>
    /// <param name="useX">If set to <c>true</c> use x.</param>
    /// <param name="min">Minimum.</param>
    /// <param name="max">Max.</param>
    Quaternion CalculateInLimits(Quaternion rotation, ref float current, float movement, bool useX = true, float min = -70, float max = 60)
    {
        // Current movement clamped to the min max, to stop people hitting max movement in a single tick.
        movement = Mathf.Clamp(movement, min, max);
        current += movement;

        // Amount to ad
        float unclamped = current;
        current = Mathf.Clamp(current, min, max);

        // Calculate reduction rate.
        float reductionRate = (unclamped - current);

        Quaternion EulerRot = Quaternion.identity;

        if (useX)
        {
            EulerRot = Quaternion.Euler(movement - reductionRate, 0, 0);
        }
        else
        {
            EulerRot = Quaternion.Euler(0, movement - reductionRate, 0);
        }

        // Rotate the camera on the pitch axis, relative to the players head rotation.
        return Quaternion.Slerp(rotation, rotation * EulerRot, Time.time);
    }


}
