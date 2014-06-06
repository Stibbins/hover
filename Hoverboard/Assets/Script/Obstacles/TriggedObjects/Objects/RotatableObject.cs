/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Rotates the object at its' origin when activated
 */

using UnityEngine;
using System.Collections;

public class RotatableObject : TriggedObject
{

    [SerializeField]
    Vector3 maxAngle=Vector3.right*90;
    [SerializeField]
    float rotationSpeedModifier = 1;

    Quaternion defaultRotation;

    protected override void Start()
    {
        defaultRotation = transform.rotation;
	}

    protected override void Update()
    {
        if (isActive)
        {
            //Rotate until the maxAngle is reached (using the shortest way)
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(maxAngle), rotationSpeedModifier * Time.deltaTime);
        }
        else
        {
            //Rotate until the default rotation is reached (using the shortest way)
            transform.rotation = Quaternion.RotateTowards(transform.rotation,defaultRotation, rotationSpeedModifier * Time.deltaTime);
        }
	}

    public override void TriggerEnter()
    {
        isActive = !isActive;
    }

    public override void TriggerExit()
    {
        //Do nothing
    }
}
