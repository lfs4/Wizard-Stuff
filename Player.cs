using UnityEngine;
using System.Collections;


[RequireComponent (typeof(PlayerPhysics))]
public class Player : MonoBehaviour {
	
	float gravity;
	public float speed;
	float acceleration;
	float jumpHeight;
	
	public  Vector2 currentSpeed;
	public Vector2 targetSpeed;
	public Vector2 amountToMove;
	
	private PlayerPhysics playerPhysics;
	private PlayerInfo playerInfo;
	
	private Vector2 startPos;
	
	private PlayerForm currentForm;
	
	float breathLimit = 5f;
	float breathDamageTimer = 2f;
	
	
	
	// Use this for initialization
	public void Start () 
	{
		playerPhysics = GetComponent<PlayerPhysics>();
		playerInfo = GetComponent<PlayerInfo>();
		currentForm = playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>();
		startPos.y = transform.position.y;
		startPos.x = transform.position.x;
	}
	
	// Update is called once per frame
	public void Update () 
	{
		CheckPlayerMovement();
		UnderWater();
		InputChangeForm();
		//if(!playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>().specialPhysicsCondition)
		//	playerInfo.stuck = false;
		//Debug.Log("Current movement speeds: " + amountToMove);

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
		
		
		currentForm = playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>();
		if(currentForm.specialPhysicsCondition)
			playerPhysics.specPhys = currentForm.specPhys;
		else 
		{
			playerPhysics.specPhys = null;
			playerInfo.stuck = false;
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
			gravity = playerInfo.landGravity;
			//playerInfo.underWater = false;
			
		}
		if(playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>().specialPhysicsCondition)
		{
			//gravity *= -1;
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
					if(!playerInfo.stuck)
						amountToMove.y = playerInfo.landJumpHeight;
					else
					{
						playerInfo.stuck = false;
						amountToMove.x = playerInfo.landJumpHeight * 2;
					}
					
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
		
		if(!playerInfo.stuck)
		{
			targetSpeed.x = Input.GetAxisRaw("Horizontal") * speed;
			//targetSpeed.y = 0;
			currentSpeed.y = IncrementTowards(currentSpeed.y, targetSpeed.x, acceleration);
			currentSpeed.x = IncrementTowards(currentSpeed.x, targetSpeed.x, acceleration);
			amountToMove.x = currentSpeed.x;
			amountToMove.y -= gravity * Time.deltaTime;
		}
		else
		{
			targetSpeed.y = Input.GetAxisRaw("Vertical") * speed;
			
			currentSpeed.y = IncrementTowards(currentSpeed.y, targetSpeed.y, acceleration);
			amountToMove.y = currentSpeed.y;
			currentSpeed.x = 0;
			amountToMove.x = 0;
		}
		
		
		//currentSpeed.x = IncrementTowards(currentSpeed.x, targetSpeed.x, acceleration);
		
		/*
		if(playerInfo.forms[playerInfo.GetForm].GetComponent<PlayerForm>().specialPhysicsCondition == true)
		{
			currentSpeed.y = IncrementTowards(currentSpeed.y, targetSpeed.y, acceleration);
		}
		*/
		
		
	
		//Debug.Log("Current Movement Amount: " + amountToMove + " CurrentSpeed: " + currentSpeed);
		
		
		playerPhysics.CheckWater(amountToMove * Time.deltaTime);
		if(currentForm.specialPhysicsCondition)
			currentForm.specPhys.GetComponent<SpecialPhysicsCondition>().HandleSpecialCases(amountToMove * Time.deltaTime);
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