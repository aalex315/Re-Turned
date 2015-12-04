using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStuff : MonoBehaviour {

	//Health Stuff
	public float maxHealth = 100.0f;
	public float curHealth = 100.0f;
	public float healthDrainSpeed = 100.0f;//Milliseconds for 1 health to drop
	//Food stuff
	public float maxFood = 100.0f;
	public float curFood = 100.0f;
	public float foodDrainSpeed = 500.0f;//Milliseconds for 1 food to drop
	public float foodItemsHeal = 30.0f;
	public float foodWarning = 40.0f;
	//Water stuff
	public float maxWater = 100.0f;
	public float curWater = 100.0f;
	public float waterDrainSpeed = 100.0f;//Milliseconds for 1 water to drop
	public float waterItemsHeal = 30.0f;
	public float waterWarning = 40.0f;
	//Weapons
	public GameObject baseballBat;
	public GameObject handgun;
	public Text pickupText;
	public Image waterIcon;
	public Image foodIcon;
	//GUI
	public GameObject stats;
	public Text hungerText;
	public Text waterText;
	public Text foodItems;
	public Text waterItems;
	public Text ammo;
	public GameObject pauseMenu;
	//Others
	public Light flashlight;
	
	private GameObject camera;
	private GameObject equipped;
	private bool hasBaseballBat = false;
	private int x = Screen.width / 2;
	private int y = Screen.height / 2;
	private int waterSupply = 0;
	private int foodSupply = 0;
	private bool statsActive;
	//GUI
	private Rect healthRect;
	private Texture2D healthTexture;

	void Start(){
		camera = GameObject.FindWithTag ("MainCamera");
		baseballBat.SetActive (true);
		equipped = baseballBat;
		//GUI
		healthRect = new Rect (Screen.width/50,Screen.height*19/20, Screen.width/75, Screen.height/75);
		healthTexture = new Texture2D (1,1);
		Color healthBarColor = new Color ();
		Color.TryParseHexString ("#ce0000", out healthBarColor);
		healthTexture.SetPixel (0,0, healthBarColor);
		healthTexture.Apply ();
		ammo.text = "";
		stats.SetActive (false);
		foodItems.text = "Food Items: " + foodSupply;
		waterItems.text = "Water Bottles: " + waterSupply;
		pauseMenu.SetActive (false);
		//Other
		flashlight.enabled = false;
	}

	void Update(){

		hungerText.text = "Hunger: " + curFood.ToString("F0");
		waterText.text = "Dehydration: " + curWater.ToString("F0");

		RaycastHit hit;
		pickupText.text = "";
		if (Physics.Raycast (Camera.main.ScreenPointToRay (new Vector3 (x, y)), out hit, 2)) {
			if (hit.collider.tag == "Baseball Bat" && hasBaseballBat) {
				pickupText.text = "Press 'E' to pickup";
				if(Input.GetKeyDown(KeyCode.E)){
					Destroy(hit.collider.gameObject);
					hasBaseballBat = true;
				}
			}
			else if (hit.collider.tag == "Water") {
				pickupText.text = "Press 'E' to pickup";
				if (Input.GetKeyDown(KeyCode.E)) {
					Destroy(hit.collider.gameObject);
					waterSupply++;
					waterItems.text = "Water Bottles: " + waterSupply;
				}
			}
			else if (hit.collider.tag == "Food") {
				pickupText.text = "Press 'E' to pickup";
				if (Input.GetKeyDown(KeyCode.E)) {
					Destroy(hit.collider.gameObject);
					foodSupply++;
					foodItems.text = "Food Items: " + foodSupply;
				}
			}
			else if (hit.collider.tag == "Grenade") {
				pickupText.text = "Press 'E' to pickup";
				if (Input.GetKeyDown(KeyCode.E)) {
					Destroy(hit.collider.gameObject);
					camera.SendMessage("AddGrenade");
				}
			}
			else if (hit.collider.tag == "Mag") {
				pickupText.text = "Press 'E' to pickup";
				if (Input.GetKeyDown(KeyCode.E)) {
					Destroy(hit.collider.gameObject);
					handgun.SendMessage("AddMoreAmmo", 7);
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Z) && waterSupply > 0) {
			curWater += waterItemsHeal;
			waterSupply--;
			Debug.Log(curWater);
			if (curWater > maxWater) {
				curWater = maxWater;
			}
			waterItems.text = "Water Bottles: " + waterSupply;
		}
		else if (Input.GetKeyDown(KeyCode.X) && foodSupply > 0) {
			curFood += foodItemsHeal;
			foodSupply--;
			if (curFood > maxFood) {
				curFood = maxFood;
			}
			foodItems.text = "Food Items: " + foodSupply;
		}
		else if (Input.GetKeyDown(KeyCode.F)) {
			if (flashlight.enabled == true) {
				flashlight.enabled = false;
			}
			else {
				flashlight.enabled = true;
			}
		}
		else if (Input.GetKeyDown(KeyCode.I)) {
			if (stats.activeSelf == true) {
				stats.SetActive(false);
			}
			else {
				stats.SetActive(true);
			}
		}
		else if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale == 1) {
				Time.timeScale = 0;
				pauseMenu.SetActive(true);
			}
			else {
				Time.timeScale = 1;
				pauseMenu.SetActive(false);
			}
		}

		//Cheap ass "inventory system"
		CheapAssInventory ();
		//Drain water & food meters
		drain ();
		//Display Food Warning
		displayFoodWaterIcon ();
	}

	void drain(){
		//Drain Food Meter
		curFood = Mathf.Clamp (curFood - (Time.deltaTime * (1000 / foodDrainSpeed)),0.0f,maxFood);
		//Drain Water Meter
		curWater = Mathf.Clamp (curWater - (Time.deltaTime * (1000 / waterDrainSpeed)), 0.0f, maxWater);
		//Drain health if food or water is 0
		if (curWater == 0 || curFood == 0) {
			curHealth = Mathf.Clamp(curHealth - (Time.deltaTime * (1000 / healthDrainSpeed)), 0.0f, maxHealth);
		}
	}

	public void GainDamage(float damage){
		curHealth -= damage;
		IfDead ();
	}

	void CheapAssInventory(){
		if (Input.GetKeyDown(KeyCode.Alpha1) && baseballBat.activeSelf == false) {
			equipped.SetActive(false);
			baseballBat.SetActive(true);
			equipped = baseballBat;
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2) && handgun.activeSelf == false) {
			equipped.SetActive(false);
			handgun.SetActive(true);
			equipped = handgun;
		}
	}

	void displayFoodWaterIcon(){
		if (waterWarning > curWater) {
			waterIcon.enabled = true;
		} 
		else if (waterWarning < curWater) {
			waterIcon.enabled = false;
		}

		if (foodWarning > curFood) {
			print("warning");
			foodIcon.enabled = true;
		} 
		else if (foodWarning < curFood) {
			foodIcon.enabled = false;
		}
	}

	void IfDead(){
		if (curHealth <= 0) {
			Application.LoadLevel("deathScene");
		}
	}

	void OnGUI(){
		float healthRatio = curHealth / maxHealth;
		float rectWidth = healthRatio * Screen.width / 3;
		healthRect.width = rectWidth;
		GUI.DrawTexture (healthRect,healthTexture);
	}
}
