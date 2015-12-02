using UnityEngine;
using System.Collections;

public class throwGrenade : MonoBehaviour {

	public Rigidbody grenade;
	public Transform throwPosition;
	public float throwStrenght;

	void Update(){
		if (Input.GetKeyDown(KeyCode.G)) {
			Rigidbody datGrenade;
			datGrenade = Instantiate(grenade,throwPosition.position, transform.rotation)as Rigidbody;
			datGrenade.AddForce(transform.forward * throwStrenght);
		}
	}
}
