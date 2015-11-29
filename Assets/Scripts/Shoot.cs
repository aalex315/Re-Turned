using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public Rigidbody projectile;
	public Transform barrelEnd;
	public float speed = 20;

	private AudioSource gunshot;
	private Animator anim;
	private bool shooting = false;

	void Awake(){
		gunshot = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
			shooting = true;
			Debug.Log(shooting);
			Rigidbody instantiatedProjectile = Instantiate(projectile, barrelEnd.position, barrelEnd.rotation)as Rigidbody;
			instantiatedProjectile.AddForce(barrelEnd.forward * -speed);
			gunshot.Play();
			anim.SetBool("shooting", shooting);
			shooting = false;
			Debug.Log(shooting);
		}
	}
}
