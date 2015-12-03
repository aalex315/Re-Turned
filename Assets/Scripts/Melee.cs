using UnityEngine;
using System.Collections;

public class Melee : MonoBehaviour {

	public float reach = 3f;
	public float damage = 25;

	private Animator anim;
	private bool allowHitting = true;

	void Awake (){
		anim = GetComponent<Animator> ();
	}

	void Update () {
		if (Input.GetButtonDown("Fire1") && allowHitting) {
			StartCoroutine(PlayOneShot("isHitting"));
		}
	}

	public IEnumerator PlayOneShot(string paramName){
		int x = Screen.width / 2;
		int y = Screen.height / 2;

		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(x, y)),out hit, reach)) {
			if (hit.collider.tag == "Zombie") {
				hit.transform.SendMessage("GainDamage", damage);
				Debug.Log("Hitting Zombie");
			}
		}

		allowHitting = false;
		anim.SetBool (paramName, true);
		yield return new WaitForSeconds(0.5f);
		anim.SetBool (paramName, false);
		allowHitting = true;
	}
}
