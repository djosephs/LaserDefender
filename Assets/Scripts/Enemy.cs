using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public GameObject projectile;
	public float projectileSpeed = 3f;
	public float firingRate = .5f;
	public float health = 150f;
	public float shotsPerSecond = .5f;
	
	
	
	void OnTriggerEnter2D (Collider2D collider)
	{
		Projectile missile = collider.gameObject.GetComponent<Projectile> ();
		if (missile) {
			print ("missile");
			health -= missile.GetDamage ();
			print (health);
			missile.Hit ();
			if (health <= 0) {
				Destroy (gameObject);
			}
		
		
		}
		
		
		
	}
	
	void Fire ()
	{
		GameObject beam = Instantiate (projectile, this.transform.position, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3 (0, -1 * projectileSpeed, 0); 
	}
	
	
	
	void Update ()
	{
		float probability = Time.deltaTime * shotsPerSecond;
		if (Random.value < probability) {
			Fire ();	
		}
			
	}
	
	
}
	

	


