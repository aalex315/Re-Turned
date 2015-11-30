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
			int x = Screen.width / 2;
			int y = Screen.height / 2;

			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(x, y)),out hit)) {
				Debug.Log(hit.point);
				if (hit.collider.tag == "Zombie") {
					hit.transform.SendMessage("GainDamage", 20f);
					Debug.Log(hit.distance);
				}
			}
			shooting = true;
			gunshot.Play();
			anim.SetBool("shooting", shooting);
			shooting = false;
		}
	}
}
