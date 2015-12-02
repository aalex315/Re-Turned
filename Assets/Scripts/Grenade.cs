using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {

	public GameObject explosion;

	void Awake(){
		Invoke ("Explode",4);
	}

	void Explode(){
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
