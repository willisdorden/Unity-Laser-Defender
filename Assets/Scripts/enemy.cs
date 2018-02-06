using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{



	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	private float xmin;
	private float xmax;
	public float padding = .5f;
	private bool dirRight = true;
	public float speed = 5.0f;
	public float spawnDelay = 0.5f;
   
	// Use this for initialization
	void Start()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
		Vector3 Rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
		xmin = leftmost.x;
		xmax = Rightmost.x;
		SpawnUntilFull();
	}

	void SpawnEnemies()
	{
		foreach (Transform child in transform)
		{
			GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	void SpawnUntilFull()
	{
		Transform freePosition = NextFreePosition();
		if (freePosition)
		{
			GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}
		if (NextFreePosition())
		{
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}

	// Update is called once per frame
	void Update()
	{
		if (dirRight)
			//	transform.Translate (Vector2.right * speed * Time.deltaTime);
			transform.position += Vector3.right * speed * Time.deltaTime;
		else
			//	transform.Translate (-Vector2.right * speed * Time.deltaTime);
			transform.position += Vector3.left * speed * Time.deltaTime;

		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		if (leftEdgeOfFormation < xmin)
		{
			dirRight = true;
		}
		if(rightEdgeOfFormation > xmax)
		{
			dirRight = false;
		}
		if (AllMembersDead())
		{
			Debug.Log("Empty Formation");
			SpawnUntilFull();
		}
	}

	bool AllMembersDead()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount > 0)
			{
				return false ;
			}
		}
		return true;
	}

	Transform NextFreePosition()
	{
		foreach (Transform childPositionGameObject in transform)
		{
			if (childPositionGameObject.childCount == 0)
			{
				return childPositionGameObject;
			}
		}
		return null;
	}
	
}
