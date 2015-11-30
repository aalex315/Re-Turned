using UnityEngine;
using System.Collections;

public class PlayerStuff : MonoBehaviour {

	public float health = 100;
	public int dealsDamage = 35;

	public void GainDamage(float damage){
		health -= damage;
		Debug.Log (health);
	}
}
