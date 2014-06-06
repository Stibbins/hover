/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Activates/deactivates the objects added to the list objectsToTrigger, when the player collides with this object
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerCollider : MonoBehaviour {

    [SerializeField]
	List<GameObject> objectsToTrigger;


#if UNITY_EDITOR
    [SerializeField]
    bool simulateTriggerCollision;
    bool hasTrigged;
#endif

#if UNITY_EDITOR
    void Update () {
        //Simulate that the player collided with this
        if (simulateTriggerCollision)
        {
            if (!hasTrigged) //Only run once when you change simulateTrigger
            {
                hasTrigged = true;

                ActivateObjects();
            }
        }
        else
        {
            if (hasTrigged) //Only run once when you change simulateTrigger
            {
                hasTrigged = false;

                DeactivateObjects();
            }
        }
	}
#endif

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            ActivateObjects();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            DeactivateObjects();
        }
    }

    void ActivateObjects()
    {
        foreach (GameObject triggedObject in objectsToTrigger)
        {
            triggedObject.GetComponent<TriggedObject>().TriggerEnter();
        }
    }

    void DeactivateObjects()
    {
        foreach (GameObject triggedObject in objectsToTrigger)
        {
            triggedObject.GetComponent<TriggedObject>().TriggerExit();
        }
    }
}
