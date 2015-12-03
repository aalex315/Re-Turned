using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject bloodParticle;
	public GameObject particle2;
	public float fireRate = 0.5f;
	public float soundRadius = 50.0f;

	private AudioSource gunshot;
	private Animator anim;
	private bool shooting = false;
	private bool allowFire = true;

	void Awake(){
		gunshot = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
		allowFire = true;
	}

	void Update () {
		if (Input.GetButtonDown("Fire1") && allowFire) {
			StartCoroutine(Fire ());
		}
	}

	IEnumerator Fire(){
		int x = Screen.width / 2;
		int y = Screen.height / 2;
		
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(x, y)),out hit)) {
			if (hit.collider.tag == "Zombie") {
				hit.transform.SendMessage("GainDamage", 20f);
				Instantiate(bloodParticle, hit.point, hit.transform.rotation);
			}
			else {
				Instantiate(particle2, hit.point, hit.transform.rotation);
			}
		}
		shooting = true;
		gunshot.Play();
		SendDetection ();
		anim.SetBool("shooting", shooting);
		shooting = false;
		allowFire = false;
		yield return new WaitForSeconds (fireRate);
		allowFire = true;
	}
	void SendDetection(){
		Collider[] cols = Physics.OverlapSphere (transform.position, soundRadius);
		foreach (Collider col in cols) {
			if (col.tag == "Zombie") {
				col.gameObject.SendMessage("DetectPlayer");
			}
		}
	}
}
