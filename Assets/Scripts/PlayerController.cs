﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = .5f;
    private float xmin;
    private float xmax;
    private float ymin = -4.5f;
    private float ymax = -1.66f;
    public float laserSpeed;
    public GameObject Laser;
    public float health = 300;
    public float firingRate = 0.2f;

    public AudioClip fireSound;
    // Use this for initialization
    void Start ()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
        Vector3 Rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1,0,distance));
        xmin = leftmost.x + padding;
        xmax = Rightmost.x - padding;
    }
	
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position +=  Vector3.left * speed * Time.deltaTime ;
			
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, firingRate);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        
        // restricts the player to the gamespace.
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
        transform.position = new Vector3(transform.position.x, newY , transform.position.z);
        
        
    }

    void Fire ()
    {
            Vector3 offset = new Vector3(0,1,0);
            GameObject beam = Instantiate(Laser, transform.position+offset, Quaternion.identity) as GameObject;
            beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0,laserSpeed,0);
        AudioSource.PlayClipAtPoint(fireSound, transform.position);
    }
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyProjectile missile = collider.gameObject.GetComponent<EnemyProjectile>();
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
}
