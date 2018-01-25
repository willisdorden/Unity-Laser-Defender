using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 15.0f;
    public float padding = .5f;
    private float xmin;
    private float xmax;
    private float ymin = -4.5f;
    private float ymax = -1.66f;
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
        // restricts the player to the gamespace.
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        float newY = Mathf.Clamp(transform.position.y, ymin, ymax);
        transform.position = new Vector3(transform.position.x, newY , transform.position.z);
    }

    void MoveWithArrow()
    {
		
    }
}
