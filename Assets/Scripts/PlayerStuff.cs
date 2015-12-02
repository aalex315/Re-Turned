using UnityEngine;
using System.Collections;

public class PlayerStuff : MonoBehaviour {

	public float health = 100;
	public GameObject baseballBat;
	public GameObject handgun;

	private GameObject equipped;
	private bool hasBaseballBat = false;
	private int x = Screen.width / 2;
	private int y = Screen.height / 2;

	void Start(){
		baseballBat.SetActive (true);
		equipped = baseballBat;
	}

	void Update(){
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (new Vector3 (x, y)), out hit)) {
			if (hit.collider.tag == "Baseball Bat") {
				if(Input.GetKeyDown(KeyCode.E)){
					Destroy(hit.collider.gameObject);
					hasBaseballBat = true;
				}
			}
		}

		//Cheap ass "inventory system"
		if (Input.GetKeyDown(KeyCode.Alpha1) && baseballBat.activeSelf == false) {
			equipped.SetActive(false);
			baseballBat.SetActive(true);
			equipped = baseballBat;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && handgun.activeSelf == false) {
			equipped.SetActive(false);
			handgun.SetActive(true);
			equipped = baseballBat;
		}

	}

	public void GainDamage(float damage){
		health -= damage;
	}
}
