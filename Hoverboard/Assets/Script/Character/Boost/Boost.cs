/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Increases the acceleration of the character by pressing a button.
 */

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnergyPool))]

public class Boost : MonoBehaviour {

    Animator animator;  //The animator of the character model
	
    [HideInInspector]
    public bool m_isBoosting;

    EnergyPool energyScript;

    [SerializeField]
    float energyDrainSpeed=1;
    [SerializeField]
    float pauseDuration=1;

    bool canBoost, hasBoosted;

	// Use this for initialization
	void Start () {
        energyScript = gameObject.GetComponent<EnergyPool>();

        animator = gameObject.GetComponent<Movement>().m_characterAnimator;

        canBoost = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Boost") && energyScript.m_energy > 0)
        {
            animator.SetBool("Boosting", true);

            hasBoosted = true;
        }
        else
        {
            animator.SetBool("Boosting", false);
        }

        if (canBoost && Input.GetButton("Boost") && energyScript.m_energy > 0 )
        {
            UseBoost();
            m_isBoosting = true;
        }
        else
        {
            m_isBoosting = false;
        }

        if (energyScript.m_energy <= 0 && hasBoosted)
        {
            //canBoost = false;
        }

        if (Input.GetButtonUp("Boost"))
        {
            canBoost = true;
        }
	}

    void UseBoost()
    {
        energyScript.Decrease(energyDrainSpeed * Time.deltaTime);

        energyScript.Pause(pauseDuration);
    }
}
