using UnityEngine;
using System.Collections;

public class destroyAfterTime : MonoBehaviour {

	public float lifetime;

	void Awake() {
		Destroy (gameObject, lifetime);
	}

}
