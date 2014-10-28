using UnityEngine;
using System.Collections;

public class en2Brains : MonoBehaviour {

	public GameObject player; // for damage
	public GameObject enemy; // for damage
	public PlayerInfo character; // for damage
	public float distance; // calculate distance between enemy and player
	public float moveSpeed;//movement speed of this enemy
	public float turnSpeed;//speed of turning
	public float idleTimeLimit;//the time between movements of this enemy
	public bool useRange;//if true, this enemy will use 0 up to the turn speed
	float idleTime = 0;
	bool turning = false;
	bool moving = false;
	int dir = 1;
	float actionTime = 0;
	float actual_turn_amount;

	void Start ()
	{
		actual_turn_amount = turnSpeed;
	}

	void Update ()
	{

		distance = Vector3.Distance (player.transform.position, enemy.transform.position);
		if (distance <= 1) { // upon collision, call combat functions
						character.ApplyDamage (1);
						character.PlayerInCombat (1);
				} 
		else if (distance >= 3) { // when distance is far enough away 
						character.PlayerInCombat (0);
				}
		if(!turning && !moving)
		{
			idleTime +=  Time.deltaTime;
			if(idleTime > idleTimeLimit)
			{
				idleTime = 0;
				turning = true;
				actionTime = 2;
				dir = 1;
				if(Random.Range(0,10) > 4)
				{
					dir = -1;
				}
				if(useRange)
				{
					actual_turn_amount = Random.Range(0, turnSpeed);
				}
			}
		}

		if(turning)
		{
			turn();
		}

		if(moving)
		{
			move();
		}
	}

	void turn()
	{
		if(actionTime < 0)
		{
			turning = false;
			moving = true;
			actionTime = 2;
		}
		else
		{
			transform.rotation *= Quaternion.Euler(0,0,actual_turn_amount*Time.deltaTime*dir);
			actionTime -= Time.deltaTime;
		}
	}

	void move()
	{
		if(actionTime < 0)
		{
			moving = false;
		}
		else
		{
			transform.position += transform.right * moveSpeed * Time.deltaTime * -1;
			actionTime -= Time.deltaTime;
		}
	}
}
