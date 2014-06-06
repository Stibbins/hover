/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * Looks for checkpoints (object with the tag "Checkpoint"), to spawn at when the player dies
 * Put this script on the player object
 */

using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    float timeSeconds;
    Vector3 position;
    Quaternion rotation;
    float energy, startEnergy;

    [SerializeField]
    Timer timerScript;

    [SerializeField]
    AchievementManager achievementScript;

    [SerializeField]
    SpawnPosition spawnPositionScript;

	[SerializeField]
	Texture checkpointNotification;

    Movement movementScript;
    EnergyPool energyScript;
    [SerializeField]
	GUITextureDisplay textureDisplay;

	bool checkpointCheck;

	void Start () 


    {
        movementScript = gameObject.GetComponent<Movement>();
        energyScript = gameObject.GetComponent<EnergyPool>();

        timeSeconds = timerScript.m_raceTime;
        position = transform.position;
        rotation = transform.rotation;


        movementScript = gameObject.GetComponent<Movement>();
        
        checkpointCheck = false;

        energy = energyScript.m_energy;

	}

	//Called from the "killbox" prefab when the player goes out of bounds
	public void TriggerReset()
	{
		if (checkpointCheck == true)
			SpawnAtCheckpoint();
		
		else 
			SpawnAtStart();
		
	}

    public void SpawnAtCheckpoint()
    {
        ResetGameState();

        //Reset transform
        transform.position = position;
        transform.rotation = rotation;

		//Reset Velocity.y so we don't fall through the floor
        movementScript.setVelocity(Vector3.zero);
        movementScript.jumpVelocity = 0;

        //Reset timer
        timerScript.SetRaceTimer(timeSeconds);
        
		    

        //Reset energy
        energyScript.m_energy = energy;
    }

    public void SpawnAtStart()
    {
        ResetGameState();

		spawnPositionScript.ResetTransform();
        movementScript.setVelocity(Vector3.zero);
        movementScript.jumpVelocity = 0;

        //Reset timer
        timerScript.SetRaceTimer(0);

        //Reset energy
        energyScript.m_energy = startEnergy;

    }

    void ResetGameState()
    {
        //Reset speed
        movementScript.ResetSpeed();

        //Reset achievements' temporary progress
        //achievementScript.LoadProgressFromFile();
    }

//#if UNITY_EDITOR
    void Update()
    {
       	if (Input.GetButtonDown("Reset"))
        {
			if (checkpointCheck == true)
        		SpawnAtCheckpoint();
        	
			else
				SpawnAtStart();
			
        }
        
        if (Input.GetButtonDown ("LevelReset"))
        {
			checkpointCheck = false;
        	SpawnAtStart();
        	
        }
        
    }
//#endif

    void OnTriggerEnter(Collider col)
    {
        //If the player collides with a checkpoint, store the values needed.
        if (col.gameObject.tag == "Checkpoint")
        {
            //The player will respawn at the checkpoints' position and rotation.
            position = col.transform.position;
            rotation = col.transform.rotation;
			//position = transform.position;
            //Store current time
            timeSeconds = timerScript.m_raceTime;
            checkpointCheck = true;
            
			textureDisplay.checkpointTexture(checkpointNotification);    
           

            energy = gameObject.GetComponent<EnergyPool>().m_energy;
        }
    }
    
}
