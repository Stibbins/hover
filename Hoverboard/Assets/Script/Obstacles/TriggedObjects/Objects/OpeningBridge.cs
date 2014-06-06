/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Rotates the object if the player collides with a trigger (never deactivates)
 */

using UnityEngine;
using System.Collections;

public class OpeningBridge : TriggedObject {

    [SerializeField]
    Vector3 maxAngle = Vector3.right * 90;
    [SerializeField]
    float rotationSpeedModifier = 1;

    Quaternion defaultRotation;

    protected override void Start()
    {
        defaultRotation = transform.rotation;
    }

	protected override void Update () 
    {
        if (isActive)
        {
            //Rotate until the maxAngle is reached (using the shortest way)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(maxAngle), rotationSpeedModifier * Time.deltaTime);
        }
	}

    public override void TriggerExit()
    {
        //Do nothing
    }
}
