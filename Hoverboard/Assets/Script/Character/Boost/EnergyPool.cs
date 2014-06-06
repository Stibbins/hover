/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Handles the player's energy, that's used by boost.cs (if the player doesn't have energy, he/she can't use boost).
 */

using UnityEngine;
using System.Collections;

public class EnergyPool : MonoBehaviour {

    [SerializeField] //Remove when done
    float energy;
    public float m_energy
    {
        get { return energy; }
        set { energy = value; }
    }
	public float m_MaxEnergy
	{
		get { return maxEnergy; }
		set { maxEnergy = value; }
	}

    [SerializeField]
    float startEnergy = 0;
    [SerializeField]
    float maxEnergy = 10;
    [SerializeField]
    float energyRegenRate = 1;

    float pauseTimer = 0;

	// Use this for initialization
	void Start () {
        energy = startEnergy;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > pauseTimer) //If energy regen isn't paused
        {
            //Increase the energy at a constant speed
            Increase(energyRegenRate * Time.deltaTime);
        }

	}

    //Pause the regeneration
    public void Pause(float durationSeconds)
    {
        pauseTimer = Time.time + durationSeconds;
    }

    public void Increase(float amount)
    {
        energy += amount;

        if (energy > maxEnergy)
        {
            //energy can't be infinitely high
            energy = maxEnergy;
        }
    }

    //Decreases the energy pool
    public void Decrease(float amount)
    {
        energy -= amount;

        if (energy < 0)
        {
            //energy can't be negative
            energy = 0;
        }
    }
}
