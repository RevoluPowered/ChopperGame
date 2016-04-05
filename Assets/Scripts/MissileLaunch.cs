using UnityEngine;
using System.Collections;

/// <summary>
/// Missile launch script - I believe this is no longer used.
/// </summary>
public class MissileLaunch : MonoBehaviour {
    /// <summary>
    /// Find the closes target within range of the missile launcher.
    /// </summary>
    /// <returns></returns>
    GameObject FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

        GameObject closestTarget = null;
        float closestDistance = float.PositiveInfinity;
        foreach (GameObject go in targets)
        {
            float distance = Vector3.Distance(go.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestTarget = go;
                closestDistance = distance;
            }

        }

        return closestTarget;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
