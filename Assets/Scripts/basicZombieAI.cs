﻿using UnityEngine;
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
	PlayerStuff script1;
	//private bool onTerrain = false;

	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
		script1 = GameObject.FindWithTag ("Player").GetComponent<PlayerStuff> ();

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

		Vector3 relativePoint = transform.InverseTransformPoint (target.transform.position);
		if (relativePoint.z > 0.0 && distance < 2) {
			Debug.Log ("Deal Damage");
			script1.gainDamage();
		}
	}
}
