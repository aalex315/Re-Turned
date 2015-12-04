using UnityEngine;
using System.Collections;

public class SpawnPickup : MonoBehaviour {

	public GameObject[] pickups;

	private GameObject item;

	void Start () {
		InvokeRepeating ("spawnObject", 10, 360);

	}

	void spawnObject(){
		Instantiate (pickups[Random.Range(0, pickups.Length)], transform.position, Quaternion.identity);
	}
}
