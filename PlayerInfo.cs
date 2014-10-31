using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {

	public GameObject player;
	
	int health = 100;
	int damageCount = 0;
	
	int maxMana = 100;
	int maxHealth = 100;
	
	public int damageResistance;
	
	public int currentHealth;
	public int currentMana;
	
	Vector2 startPosition;
	
	public GUIText healthbar;
	public GUIText manaBar;
	float timer = 0.0f;
	public float manaTimer = 0f;
	
	bool playerFighting;
	public bool underWater;
	
	public enum Form {Wizard, Croc};
	
	Form currentForm;

	// Use this for initialization
	void Start () {
		currentHealth = health;	
		currentMana = maxMana;
		startPosition = transform.position;
		
	}
	
	public Form GetForm
	{
		get
		{
			return currentForm;
		}
		set
		{
			currentForm = value;
		}
	}

	
	// Update is called once per frame
	void Update () {

		//print ("Player in combat? " + playerFighting);
		if (!playerFighting) {
						if (currentHealth != maxHealth) {
								timer = timer + Time.deltaTime;
								if (timer > 6.0f) {
										if(GetForm == Form.Croc && underWater)
										{
											currentHealth += 20;
											
										}											
										else if(GetForm == Form.Wizard && !underWater)
											currentHealth = currentHealth + 10;
											
										timer = 0.0f;
								}
						}
				}
		if (currentHealth <= 0) 
		{
			player.transform.position = startPosition;				
			currentHealth = 100;
		}
		
		CheckMana();
		if(currentHealth > maxHealth)
			currentHealth = maxHealth;
		healthbar.text = "Health : " + currentHealth + " / 100";
		manaBar.text = "Mana: " + currentMana + "/ " + maxMana;
	}
	public void CheckMana()
	{	
		manaTimer += Time.deltaTime;
		if(GetForm == Form.Croc)
		{
			if(manaTimer >= 6f)
			{
				currentMana -= 15;
				if(currentMana <= 0)
				{
					currentMana = 0;
					GetForm = Form.Wizard;
				}
				manaTimer = 0;			
			}
		}
		if(GetForm == Form.Wizard)
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
				damageCount = damageCount + damage;
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
