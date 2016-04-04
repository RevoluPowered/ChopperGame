using UnityEngine;
using System.Collections;

public class DeathHit : MonoBehaviour {
    public float hitCount = 0;
    public float maxHits = 2;

    void OnParticleCollision( GameObject other )
    {
        ++hitCount;
        Console.Log(" particle hit event");
        if (hitCount > maxHits)
        {
            //explosion effect
            Destroy(transform.root.gameObject);
            Console.Log(" particle Death event");
        }
    }

    void OnCollisionEnter( Collision col )
    {
        ++hitCount;
        Console.Log("hit event");
        if (hitCount > maxHits)
        {
            //explosion effect
            Destroy(transform.root.gameObject);
            Console.Log("Death event");
        }
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
