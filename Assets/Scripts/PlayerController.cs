using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float speed = 35f;
	public float padding = .15f;
	float xmin;
	float xmax;
	
	// Use this for initialization
	void Start ()
	{
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x;
		xmax = rightmost.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		KeyControls ();
		
	}
	
	void KeyControls ()
	{
		if (Input.GetKey (KeyCode.LeftArrow)) {			
			transform.position += Vector3.left * speed * Time.deltaTime;
		}
		if (Input.GetKey (KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		//Restricts player to gamespace
		float newX = Mathf.Clamp (transform.position.x, xmin + padding, xmax - padding);
		transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
	}
}