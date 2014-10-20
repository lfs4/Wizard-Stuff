using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public Transform player;
	private float trackSpeed = 10;
	
	public void SetTarget(Transform t)
	{
		player = t;
	}
	
	
	void LateUpdate()
	{
		if(player)
		{
			float x = IncrementTowards(transform.position.x, player.transform.position.x, trackSpeed);
			float y = IncrementTowards (transform.position.y , player.transform.position.y, trackSpeed);
			transform.position = new Vector3 (x,y, transform.position.z); 
		}	
	}
	private float IncrementTowards(float n, float target, float a)
	{
		if(n == target)
		{
			return n;
		}
		else
		{
			float dir = Mathf.Sign(target -n);
			n += a * Time.deltaTime * dir;
			return (dir == Mathf.Sign(target-n)) ? n: target;
		}
		
	}
}
