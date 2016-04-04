using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Display for the chopper health
/// This also enforces collision damage and the damage base to exist.
/// </summary>
[RequireComponent(typeof(DamageBase))]
[RequireComponent(typeof(CollisionDamage))]
[RequireComponent(typeof(HeliControl))]
public class ChopperHealth : MonoBehaviour {
    /// <summary>
    /// The damage base which is attached to the game object.
    /// </summary>
    private DamageBase mDamageBase;

    /// <summary>
    /// The control system for the chopper
    /// </summary>
    private HeliControl mHeliControl;

    /// <summary>
    /// Startup function / Unity
    /// </summary>
    void Start () {
        mDamageBase = GetComponent<DamageBase>();
        mHeliControl = GetComponent<HeliControl>();
        mDamageBase.DeathEvent += MDamageBase_DeathEvent;

	}

    private void MDamageBase_DeathEvent(DamageBase damageBase, float damageAmount, float overDamage, GameObject source)
    {
        Console.Log("Chopper died!");
        mHealthHUD.enabled = false;
        // Your chopper has just been hit, you're dead.
        mHeliControl.enabled = false;
    }

    /// <summary>
    /// The health hud object.
    /// </summary>
    public Scrollbar mHealthHUD;
	
	/// <summary>
    /// Fixed update based on the physics step time in Unity.
    /// </summary>
	void FixedUpdate () {
        mHealthHUD.size = mDamageBase.mHealth / mDamageBase.mMaximumHealth;        
	}
}
