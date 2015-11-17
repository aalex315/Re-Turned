using UnityEngine;
using System.Collections;

public class zombieAI : MonoBehaviour {

	public float fieldOfViewAngle = 110f;
	public bool playerInSight;
	public Vector3 personalLastSighting;

	private NavMeshAgent nav;
	private CapsuleCollider col;
	private Animator anim;
	//private LastPlayerSighting lastPlayerSighting;
	private GameObject player;
	private Animator playerAnim;
	//private PlayerHealth playerHealth
	//private HashIDs hash;
	private Vector3 previousSighting;

	void awake(){
		nav = GetComponent<NavMeshAgent> ();
		col = GetComponent<CapsuleCollider>();
		//anim = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerAnim = player.GetComponent<Animator> ();
		//playerHealth = player.GetComponent<PlayerHealth>();

		//personalLastSighting = lastPlayerSighting.resetPosition;
		//previousSighting = lastPlayerSighting.resetPosition;
	}

	void Update(){
		/*
		if (lastPlayerSighting.position != previousSighting) {
			personalLastSighting = lastPlayerSighting.position;
		}

		previousSighting = lastPlayerSighting.position; */

	}

	void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			playerInSight = false;

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);

			if (angle < fieldOfViewAngle * 0.5f) {
				RaycastHit hit;

				if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius)) {
					if (hit.collider.gameObject == player) {
						playerInSight = true;
						//lastPlayerSighting.position = player.transform.position;
					}
				}
			}

			int playerLayerZeroStateHash = playerAnim.GetCurrentAnimatorStateInfo(0).fullPathHash;
			int playerLayerOneStateHash = playerAnim.GetCurrentAnimatorStateInfo(1).fullPathHash;
			/*
			if (playerLayerZeroStateHash == hash.locomotionState || playerLayerOneStateHash == hash.shoutState) {
				if (CalculatePathLength(other.transform.position) <= col.radius) {
					personalLastSighting = player.transform.position;
				}
			}*/
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "player") {
			playerInSight = false;
		}
	}

	float CalculatePathLength(Vector3 targetPosition){
		NavMeshPath path = new NavMeshPath ();
		if (nav.enabled) {
			nav.CalculatePath(targetPosition, path);
		}

		Vector3[] allWayPoints = new Vector3[path.corners.Length+2];

		allWayPoints [0] = transform.position;
		allWayPoints [allWayPoints.Length - 1] = targetPosition;

		for (int i = 0; i < path.corners.Length; i++) {
			allWayPoints[i+1] = path.corners[i];
		}

		float pathLenght = 0f;

		for (int i = 0; i < allWayPoints.Length-1; i++) {
			pathLenght+=Vector3.Distance(allWayPoints[i], allWayPoints[i+1]);
		}

		return pathLenght;
	}
}







