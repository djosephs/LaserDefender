using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	public float width = 10f;
	public float height = 5f;
	public GameObject enemyPrefab;
	public float speed = .2f;
	public float padding = .15f;
	float xmin;
	float xmax;
	private bool isMovingRight = true;
	// Use this for initialization
	void Start ()
	{
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
		
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xmin = leftmost.x;
		xmax = rightmost.x;
		
		
	}
	
	
	
	public void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}
	
	// Update is called once per frame
	void Update ()
	{	
		if (isMovingRight) {
			transform.position += new Vector3 (speed * Time.deltaTime, 0);
			foreach (Transform child in transform) {
				if (child.transform.position.x + padding >= xmax) {
					isMovingRight = false;
				}
			}
		}
		if (isMovingRight == false) {
			transform.position -= new Vector3 (speed * Time.deltaTime, 0);
			foreach (Transform child in transform) {
				if (child.transform.position.x - padding <= xmin) {
					isMovingRight = true;
				}
			}
		}
	}


}
