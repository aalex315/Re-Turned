using UnityEngine;
using System.Collections;

public class basicZombieAI : MonoBehaviour {

	NavMeshAgent agent;

	private Transform target;
	public float spottingLenght = 10;
	public float lostLenght = 20;

	private Transform currentDest;
	private float distance;
	private Animator anim;
	private bool isWalking = false;

	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
	}

	void Update () {
		distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance <= spottingLenght) {
			agent.SetDestination (target.position);
			isWalking = true;
			anim.SetBool("isWalking", isWalking);

		}
		else if (distance >= lostLenght) {
			agent.SetDestination(transform.position);
			isWalking = false;
			anim.SetBool("isWalking", isWalking);
		}
		//agent.SetDestination (target.position);
	}
}
