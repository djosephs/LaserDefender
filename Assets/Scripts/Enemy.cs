using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public float health = 150f;
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
}
