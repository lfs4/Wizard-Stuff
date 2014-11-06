using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInfo : MonoBehaviour {

	public GameObject player;
	public PlayerForm Player;
	
	List<GameObject> forms;
	
	int health = 100;
	int damageCount = 0;
	
	int maxMana = 100;
	int maxHealth = 100;
	
	
	
	public int currentHealth;
	public int currentMana;
	
	Vector2 startPosition;
	
	public GUIText healthbar;
	public GUIText manaBar;
	float timer = 0.0f;
	float manaTimer = 0f;
	
	private float regenTimer = 6.0f;

	bool playerFighting;
	
	public bool underWater;
	
	int currentForm;
	
	
	/*
	Player holder variables for form values
	*/
	public string formName;
	public int landRegenAmout;
	public int waterRegenAmount;
	public int attackPower;
	public int manaDrainAmount;
	public float landMoveSpeed;
	public float waterMoveSpeed;
	public int damageResistance;

	// Use this for initialization
	void Start () 
	{
		currentHealth = health;	
		currentMana = maxMana;
		startPosition = transform.position;
		
	}

	void Update () 
	{
		UpdateHealth();
		CheckMana();
		UpdateBars();
	}
	void UpdateBars()
	{
		healthbar.text = "Health : " + currentHealth + " / 100";
		manaBar.text = "Mana: " + currentMana + "/ " + maxMana;
	}
	void UpdateHealth()
	{
		if (!playerFighting) 
		{
			if (currentHealth != maxHealth) 
			{
				timer = timer + Time.deltaTime;
				if (timer > regenTimer) {
					if(underWater)
					{
						currentHealth += waterRegenAmount;
					}											
					else if(!underWater)
					{
						currentHealth = currentHealth += landRegenAmout;
					}
					timer = 0.0f;
				}
			}
		}
		if (currentHealth <= 0) 
		{
			player.transform.position = startPosition;				
			currentHealth = maxHealth;
		}
		if(currentHealth > maxHealth)
			currentHealth = maxHealth;
	}
	public void CheckMana()
	{	
		manaTimer += Time.deltaTime;
		if(formName != "Wizard")
		{
			if(manaTimer >= 6f)
			{
				currentMana -= manaDrainAmount;
				if(currentMana <= 0)
				{
					currentMana = 0;
				}
				manaTimer = 0;			
			}
		}
		if(formName == "Wizard")
		{
			if(manaTimer >= 2f)
			{	
				if(currentMana + 15 <= maxMana)
					currentMana += 15;
				else 
					currentMana = maxMana;
				manaTimer = 0;			
			}
		}
	}
	public void ApplyDamage (int damage) 
	{
				damageCount += damage;
				if (damageCount == 10) {
						currentHealth = currentHealth - 10;
						healthbar.text = "Health : " + currentHealth + " / 100";
						damageCount = 0;
				}
		}
	public void PlayerInCombat (int combat)
	{
				if (combat == 1) {
						playerFighting = true;

				} else {
						playerFighting = false;
				}
		}

}
