using UnityEngine;
using System.Collections;

/// <summary>
/// Rotor Handling script
/// Also handles damage.
/// </summary>
public class ChopperRotor : MonoBehaviour {

    /// <summary>
    /// What is the rotation axis?
    /// </summary>
    public Vector3 mRotationAxis = Vector3.zero;

    /// <summary>
    /// Specify the RPM of the Rotor.
    /// </summary>
    public float mSpeed = 4000.0f;

    /// <summary>
    /// Our chopper's one and only Damage Base for handling damage callback's and events.
    /// </summary>
    private DamageBase mDamageBase;

	/// <summary>
    /// Unity Start Function / Callback.
    /// </summary>
	void Start () {
        try
        {
            mDamageBase = transform.root.GetComponent<DamageBase>();

            mDamageBase.DeathEvent += MDamageBase_DeathEvent;
        }
        catch( System.Exception e )
        {
            Console.Log("DamageHandler [ChopperRotor]");
            Console.LogError("[ERROR]\n" + e);
        }
	}

    private void MDamageBase_DeathEvent(DamageBase damageBase, float damageAmount, float overDamage, GameObject source)
    {
        // Detach the chopper blade from the object, this will essentially look like the chopper is broken.
        gameObject.AddComponent<Rigidbody>();
        
    }

    /// <summary>
    /// Using fixed update as we don't need to do this each frame.
    /// </summary>
    void FixedUpdate () {
        if (mDamageBase.mAlive)
        {
            transform.Rotate((mRotationAxis * mSpeed) * Time.deltaTime);
        }
	}
}
