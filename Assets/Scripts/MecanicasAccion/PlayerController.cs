using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody2D rb;

	public float speed;

	public bool caminar = true;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		checkMovement();
	}

	void checkMovement()
	{
		if(caminar)
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		if(moveHorizontal > 0)
		{
			transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
		}
		else if(moveHorizontal < 0)
		{
			transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
		}
		Vector3 movement = new Vector3 (moveHorizontal, moveVertical,0f);
		rb.velocity = movement * speed;	
		}
	}

	public void cambiarCaminar(bool can)
	{
		caminar = can;
		if(!caminar)
		{
			rb.velocity = new Vector2(0,0);
		}
	}
}
