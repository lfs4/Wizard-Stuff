using UnityEngine;
using System.Collections;

public class SpecialPhysicsCondition : MonoBehaviour {

	public string form;
	
	public BoxCollider collider;

	Vector3 c;
	Vector3 s;	
	
	public bool stuck; 
	
	public LayerMask wallMask;
	
	public LayerMask ceilingMask;
	
	public bool canStick;
	
	PlayerPhysics physics;

	PlayerInfo playerInfo;
	
	Ray ray;
	RaycastHit hit;
	
	/*new variables*/
	bool isClimbing;
	
	void Start()
	{
		SetCollider();
	}
	public void HandleSpecialCases(Vector2 moveAmount)
	{
		if(form == "Newt")
		{
			NewtWallCling(moveAmount);
		}
		if(form == "Dragon")
		{
			DragonJumpAndFly();
		}
	}
	public void SetCollider()
	{
		collider = GetComponent<BoxCollider>();
		c = collider.center;
		s = collider.size;
	}
	void NewtWallCling(Vector2 moveAmount)
	{
		string collisions = "You are hitting: ";
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 pos = transform.position;
		// check the bottom of the collider to see if im above water
		for(int i = 0; i < 3; i ++)
		{
			
			//float dir = Mathf.Sign(deltaY);
			float x = (pos.x + c.x - s.x/2) + s.x/2 * i; // Left, centre and then rightmost point of collider
			float y = pos.y + c.y + s.y/2 * 1; // Top of collider
			
			ray = new Ray(new Vector3(x,y), new Vector2(0,1));
			Debug.DrawRay(ray.origin,ray.direction);
			
			if (Physics.Raycast(ray,out hit,1,ceilingMask))
			{
				collisions += "ceiling, ";
			}
		}	
		
		for(int i = 0; i < 3; i ++)
		{
			float x = pos.x + c.x + (s.x/2 - (s.x/4)) * -1;
			float y = pos.y + c.y - s.y/2 + s.y/2 * i;
			ray = new Ray(new Vector3(x, y),new Vector2(-1,0));
			
			Debug.DrawRay(ray.origin, ray.direction, Color.blue);
			
			if(Physics.Raycast(ray, out hit, 1, wallMask))
			{
				canStick = true;
			}
			else
				canStick = false;
		}
		
		for(int i = 0; i < 3; i ++)
		{
			float x = pos.x + c.x + (s.x/2 - (s.x/4)) * 1;
			float y = pos.y + c.y - s.y/2 + s.y/2 * i;
			ray = new Ray(new Vector3(x, y),new Vector2(1,0));
			
			Debug.DrawRay(ray.origin, ray.direction);
			
			if(Physics.Raycast(ray, out hit, 1, wallMask))
			{
				collisions += "wall right";
			}
		}
		
		//Debug.Log(collisions);
	}

	void DragonJumpAndFly()
	{
	
	}
}
