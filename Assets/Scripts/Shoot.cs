using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shoot : MonoBehaviour {

	public GameObject bloodParticle;
	public GameObject particle2;
	public float fireRate = 0.5f;
	public float soundRadius = 50.0f;
	public Text ammoCounter;

	private int maxAmmoInClip = 7;
	private int ammoInClip = 7;
	private int totalAmmo = 14;
	private AudioSource gunshot;
	private Animator anim;
	private bool shooting = false;
	private bool allowFire = true;

	void Awake(){
		gunshot = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetButtonDown("Fire1") && allowFire && ammoInClip > 0) {
			StartCoroutine(Fire());
		}
		else if(Input.GetKeyDown(KeyCode.R)){
			StartCoroutine(Reload());
		}

		if (ammoInClip == 0) {
			StartCoroutine(Reload());
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
		ammoInClip--;
		UpdateText ();
		shooting = true;
		gunshot.Play();
		SendDetection ();
		anim.SetBool("shooting", shooting);

		allowFire = false;
		yield return new WaitForSeconds (fireRate);
		shooting = false;
		allowFire = true;
	}

	IEnumerator Reload(){
		allowFire = false;
		yield return new WaitForSeconds (1);
		int ammoNeeded = maxAmmoInClip - ammoInClip;
		if (totalAmmo >= ammoNeeded) {
			totalAmmo -= ammoNeeded;
			ammoInClip += ammoNeeded;
		}
		else {
			ammoInClip += totalAmmo;
			totalAmmo = 0;
		}
		UpdateText ();
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

	void UpdateText(){
		ammoCounter.text = ammoInClip + "/" + totalAmmo;
	}

	void AddMoreAmmo(int count){
		print (totalAmmo);
		totalAmmo += count;
		print (totalAmmo);
		UpdateText ();
	}

}
