using UnityEngine;
using System.Collections;


[RequireComponent (typeof(PlayerPhysics))]
public class Player : MonoBehaviour {
	
	public float gravity;
	public float speed;
	public float acceleration;
	public float jumpHeight;
	
	public  float currentSpeed;
	public float targetSpeed;
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
		
		if(playerPhysics.movementStopped)
		{
			targetSpeed = 0;
			currentSpeed = 0;
		}
		else if(playerPhysics.grounded)
		{
			amountToMove.y = 0;
			speed = 8;
			if(Input.GetButtonDown("Jump"))
			{
				amountToMove.y = jumpHeight;
			}
		}
		else if(!playerPhysics.grounded && !playerInfo.underWater)
		{
			speed = playerInfo.landMoveSpeed;
		}
		else if (!playerPhysics.grounded && playerInfo.underWater || playerPhysics.grounded && playerInfo.underWater)
		{
			if(Input.GetButtonDown("Jump"))
			{
				amountToMove.y = jumpHeight;
			}
		}
		
		targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
		currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
		
		
		amountToMove.x = currentSpeed;
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