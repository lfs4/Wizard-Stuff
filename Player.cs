﻿using UnityEngine;
using System.Collections;


[RequireComponent (typeof(PlayerPhysics))]
public class Player : MonoBehaviour {
	
	public float gravity = 20;
	public float speed = 8;
	public float acceleration = 30;
	public float jumpHeight = 12;
	
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
		startPos.y = transform.position.y + 5;
		startPos.x = transform.position.x;
	}
	// Update is called once per frame
	public void Update () 
	{
		CheckPlayerMovement();
		UnderWater();
		CheckForm();
		/*if(transform.position.y < -50)
		{
			transform.position = startPos;
		}*/
		
		//Debug.Log("Player collider cent: " + playerPhysics.collider.center);
	}
	void CheckForm()
	{
		if(Input.GetKeyUp(KeyCode.F))
		{
			if(playerInfo.GetForm == PlayerInfo.Form.Wizard)
			{
				
				playerInfo.GetForm = PlayerInfo.Form.Croc;
				playerInfo.damageResistance = 5;
			}
			else
			{
				playerInfo.GetForm = PlayerInfo.Form.Wizard;
				playerInfo.damageResistance = 0;
			}	
			playerInfo.manaTimer = 0;
			Debug.Log(playerInfo.GetForm);
		}
	}
	void UnderWater()
	{
		if(playerPhysics.underWater)
		{
			if(playerInfo.GetForm == PlayerInfo.Form.Wizard)
			{
				playerInfo.underWater = true;
				gravity = 5;
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
			}
			
		}
		else 
		{
			speed = 8;
			acceleration = 30;
			gravity = 20;
			playerInfo.underWater = false;
			
		}
	}
	void OnTriggerEnter(Collider collider)
	{
		if(collider.transform.tag == "Bullet")
		{
			playerInfo.currentHealth -=10;
			Destroy(collider.gameObject);
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
		else if(!playerPhysics.grounded && !playerPhysics.underWater && playerInfo.GetForm == PlayerInfo.Form.Wizard)
		{
			speed = 4;
		}
		else if (!playerPhysics.grounded && playerPhysics.underWater || playerPhysics.grounded && playerPhysics.underWater)
		{
			if(Input.GetButtonDown("Jump"))
			{
				amountToMove.y = jumpHeight;
			}
		}
		
		if(playerInfo.GetForm == PlayerInfo.Form.Croc)
		{
			if(!playerPhysics.underWater)
				speed = 4;
			else
				speed = 12;
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