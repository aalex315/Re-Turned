using UnityEngine;
using System.Collections;

public class basicZombieAI : MonoBehaviour {

	NavMeshAgent agent;

	public float spottingLenght = 10;
	public float lostLenght = 20;

	private Transform target;
	private Transform currentDest;
	private float distance;
	private Animator anim;
	private bool isWalking = false;
	PlayerStuff playerStuff;
	//private bool onTerrain = false;

	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
		playerStuff = GameObject.FindWithTag ("Player").GetComponent<PlayerStuff> ();

	}

	void Update () {
		distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance <= spottingLenght && checkIfPlayerIsInFront() > 0.0) {
			agent.SetDestination (target.position);
			isWalking = true;
			anim.SetBool("isWalking", isWalking);

		}
		else if (distance >= lostLenght) {
			agent.SetDestination(transform.position);
			//agent.Stop(true);
			isWalking = false;
			anim.SetBool("isWalking", isWalking);
		}

		if (checkIfPlayerIsInFront() > 0.0) {
			//playerStuff.gainDamage();
		}
	}

	float checkIfPlayerIsInFront(){
		Vector3 relativePoint = transform.InverseTransformPoint (target.transform.position);
		return relativePoint.z;
	}
}
