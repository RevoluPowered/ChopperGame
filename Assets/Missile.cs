using UnityEngine;
using System.Collections;


public class Missile : MonoBehaviour {

    public AudioSource mMissileLaunchSound;
    

   

    /// <summary>
    /// Target specified to the missile.
    /// </summary>
    public GameObject mCurrentTarget;

    // Targeting information.
    private Transform mTargetTransform;
    private Rigidbody mTargetRigidbody;

    private Rigidbody mRigidbody;

    // Traveling particles enabled on travel only!
    public GameObject mParticles;

    /// <summary>
    /// Object to spawn the missile explosion effect with.
    /// </summary>
    public GameObject mMissileExplosionPrefab;


	// Use this for initialization
	void Start () {
        mRigidbody = GetComponent<Rigidbody>();
        // Force update of target to current selection from unity editor.
        SetMissileTarget(mCurrentTarget);
        mParticles.SetActive(false);
	}

    void SetMissileTarget( GameObject target )
    {
        mCurrentTarget = target;
        
        mTargetTransform = mCurrentTarget.GetComponent<Transform>();
        mTargetRigidbody = mCurrentTarget.GetComponent<Rigidbody>();
    }
    float tgtTime = 0.0f;
    // Update is called once per frame

    bool finalisedPositon = false;
    Vector3 finalPosition = Vector3.zero;

    protected Vector3 FuturePositon()
    {
        if(!finalisedPositon)
        {
            finalPosition = CalculateInterceptCourse(mTargetTransform.position, mTargetRigidbody.velocity, transform.position, 60.0f);
            finalisedPositon = true;
            
            Debug.Log("Final position: " + finalPosition);
        }


        return finalPosition;
    }

    public static Vector3 CalculateInterceptCourse(Vector3 aTargetPos, Vector3 aTargetSpeed, Vector3 aInterceptorPos, float aInterceptorSpeed)
    {
        Vector3 targetDir = aTargetPos - aInterceptorPos;
        float iSpeed2 = aInterceptorSpeed * aInterceptorSpeed;
        float tSpeed2 = aTargetSpeed.sqrMagnitude;
        float fDot1 = Vector3.Dot(targetDir, aTargetSpeed);
        float targetDist2 = targetDir.sqrMagnitude;
        float d = (fDot1 * fDot1) - targetDist2 * (tSpeed2 - iSpeed2);
        if (d < 0.1f)  // negative == no possible course because the interceptor isn't fast enough
            return Vector3.zero;
        float sqrt = Mathf.Sqrt(d);
        float S1 = (-fDot1 - sqrt) / targetDist2;
        float S2 = (-fDot1 + sqrt) / targetDist2;
        if (S1 < 0.0001f)
        {
            if (S2 < 0.0001f)
                return Vector3.zero;
            else
                return (S2) * targetDir + aTargetSpeed;
        }
        else if (S2 < 0.0001f)
            return (S1) * targetDir + aTargetSpeed;
        else if (S1 < S2)
            return (S2) * targetDir + aTargetSpeed;
        else
            return (S1) * targetDir + aTargetSpeed;
    }

    private float mTimeoutCounter = 0.0f;
    public float mTimeoutDetonation = 2.0f;
    void Update() {
        if (mDebugLauncher)
        {
            Launch();
            // Reset.
            mDebugLauncher = false;
        }

        if (mObjectAttacking)
        {
            // Move the missile 
            mRigidbody.AddForce(FuturePositon() * ((mRigidbody.mass * 60.0f) * Time.deltaTime));

            // Rotate the missile to the velocity direction.
            // Similar to Quaternion.Slerp (from, to)
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, FuturePositon(), Time.deltaTime, 0.0f));

            float dist = Vector3.Distance(finalPosition, transform.position);

            if (dist < mClosestDistance)
            {
                mClosestDistance = dist;
                Debug.Log("Closest so far: " + dist);
            }
            if (dist < 85.0f)
            {
                Debug.Log("In range!");
                Detonate();
            }

            if (mTimeoutCounter > mTimeoutDetonation)
            {
                Detonate();
            }
            else
            {
                mTimeoutCounter += Time.deltaTime;
            }
        }
	}
    public float mClosestDistance = float.PositiveInfinity;
    /// <summary>
    /// Designed to be used when coding the missile only, and as a test fire button.
    /// </summary>
    public bool mDebugLauncher = false;
    
    /// <summary>
    /// After the object is launched this is used to tell the missile to update it's target
    /// force and various other detection is done.
    /// </summary>
    private bool mObjectAttacking = false;

    /// <summary>
    /// Launch the missile
    /// </summary>
    void Launch()
    {
        mMissileLaunchSound.Play();
        mObjectAttacking = true;
        mParticles.SetActive(true);
    }

    void Detonate()
    {
        // Instiantiate explosion.
        GameObject explosion = Instantiate<GameObject>(mMissileExplosionPrefab);
        // Set explosion position.
        explosion.transform.position = transform.position;

        Destroy(gameObject);
    }


    void OnCollisionEnter( Collision col )
    {
        Detonate();        
    }

}
