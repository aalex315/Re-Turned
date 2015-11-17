using UnityEngine;
using System.Collections;

public class GhostAI : MonoBehaviour {
	public Transform[] waypoints;
	int currentWaypoint = 0;
	public bool loop = true;
	public Transform player; // good guy
	public float playerDistance = 0; // distance to player when we start following
	
	Vector3 nextLocation; // var target
	Vector3 moveDirection;
	Vector3 distanceFromPlayer;
	
	NavMeshAgent agent;
	
	void Start () {
		//currentWaypoint = Random.Range (0, 6);
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (currentWaypoint < waypoints.Length) {
			nextLocation = waypoints [currentWaypoint].position;
			moveDirection = nextLocation - transform.position;
			distanceFromPlayer = player.position - transform.position;
			//Debug.Log ("moveDirection.magnitude: " + moveDirection.magnitude);
			if (moveDirection.magnitude < 2) {
				currentWaypoint++;
			} else if (distanceFromPlayer.magnitude < playerDistance) {
				nextLocation = player.position;
			}
		} else {
			
			if(loop) {
				currentWaypoint = 0;
			}
		}
		//Debug.Log ("current: " + currentWaypoint + "location: " + nextLocation);
		agent.SetDestination (nextLocation);
	}
}