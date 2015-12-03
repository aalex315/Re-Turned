using UnityEngine;
using System.Collections;

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
	//Water stuff
	public float maxWater = 100.0f;
	public float curWater = 100.0f;
	public float waterDrainSpeed = 100.0f;//Milliseconds for 1 water to drop
	public float waterItemsHeal = 30.0f;
	//Weapons
	public GameObject baseballBat;
	public GameObject handgun;
	//GUI
	public Rect healthRect;
	public Texture2D healthTexture;

	private GameObject equipped;
	private bool hasBaseballBat = false;
	private int x = Screen.width / 2;
	private int y = Screen.height / 2;
	private int waterSupply = 0;
	private int foodSupply = 0;

	void Start(){
		baseballBat.SetActive (true);
		equipped = baseballBat;
		healthRect = new Rect (Screen.width/50,Screen.height*19/20, Screen.width/75, Screen.height/75);
		healthTexture = new Texture2D (1,1);
		Color healthBarColor = new Color ();
		Color.TryParseHexString ("#ce0000", out healthBarColor);
		healthTexture.SetPixel (0,0, healthBarColor);
		healthTexture.Apply ();
	}

	void Update(){
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (new Vector3 (x, y)), out hit, 2)) {
			if (hit.collider.tag == "Baseball Bat" && hasBaseballBat) {
				if(Input.GetKeyDown(KeyCode.E)){
					Destroy(hit.collider.gameObject);
					hasBaseballBat = true;
				}
			}
			else if (hit.collider.tag == "Water") {
				if (Input.GetKeyDown(KeyCode.E)) {
					Destroy(hit.collider.gameObject);
					waterSupply++;
				}
			}
			else if (hit.collider.tag == "Food") {
				if (Input.GetKeyDown(KeyCode.E)) {
					Destroy(hit.collider.gameObject);
					foodSupply++;
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Z) && waterSupply > 0) {
			curWater += waterItemsHeal;
			waterSupply--;
			Debug.Log(curWater);
			if (curWater > maxWater) {
				curWater = maxWater;
				Debug.Log (curWater);
			}
		}
		else if (Input.GetKeyDown(KeyCode.X)) {
			curFood += foodItemsHeal;
			foodSupply--;
			if (curFood > maxFood) {
				curFood = maxFood;
			}
		}

		//Cheap ass "inventory system"
		CheapAssInventory ();
		//Drain water & food meters
		drain ();
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
		Debug.Log (curHealth);
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

	void OnGUI(){
		float healthRatio = curHealth / maxHealth;
		float rectWidth = healthRatio * Screen.width / 3;
		healthRect.width = rectWidth;
		GUI.DrawTexture (healthRect,healthTexture);
	}
}
