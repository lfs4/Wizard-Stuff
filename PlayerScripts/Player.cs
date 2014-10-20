using UnityEngine;
using System.Collections;


[RequireComponent (typeof(PlayerPhysics))]
public class Player : MonoBehaviour {

    public float gravity = 20;
    public float speed = 8;
    public float acceleration = 30;
    public float jumpHeight = 12;
    
    private float currentSpeed;
    private float targetSpeed;
    private Vector2 amountToMove;
    
	private PlayerPhysics playerPhysics;



	// Use this for initialization
	public void Start () 
	{
		playerPhysics = GetComponent<PlayerPhysics>();
	}
	// Update is called once per frame
	public void Update () 
	{
		CheckPlayerMovement();
	}
	public void CheckPlayerMovement()
	{
		if(playerPhysics.movementStopped)
		{
			targetSpeed = 0;
			currentSpeed = 0;
		}
			if(playerPhysics.grounded)
			{
				amountToMove.y = 0;
				speed = 8;
				if(Input.GetButtonDown("Jump"))
				{
					amountToMove.y = jumpHeight;
				}
			}
			else 
			{
				speed = 4;
			}
			
			targetSpeed = Input.GetAxisRaw("Horizontal") * speed;
			currentSpeed = IncrementTowards(currentSpeed, targetSpeed, acceleration);
		
			
			amountToMove.x = currentSpeed;
			amountToMove.y -= gravity * Time.deltaTime;
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
