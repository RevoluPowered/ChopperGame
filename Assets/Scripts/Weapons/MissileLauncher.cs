using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DamageBase))]
public class MissileLauncher : MonoBehaviour {

    /// <summary>
    /// The missle prefab to fire.
    /// </summary>
    public GameObject mMissilePrefab;

    /// <summary>
    /// The place where the missile will spawn.
    /// </summary>
    public GameObject mSpawnNode;

    /// <summary>
    /// Missiles in storage.
    /// </summary>
    public int mMissilesStored = 60;

    /// <summary>
    /// Radius in which we can target the player
    /// </summary>
    public float mTargetRadius = 250.0f;
    
    /// <summary>
    /// Enemy detection sound
    /// </summary>
    public AudioSource mEnemyDetected;

    /// <summary>
    /// Current missiles loaded in the launcher.
    /// </summary>
    public int mMissilesLoaded = 1;

    /// <summary>
    /// Chance the missile will aim to hit, (obviously my algorithm will hit every time otherwise!)
    /// </summary>
    public int mChanceOfHitToOne = 3;

    /// <summary>
    /// The damage base which is attached to the game object.
    /// </summary>
    private DamageBase mDamageBase;

    // Use this for initialization
    void Start () {
        // Get local damage base.
        mDamageBase = GetComponent<DamageBase>();
        
	    if(mMissilePrefab == null)
        {
            Console.Log("[Launcher] Missile prefab not set, destroying launcher.");
            Console.LogError("Error launcher not assigned prefab.");

            Destroy(this);
        }
        else
        {
            // Register to our 'score' counter in the information status ui in the game.
            InformationStatusUI.RegisterDamageBase(mDamageBase);
        }
        
	}
	
	/// <summary>
    /// Unity Update Callback
    /// </summary>
	void FixedUpdate () {

        if(mMissilesLoaded == 0)
        {
            BeginReloading();
        }

        if (mReloading)
        {
            if(mMissilesStored == 0)
            {
                Console.Log("[Launcher] Missiles spent from launcher.");
                Destroy(this);
            }


            mReloadCounter += Time.deltaTime;
            if(mReloadCounter >= mReloadTime)
            {
                Console.Log("[Launcher] Reloading completed!");
                mReloadCounter = 0.0f;
                mMissilesLoaded = 1;
                mMissilesStored -= 1;
                // Deduct missiles used in reloading.
                mMissilesStored -= mMissilesLoaded;

                mReloading = false;
            }
        }
        else
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag(mTargetTag);

            foreach (GameObject ply in players)
            {
                if (Vector3.Distance(transform.position, ply.transform.position) <= mTargetRadius)
                {
                    Console.Log("[Launcher] Firing, target found!");
                    // We have a valid target spawn missile.
                    FireMissile(ply);
                    if (!mEnemyDetected.isPlaying)
                    {
                        mEnemyDetected.Play();
                    }
                    // play lock sound on player .
                }
            }
        }
        
            
	}

    /// <summary>
    /// The tag which the launcher shoots at
    /// </summary>
    public string mTargetTag = "Player";

   
    /// <summary>
    /// Fire the missile at the target - uses hit / miss target logic based on probability.
    /// </summary>
    /// <param name="target"></param>
    void FireMissile( GameObject target )
    {
        // Have we ran out of missiles? Okay, Don't fire! 
        if (mMissilesLoaded <= 0) return;
        Console.Log("[Missile] Firing sequence!");
        // Instantiate missile for firing sequence
        GameObject missile = Instantiate<GameObject>(mMissilePrefab);
        
        // Spawn missile at the launcher node location
        missile.transform.position = mSpawnNode.transform.position;

        // Retrieve the missile controller.
        Missile missilecontroller = missile.GetComponent<Missile>();

        // Assign the target to the missile.
        missilecontroller.SetMissileTarget(target);
       
        // If we're shooting to hit.
        if (Random.Range(0,mChanceOfHitToOne) != 1)
        {
            Console.Log("[Missile] Chance of hit is low.");
            // Shooting to miss the target, they've beaten the odds.
            missilecontroller.Launch();
        }
        else
        {
            Console.Log("[Missile] Chance of hit is high.");
            // Shooting to hit, provided it's in range!
            missilecontroller.Launch();
        }

        // Deduct missiles available.
        mMissilesLoaded -= 1;

        // If we no longer have any missiles, get reloading our launch system.
        if(mMissilesLoaded <= 0)
        {
            BeginReloading();
        }
    }

    /// <summary>
    /// Are we currently reloading the launcher
    /// </summary>
    bool mReloading = false;

    /// <summary>
    /// The time reload takes
    /// </summary>
    float mReloadTime = 10.0f;

    /// <summary>
    /// The reload timer.
    /// </summary>
    float mReloadCounter = 0.0f;

    /// <summary>
    /// Reload the launcher.
    /// </summary>
    void BeginReloading()
    {
        // Stop playing the sound;
       // mEnemyDetected.Stop();

        mReloading = true;
        mReloadCounter += Time.deltaTime;
    }

    
}
