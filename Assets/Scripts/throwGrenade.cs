using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class throwGrenade : MonoBehaviour {

	public Rigidbody grenade;
	public Transform throwPosition;
	public float throwStrenght;
	public Text grenadeText;

	private int grenadeCount;

	void Awake(){
		grenadeText.text = "Grenades: " + grenadeCount;
	}

	void Update(){
		if (Input.GetKeyDown(KeyCode.G) && grenadeCount > 0) {
			Rigidbody datGrenade;
			datGrenade = Instantiate(grenade,throwPosition.position, transform.rotation)as Rigidbody;
			datGrenade.AddForce(transform.forward * throwStrenght);
			grenadeCount--;
			grenadeText.text = "Grenades: " + grenadeCount;
		}
	}

	void AddGrenade(){
		grenadeCount++;
		grenadeText.text = "Grenades: " + grenadeCount;
	}
}
