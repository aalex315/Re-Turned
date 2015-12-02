using UnityEngine;
using System.Collections;

public class PlayerStuff : MonoBehaviour {

	public float health = 100;

	private bool hasBaseballBat = false;
	private int x = Screen.width / 2;
	private int y = Screen.height / 2;
	private bool baseballBat = false;
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
	}

	public void GainDamage(float damage){
		health -= damage;
		Debug.Log (health);
	}
}
