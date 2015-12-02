using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject bloodParticle;
	public GameObject particle2;

	private AudioSource gunshot;
	private Animator anim;
	private bool shooting = false;

	void Awake(){
		gunshot = GetComponent<AudioSource> ();
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetButtonDown("Fire1")) {
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
			anim.SetBool("shooting", shooting);
			shooting = false;
		}
	}
}
