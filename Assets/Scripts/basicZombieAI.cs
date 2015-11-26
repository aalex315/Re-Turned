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
	private bool isAttacking = false;
	PlayerStuff script1;
	//private bool onTerrain = false;

	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator> ();
		script1 = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStuff>();

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
			isWalking = false;
			anim.SetBool("isWalking", isWalking);
		}

		if (checkIfPlayerIsInFront() > 0.0 && distance < 2) {
			//script1.gainDamage();
			agent.Stop();
			StartCoroutine(PlayOneShot("isAttacking"));

		}
	}

	float checkIfPlayerIsInFront(){
		Vector3 relativePoint = transform.InverseTransformPoint (target.transform.position);
		return relativePoint.z;
	}

	public IEnumerator PlayOneShot(string paramName){
		anim.SetBool (paramName, true);
		yield return new WaitForSeconds(1);
		agent.Resume();
		anim.SetBool (paramName, false);
	}
}
