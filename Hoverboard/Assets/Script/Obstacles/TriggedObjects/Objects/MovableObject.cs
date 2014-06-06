/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Moves the object when activated
 */

using UnityEngine;
using System.Collections;

public class MovableObject : TriggedObject {

    [SerializeField]
    Vector3 moveToPosition = Vector3.right * 100;
    [SerializeField]
    float speedModifier = 1;

    Vector3 defaultPosition;

    protected override void Start()
    {
        defaultPosition = transform.position;
    }

    protected override void Update()
    {
        if (isActive)
        {
            //Rotate until the maxAngle is reached (using the shortest way)
            transform.position = Vector3.MoveTowards(transform.position, moveToPosition + defaultPosition, speedModifier * Time.deltaTime);
        }
        else
        {
            //Rotate until the default rotation is reached (using the shortest way)
            transform.position = Vector3.MoveTowards(transform.position, defaultPosition, speedModifier * Time.deltaTime);
        }
    }
}
