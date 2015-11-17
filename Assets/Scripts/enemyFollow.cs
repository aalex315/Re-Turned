using UnityEngine;
using System.Collections;

public class enemyFollow : MonoBehaviour {

	NavMeshAgent agent;

	public Transform target;
	public float spottingLenght = 10;
	public float lostLenght = 20;

	private Transform currentDest;
	private float distance;
	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		//target = GameObject.FindWithTag ("Player").transform;
	}

	void Update () {
		distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance <= spottingLenght) {
			agent.SetDestination (target.position);
		}
		else if (distance >= lostLenght) {
			agent.SetDestination(transform.position);
		}
		//agent.SetDestination (target.position);
	}
}
