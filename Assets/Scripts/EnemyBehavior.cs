using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{


	public float health = 200;
	public GameObject Laser;
	public float LaserSpeed = 10;
	public float shotsPerSeconds = 0.5f;
	void OnTriggerEnter2D(Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if (missile)
		{
			health -= missile.getDamage();
			missile.Hit();
			if (health <=0)
			{
				Destroy(gameObject);
			}
		}
		
	}

	void Update()
	{
		float probability = Time.deltaTime * shotsPerSeconds;

		if (Random.value < probability)
		{
			Fire();

		}
	}

	void Fire()
	{
		Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
		GameObject missile = Instantiate(Laser, startPosition, Quaternion.identity) as GameObject;
		missile.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -LaserSpeed);
	}
	
}
