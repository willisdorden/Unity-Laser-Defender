using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{


	public float health = 200;
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile)
		{
			health -= missile.getDamage();
			if (health <=0)
			{
				Destroy(gameObject);
			}
		}
		
	}
}
