using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public Rigidbody projectile;
	public Transform barrelEnd;
	public float speed = 20;

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			Rigidbody instantiatedProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation)as Rigidbody;
			instantiatedProjectile.AddForce(barrelEnd.forward * -speed);
		}
	}
}
