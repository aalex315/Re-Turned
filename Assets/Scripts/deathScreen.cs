using UnityEngine;
using System.Collections;

public class deathScreen : MonoBehaviour {

	public void Restart(){
		Application.LoadLevel ("game");
	}

	public void MainMenu(){
		Application.LoadLevel ("MainMenu");
	}
}
