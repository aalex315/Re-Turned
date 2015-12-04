using UnityEngine;
using System.Collections;

public class DayNightController : MonoBehaviour {

	public Light sun;
	public float secondsInFullDay = 120f;
	[Range(0,1)]
	public float currentTimeOfDay = 0;
	[HideInInspector]
	public float timeMultiplier = 1f;
	
	float sunInitialIntensity;
	
	void Start() {
		sunInitialIntensity = 2.2f;
	}
	
	void Update() {
		UpdateSun();
		
		currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
		
		if (currentTimeOfDay >= 1) {
			currentTimeOfDay = 0;
		}
		if (currentTimeOfDay >= 0.72 && currentTimeOfDay <= 0.74) {
			RenderSettings.fog = true;
			RenderSettings.fogDensity = 0.001f;
		}
		else if((currentTimeOfDay >= 0.74 && currentTimeOfDay <= 0.76) || (currentTimeOfDay >= 0.22 && currentTimeOfDay <= 0.24)){
			RenderSettings.fogDensity = 0.003f;
		}
		else if((currentTimeOfDay >= 0.76 && currentTimeOfDay <= 0.78) || (currentTimeOfDay >= 0.2 && currentTimeOfDay <= 0.22)){
			RenderSettings.fogDensity = 0.005f;
		}
		else if((currentTimeOfDay >= 0.78 && currentTimeOfDay <= 0.8) || (currentTimeOfDay >= 0.18 && currentTimeOfDay <= 0.2)){
			RenderSettings.fogDensity = 0.0075f;
		}
		else if(currentTimeOfDay >= 0.8 || (currentTimeOfDay >= 0 && currentTimeOfDay < 0.18)){
			RenderSettings.fogDensity = 0.01f;
		}
		else {
			RenderSettings.fogDensity = 0.0f;
			RenderSettings.fog = false;
		}
	}
	
	void UpdateSun() {
		sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90, 170, 0);
		
		float intensityMultiplier = 1;
		if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f) {
			intensityMultiplier = 0;
		}
		else if (currentTimeOfDay <= 0.25f) {
			intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
		}
		else if (currentTimeOfDay >= 0.73f) {
			intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
		}
		
		sun.intensity = sunInitialIntensity * intensityMultiplier;
	}
}
