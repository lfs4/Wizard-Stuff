using UnityEngine;
using System.Collections;

public class Player : Entity {

    public GameObject camera;
    public Vector2 velocity;
    float moveSpeed = 5;


	// Use this for initialization
	public override void StartingActions () 
	{
		camera = GameObject.FindGameObjectWithTag ("MainCamera");
	}
	// Update is called once per frame
	public override void UpdateActions () 
	{
		CheckPlayerMovement();
	}

	void CheckPlayerMovement()
	{
		if (Input.GetKey (KeyCode.RightArrow))
		{
			transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0));	
		}
			
		else if (Input.GetKey (KeyCode.LeftArrow))
		{
			transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0));	
		}
	}
}
