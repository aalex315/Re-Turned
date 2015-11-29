using UnityEngine;
using System.Collections;

public class destroyBullet : MonoBehaviour {

	void OnCollisionEnter(Collision other){
		Destroy (this);
	}

}
