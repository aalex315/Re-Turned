using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float radius = 5.0F;
	public float power = 20.0F;

	void Awake(){
		Vector3 explosionPos = transform.position;
		Collider[] colliders = Physics.OverlapSphere (explosionPos, radius);
		foreach (Collider hit in colliders) {
			Rigidbody rb = hit.GetComponent<Rigidbody>();
			if (rb != null && hit.gameObject.tag == "Zombie") {
				rb.AddExplosionForce(power, explosionPos, radius);
				hit.gameObject.SendMessage("GainDamage", 100.0F);
			}
			else if(rb != null){
				rb.AddExplosionForce(power, explosionPos, radius);
			}
		}
	}
}
