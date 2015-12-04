using UnityEngine;
using System.Collections;

public class basicZombieAI : MonoBehaviour {

	NavMeshAgent agent;

	public float spottingLenght = 10;
	public float lostLenght = 20;
	public float attackDistance = 2;
	public int damage = 20;
	public float health = 100;
	public float despawnDistance = 200.0f;
	public AudioClip[] sounds;
	public AudioSource audios;
	public GameObject[] pickups;

	private Transform target;
	private Transform currentDest;
	private float distance;
	private Animator anim;
	private bool isWalking = false;
	private bool isDead = false;
	private Collider coll;
	private Rigidbody rb;
	private bool notBusy = true;
	private bool spottedPlayer = false;

	void Awake () {
		agent = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag("Player").transform;
		anim = GetComponent<Animator> ();
		coll = GetComponent<Collider> ();
		rb = GetComponent<Rigidbody> ();
		InvokeRepeating ("PlayRandomSound", Random.Range(0,5), Random.Range(5,10));

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
		if (!isDead && agent) {
			distance = Vector3.Distance (transform.position, target.transform.position);
			if (distance <= spottingLenght && checkIfPlayerIsInFront() > 0.0 && notBusy && agent && !spottedPlayer) {
				agent.Resume();
				agent.SetDestination (target.position);
				isWalking = true;
				anim.SetBool("isWalking", isWalking);
				
			}
			else if (distance >= lostLenght && notBusy && agent && !spottedPlayer) {
				agent.Stop();
				isWalking = false;
				anim.SetBool("isWalking", isWalking);
			}
			else if (spottedPlayer) {
				agent.Resume();
				agent.SetDestination (target.position);
				isWalking = true;
				anim.SetBool("isWalking", isWalking);
			}
			
			if (checkIfPlayerIsInFront() > 0.0 && distance < attackDistance && notBusy && agent) {
				agent.Stop();
				StartCoroutine(PlayOneShot("isAttacking"));
			}
		}
		if(distance >= despawnDistance){
			target.SendMessage("spawnOneEnemy");
			Destroy(gameObject);
		}
	}

	void GainDamage(float damage){
		health -= damage;
		agent.Resume ();
		agent.SetDestination (target.position);
		checkIfDead ();
		isWalking = true;
		anim.SetBool ("isWalking", isWalking);
		spottedPlayer = true;
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
			StartCoroutine(DeathSound());
			dropLoot();
		}
	}

	void dropLoot(){
	Instantiate (pickups[Random.Range(0, pickups.Length)], transform.position + new Vector3(0,1,0), Quaternion.identity);
	}

	float checkIfPlayerIsInFront(){
		Vector3 relativePoint = transform.InverseTransformPoint (target.transform.position);
		return relativePoint.z;
	}

	public IEnumerator PlayOneShot(string paramName){
		notBusy = false;
		anim.SetBool (paramName, true);
		yield return new WaitForSeconds(1f);
		if (distance < attackDistance) {
			target.SendMessage ("GainDamage", 30);
		}
		anim.SetBool (paramName, false);
		agent.Resume();
		notBusy = true;
	}

	void DetectPlayer(){
		spottedPlayer = true;
		agent.Resume ();
		Transform soundPoint = target.transform;
		agent.SetDestination (soundPoint.position);
		isWalking = true;
		anim.SetBool("isWalking", isWalking);
	}

	void PlayRandomSound(){
		if (audios.enabled == true) {
			audios.PlayOneShot (sounds[Random.Range(0,sounds.Length - 1)]);
		}
	}

	IEnumerator DeathSound(){
		audios.PlayOneShot(sounds[4]);
		yield return new WaitForSeconds(5f);
		audios.enabled = false;
	}
}
