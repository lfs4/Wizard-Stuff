using UnityEngine;
using System.Collections;

public class SpecialPhysicsCondition : MonoBehaviour {

	public string form;
	
	public BoxCollider collider;
	
	public BoxCollider altCollider;

	Vector3 c;
	Vector3 s;	
	
	public bool stuck; 
	
	public LayerMask wallMask;
	
	public LayerMask ceilingMask;
	
	public bool canStick;
	
	public bool canStickToCeiling;
	public bool canStickToWall;
	public string stick = "";
	string stickOld = "";
	
	PlayerInfo playerInfo;
	
	Ray ray;
	RaycastHit hit;
	
	/*new variables*/
	bool isClimbing;
	
	void Start()
	{
		collider = GetComponent<BoxCollider>();
		SetCollider(collider);
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
	public void SetCollider(BoxCollider _collider)
	{
		collider = _collider;
		//altCollider = GetComponentInChildren<BoxCollider>();
		c = collider.center;
		s = collider.size;
	}
	void NewtWallCling(Vector2 moveAmount)
	{
		string collisions = "You are hitting: ";
		int stickValue = 0;
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 pos = transform.position;
		canStickToCeiling = false;
		canStickToWall = false;
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
				stickValue++;
				canStickToCeiling = true;
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
				stickValue++;
				canStickToWall = true;
			}
		}
		
		for(int i = 0; i < 3; i ++)
		{
			float x = pos.x + c.x + (s.x/2 - (s.x/4)) * 1;
			float y = pos.y + c.y - s.y/2 + s.y/2 * i;
			ray = new Ray(new Vector3(x, y),new Vector2(1,0));
			
			Debug.DrawRay(ray.origin, ray.direction);
			
			if(Physics.Raycast(ray, out hit, 1, wallMask))
			{
				stickValue++;
				canStickToWall = true;
			}
		}
		
		if(stickValue > 0)
			canStick = true;
		else 
			canStick = false;
		/*	
		if (canStick)
		{
			if(canStickToWall && canStickToCeiling)
			{
				//just here to make sure the collider doest keep switihing if both are true
			}
			else if(canStickToWall)
			{
				SetCollider(altCollider);
			}
			else if(canStickToCeiling)
			{
				SetCollider(GetComponent<BoxCollider>());
				stick = "ciel";
			}
			
			GetComponentInParent<PlayerPhysics>().SetNewCollider();
		}*/
		//Debug.Log(collisions);
	}

	void DragonJumpAndFly()
	{
	
	}
}
