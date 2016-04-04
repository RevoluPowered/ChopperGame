using UnityEngine;
using System.Collections;

public class MissileLaunch : MonoBehaviour {

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
