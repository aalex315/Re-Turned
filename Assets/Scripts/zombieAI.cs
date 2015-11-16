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
}
