using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TomWorld
{

	public List<GameObject> enemies;

	public void init()
	{
		enemies = new List<GameObject>();
	}

	public void makeEnemies(int count, Vector2 point, Vector2 xyRange, GameObject content)
	{
		for(int i = 0;i < count;i++)
		{
			GameObject e = GameObject.Instantiate(content) as GameObject;
			point.x += Random.Range(-xyRange.x, xyRange.x);
			point.y += Random.Range(-xyRange.y, xyRange.y);
			e.transform.position = point;
			enemies.Add(e);
		}
	}

}
