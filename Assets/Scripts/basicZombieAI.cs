﻿using UnityEngine;
using System.Collections;

public class basicZombieAI : MonoBehaviour {

	NavMeshAgent agent;

	public float spottingLenght = 10;
	public float lostLenght = 20;
	public float attackDistance = 2;
	public int damage = 20;
	public float health = 100;

	private Transform target;
	private Transform currentDest;
	private float distance;
	private Animator anim;
	private bool isWalking = false;
	private bool isDead = false;
	private Collider coll;
	private Rigidbody rb;
	PlayerStuff script1;

	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator> ();
		script1 = GetComponent<PlayerStuff> ();
		coll = GetComponent<Collider> ();
		rb = GetComponent<Rigidbody> ();

		//Moves to navmesh if outside of it
		NavMeshHit closestHit;
		if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas)) {
			gameObject.transform.position = closestHit.position;
		}
		if (agent.isOnNavMesh == false) {
			Destroy(this);
		}
	}

	void Update () {
		if (!isDead) {
			distance = Vector3.Distance (transform.position, target.transform.position);
			if (distance <= spottingLenght && checkIfPlayerIsInFront() > 0.0) {
				agent.Resume();
				agent.SetDestination (target.position);
				isWalking = true;
				anim.SetBool("isWalking", isWalking);
				
			}
			else if (distance >= lostLenght) {
				agent.Stop();
				isWalking = false;
				anim.SetBool("isWalking", isWalking);
			}
			
			if (checkIfPlayerIsInFront() > 0.0 && distance < attackDistance && this.anim.GetCurrentAnimatorStateInfo(0).IsName("attack0") == false) {
				agent.Stop();
				StartCoroutine(PlayOneShot("isAttacking"));
			}
		}
	}

	void GainDamage(float damage){
		health -= damage;
		agent.Resume ();
		agent.SetDestination (target.position);
		checkIfDead ();
		isWalking = true;
		anim.SetBool ("isWalking", isWalking);
	}

	void checkIfDead(){
		if (health <= 0) {
			isDead = true;
			anim.SetBool("isDead", isDead);
			agent.Stop();
			rb.isKinematic = true;
			coll.enabled = false;
			agent.enabled = false;
			target.SendMessage("spawnOneEnemy");

		}
	}

	float checkIfPlayerIsInFront(){
		Vector3 relativePoint = transform.InverseTransformPoint (target.transform.position);
		return relativePoint.z;
	}

	public IEnumerator PlayOneShot(string paramName){
		anim.SetBool (paramName, true);
		yield return new WaitForSeconds(1);
		anim.SetBool (paramName, false);
		target.SendMessage ("GainDamage", 30);
		agent.Resume();
	}
}
