using UnityEngine;
using System.Collections;

public class en1Brains : MonoBehaviour {

	//variables to be assigned in the inspector
	public Vector3[] patrol_points;//points this mob will walk between
	float wait;
	public float waitLimit;//time this mob will wait before proceeding to the next patrol point
	public float moveSpeed;//how quickly the mob will approach the next patrol point

	int current_point = 0;//which point to move to
	int point_dir = 1;//which direction to move through patrol points
	public GameObject player; // for damage
	public GameObject enemy; // for damage
	public PlayerInfo character; // for damage
	public float distance; // calculate distance between enemy and player


	void Start ()
	{

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
		if(!(wait > 0))
		{
			moveToNextPoint();
		}
		else
		{
			wait -= Time.deltaTime;
		}
	}

	void moveToNextPoint()
	{
		transform.position = Vector3.MoveTowards(transform.position, patrol_points[current_point], moveSpeed*Time.deltaTime);
		if(Vector3.Distance(transform.position, patrol_points[current_point]) < 0.1f)
		{
			current_point+= point_dir;
			if(current_point > patrol_points.Length-1 || current_point < 0)
			{
				point_dir *= -1;
				current_point += point_dir;
				transform.localScale = new Vector3(point_dir, 1, 1);
			}

			wait = waitLimit;
		}
	}
}
