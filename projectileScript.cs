using UnityEngine;
using System.Collections;

public class projectileScript : MonoBehaviour
{

	public Vector3 direction;
	int moveSpeed = 10;

	void Update()
	{
		transform.position += (direction * moveSpeed) * Time.deltaTime;
	}
}
