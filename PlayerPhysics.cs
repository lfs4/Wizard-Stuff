using UnityEngine;
using System.Collections;


[RequireComponent (typeof(BoxCollider))]
public class PlayerPhysics : MonoBehaviour {
	
	public LayerMask collisionMask;
	public LayerMask waterMask;
	public LayerMask wallsMask;
	
	private PlayerInfo playerInfo;
	public SpecialPhysicsCondition specPhys;

	
	private BoxCollider collider;
	private Vector3 s;
	private Vector3 c;
	
	private float skin = .005f;
	
	
	public bool grounded;
	public bool movementStopped;
	//public bool movementStoppedY;
			
	
	Ray ray;
	RaycastHit hit;
	
	void Start() {
		playerInfo = GetComponent<PlayerInfo>();
		playerInfo.ChangeForm(0);
		collider = playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>().collider;
		//collider = GetComponent<BoxCollider>();
		s = collider.size;
		c = collider.center;
	}
	public void SetNewCollider()
	{
		collider = playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>().collider;
		s = collider.size;
		c = collider.center;

	}
	public void CheckWater(Vector2 moveAmount)
	{
		int waterCheck = 0;
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector2 pos = transform.position;
		// check the bottom of the collider to see if im above water
		for(int i = 0; i < 3; i ++)
		{
		
			//float dir = Mathf.Sign(deltaY);
			float x = (pos.x + c.x - s.x/2) + s.x/2 * i; // Left, centre and then rightmost point of collider
			float y = pos.y + c.y + s.y/2 * 1; // Bottom of collider
			
			ray = new Ray(new Vector3(x,y), new Vector2(0,1));
			//Debug.DrawRay(ray.origin,ray.direction);
			
			if (Physics.Raycast(ray,out hit,1,waterMask))
			{
				waterCheck++;
			}
		}	
		for(int i = 0; i < 3; i ++)
		{
			
			//float dir = Mathf.Sign(deltaY);
			float x = (pos.x + c.x - s.x/2) + s.x/2 * i; // Left, centre and then rightmost point of collider
			float y = pos.y + c.y + s.y/2 * - 1; // Bottom of collider
			
			ray = new Ray(new Vector3(x,y), new Vector2(0, -1));
			//Debug.DrawRay(ray.origin,ray.direction, Color.blue);
			
			if (Physics.Raycast(ray,out hit,1,waterMask))
			{
				waterCheck++;
			}
			
		}	
		
		for(int i = 0; i < 3; i ++)
		{
			float x = pos.x + c.x + (s.x/2 - (s.x/4)) * -1;
			float y = pos.y + c.y - s.y/2 + s.y/2 * i;
			ray = new Ray(new Vector3(x, y),new Vector2(-1,0));
			
			//Debug.DrawRay(ray.origin, ray.direction, Color.blue);
			
			if(Physics.Raycast(ray, out hit, 1, waterMask))
			{
				waterCheck++;
			}
		}
		
		for(int i = 0; i < 3; i ++)
		{
			float x = pos.x + c.x + (s.x/2 - (s.x/4)) * 1;
			float y = pos.y + c.y - s.y/2 + s.y/2 * i;
			ray = new Ray(new Vector3(x, y),new Vector2(1,0));
			
			//Debug.DrawRay(ray.origin, ray.direction, Color.blue);
			
			if(Physics.Raycast(ray, out hit, 1, waterMask))
			{
				waterCheck++;
			}
		}
	
		 if(waterCheck > 0)
		 {
		 	playerInfo.underWater = true;
		 }
		 else
		 {
		 	playerInfo.underWater = false;
		 }
		
		
	}
	public void Move(Vector2 moveAmount) {
		
		float deltaY = moveAmount.y;
		float deltaX = moveAmount.x;
		Vector3 p = transform.position;
		
		// Check collisions above and below
		grounded = false;
		
//		Debug.Log("Player center" + c);
		for (int i = 0; i<3; i ++) {
			float dir = Mathf.Sign(deltaY);
			float x = (p.x + c.x - s.x/2) + s.x/2 * i; // Left, center and then rightmost point of collider
			float y = p.y + c.y + s.y/2 * dir; // Bottom of collider
			
			ray = new Ray(new Vector3(x,y, p.z), new Vector2(0,dir));
			//Debug.DrawRay(ray.origin,ray.direction);
			
			if (Physics.Raycast(ray,out hit,Mathf.Abs(deltaY) + skin,collisionMask)) 
			{
				// Get Distance between player and ground
				float dst = Vector3.Distance (ray.origin, hit.point);
				
				//Debug.Log("Raycast hit normal: " + hit.normal);
				
				//Debug.Log("Ray hit angle: " + Vector3.Angle(hit.normal, Vector3.up));
				
				// Stop player's downwards movement after coming within skin width of a collider
				if (dst > skin) {
					deltaY = dst * dir - skin * dir;
				}
				else {
					deltaY = 0;
				}
				
				if(playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>().specialPhysicsCondition == true)
				{
					if(specPhys.canStick)
						playerInfo.stuck = true;
					else
					{
						playerInfo.stuck = false;
					}
					
					grounded = true;
					break;
				}
				else
				{
					grounded = true;
					
					break;
				}
				
				
				
			}
		}
		movementStopped = false;
		// Check collisions left and right
		
		for (int i = 0; i<3; i ++) {
			float dir = Mathf.Sign(deltaX);
			float x = p.x + c.x + ((s.x/2)) * dir;
			float y = p.y + c.y - s.y/2 + s.y/2 * i;
			
			ray = new Ray(new Vector3(x,y, p.z), new Vector2(dir,0));
			//Debug.DrawRay(ray.origin,ray.direction);
			
			if (Physics.Raycast(ray,out hit,Mathf.Abs(deltaX) + skin,wallsMask)) 
			{
					// Get Distance between player and ground
					float dst = Vector3.Distance (ray.origin, hit.point);
					playerInfo.stuck = false;
					// Stop player's downwards movement after coming within skin width of a collider
					if ((dst > skin)) 
					{
						deltaX = dst * dir - skin * dir;
					}
					
					else {
						deltaX = 0;
					}
					
					if(playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>().specialPhysicsCondition == true)
					{
						if(specPhys.canStick)
							playerInfo.stuck = true;
						else
						{
							playerInfo.stuck = false;
						}
					}
					else
					{
						movementStopped = true;
						break;
					}
			}

		}
	
		Vector2 finalTransform = new Vector2(deltaX,deltaY);
		
		
		transform.Translate(finalTransform);
	}
	void Flip(float deltaX)
	{
		Vector2 spriteScale = transform.localScale;
		spriteScale.x *= Mathf.Sign(deltaX);
		transform.localScale = spriteScale;
	}
	
	
}