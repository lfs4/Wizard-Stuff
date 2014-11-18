using UnityEngine;
using System.Collections;


[RequireComponent (typeof(PlayerPhysics))]
public class Player : MonoBehaviour {
	
	public float gravity;
	public float speed;
	public float acceleration;
	public float jumpHeight;
	
	public  Vector2 currentSpeed;
	public Vector2 targetSpeed;
	public Vector2 amountToMove;
	
	private PlayerPhysics playerPhysics;
	private PlayerInfo playerInfo;
	
	private Vector2 startPos;
	
	float breathLimit = 5f;
	float breathDamageTimer = 2f;
	
	
	
	// Use this for initialization
	public void Start () 
	{
		playerPhysics = GetComponent<PlayerPhysics>();
		playerInfo = GetComponent<PlayerInfo>();
		startPos.y = transform.position.y;
		startPos.x = transform.position.x;
	}
	// Update is called once per frame
	public void Update () 
	{
		CheckPlayerMovement();
		UnderWater();
		InputChangeForm();
		Debug.Log("Current movement speeds: " + amountToMove);

	}
	void InputChangeForm()
	{
		if(Input.GetKeyUp(KeyCode.Alpha1))
		{
			playerInfo.ChangeForm(0);
		}
		if(Input.GetKeyUp(KeyCode.Alpha2))
		{
			playerInfo.ChangeForm(1);
		}
		if(Input.GetKeyUp(KeyCode.Alpha3))
		{
			playerInfo.ChangeForm(2);
		}
		
	
	}
	void UnderWater()
	{
		if(playerInfo.underWater)
		{
			//if(playerInfo.formName != "Croc")
			//{
				//playerInfo.underWater = true;
				gravity = playerInfo.waterGravity;
				breathLimit -= Time.deltaTime;
				if(breathLimit <= 0)
				{
					breathDamageTimer -= Time.deltaTime;
					if(breathDamageTimer <= 0)
					{
						playerInfo.currentHealth -= 10;
						breathDamageTimer = 2;
					}
					
				}
			//}
			
		}
		else 
		{
			speed = playerInfo.landMoveSpeed;
			acceleration = playerInfo.landAccel;
			gravity = playerInfo.landAccel;
			//playerInfo.underWater = false;
			
		}
	}
	void OnTriggerEnter(Collider collider)
	{
		if(collider.GetComponent<Projectile>())
		{
			collider.GetComponent<Projectile>().PlayerHit(gameObject);
		}
	}

	public void CheckPlayerMovement()
	{
		
		if (playerPhysics.movementStopped)
		{
			targetSpeed.x = 0;
			currentSpeed.y = 0;
			currentSpeed.x = 0;
		}
		if(!playerInfo.underWater)
		{
			if(playerPhysics.grounded)
			{
				amountToMove.y = 0;
				speed = playerInfo.landMoveSpeed;
				if(Input.GetButtonDown("Jump"))
				{
					amountToMove.y = playerInfo.landJumpHeight;
				}
			}
			if(!playerPhysics.grounded)
			{
				speed = playerInfo.landFloatSpeed;
			}
		}
		else if(playerInfo.underWater)
		{
			speed = playerInfo.waterMoveSpeed;
			acceleration = playerInfo.waterAccel;
			gravity = playerInfo.waterGravity;
			if(Input.GetButtonDown("Jump"))
			{
				amountToMove.y = playerInfo.waterJumpHeight;
			}
		}
		/*else if (!playerPhysics.grounded && playerInfo.underWater || playerPhysics.grounded && playerInfo.underWater)
		{
			if(!playerPhysics.grounded)
			{
				
			}
			if(Input.GetButtonDown("Jump"))
			{
				
			}
		}
		*/
		
		targetSpeed.x = Input.GetAxisRaw("Horizontal") * speed;
		currentSpeed.y = IncrementTowards(currentSpeed.y, targetSpeed.x, acceleration);
		currentSpeed.x = IncrementTowards(currentSpeed.x, targetSpeed.x, acceleration);
		
		
		amountToMove.x = currentSpeed.x;
		amountToMove.y -= gravity * Time.deltaTime;
		playerPhysics.CheckWater(amountToMove * Time.deltaTime);
		playerPhysics.Move(amountToMove * Time.deltaTime);
		
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