using UnityEngine;
using System.Collections;

public class pauseMenu : MonoBehaviour {

	public GameObject datMenu;

	public void Resume(){
		Time.timeScale = 1;
		datMenu.SetActive (false);
	}

	public void MainMenu(){
		Application.LoadLevel ("MainMenu");
	}
}
