﻿using UnityEngine;
using System.Collections;

public class PlayerStuff : MonoBehaviour {

	public int health = 100;
	public int dealsDamage = 35;

	public void gainDamage(int damage){
		health -= damage;
		Debug.Log (health);
	}
}
