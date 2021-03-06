﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
	
	public float width = 10f;
	public float height = 5f;
	public GameObject enemyPrefab;
	public float speed = .2f;
	public float padding = .15f;
	public float spawnDelay = .5f;
	float xmin;
	float xmax;
	private bool isMovingRight = true;
	// Use this for initialization
	void Start ()
	{
		SpawnEnemies ();
	
	}
	
	public void SpawnEnemies ()
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
		
	void SpawnUntilFull ()
	{
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition; 
		}
		if (NextFreePosition ()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}
		
		
	Transform NextFreePosition ()
	{
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject; 
			}
		}
		return null;
	}
	
	bool AllMembersDead ()
	{
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}
		return true;
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
		if (AllMembersDead ()) {
			Debug.Log ("Empty formation");
			SpawnUntilFull ();
		}
	}




}
