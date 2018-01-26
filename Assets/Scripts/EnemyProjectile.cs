using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

	public float damage = 50f;

	public float getDamage()
	{
		return damage;
	}
	public void Hit()
	{
		Destroy(gameObject);
	}
	
}


