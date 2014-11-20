using UnityEngine;
using System.Collections;

public class PlayerForm : MonoBehaviour {
	
	public string formName;
	public int landRegenAmount;
	public int waterRegenAmount;
	public int breathLimit;
	public int attackPower;
	public int manaDrainAmount;
	public float landMoveSpeed;
	public float waterMoveSpeed;
	public float waterFloatSpeed;
	public float landFloatSpeed;
	public int landJumpHeight;
	public int waterJumpHeight;
	public float landAccel;
	public float waterAccel;
	public float landGravity;
	public float waterGravity;
	public int damageResistance;
	
	public bool specialPhysicsCondition;
	
	public SpecialPhysicsCondition specPhys;
	
	public BoxCollider collider;
	
	void Start()
	{
		collider = GetComponent<BoxCollider>();
		if(GetComponent<SpecialPhysicsCondition>())
		{
			specialPhysicsCondition = true;
			specPhys = GetComponent<SpecialPhysicsCondition>();
			specPhys.form = formName;
		}
	}
	


}
