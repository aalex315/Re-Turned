using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {
	
	public float spawningRadius = 50.0f;
	public float minSpawnRadius = 50.0f;
	public float maxSpawnRadius = 100.0f;
	public GameObject prefab;
	public int zombieCount = 30;

	private Transform target;
	private GameObject[] countZombies;

	void Update() {
		countZombies = GameObject.FindGameObjectsWithTag ("Zombie");
		spawnZombies ();
	}
	
	Vector3 RandomCircle (Vector3 center, float radius){
		float ang = Random.value * 360;
		Vector3 pos;
		pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad); 
		//pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		pos.y = 0;
		pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
		return pos;
	}

	void spawnZombies(){
		if (countZombies.Length < zombieCount) {
			Vector3 center = transform.position;
			for (int i = 0; i < zombieCount; i++){
				Vector3 pos = RandomCircle(center, Random.Range(minSpawnRadius, maxSpawnRadius));
				//Vector3 oldPos = (pos);
				pos.y = Terrain.activeTerrain.SampleHeight(pos);
				Debug.Log(pos);

				Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
				if (Physics.CheckSphere(new Vector3(pos.x, pos.y + 1.1f, pos.z), 1) == false) {
					Instantiate(prefab, pos, rot);
				}
			}
		}
	}
}
