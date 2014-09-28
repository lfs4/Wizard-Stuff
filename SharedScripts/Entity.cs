using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		StartingActions();
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateActions();
	}
	public virtual void StartingActions(){}
	public virtual void UpdateActions(){}
}
