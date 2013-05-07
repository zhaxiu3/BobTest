using UnityEngine;
using System.Collections;

public class UnderWaterBlur : MonoBehaviour {
	private bool blurOn = false;
	private bool UseBlur = false;
	// Use this for initialization
	void Start () {
		UseBlur = GetComponent<WowMainCamera>().UseBlurEffect;
	}
	
void OnTriggerEnter(Collider other) {
	if(UseBlur==true){
		if(other.name =="UnderWaterSurface") {		
			if(blurOn == false){			
				GetComponent<BlurEffect>().enabled = true;
				blurOn = true;
			}
		}
	}
}

void OnTriggerExit(Collider other) {
	if(UseBlur==true){
		if(other.name =="UnderWaterSurface") {
			if(blurOn == true){
				GetComponent<BlurEffect>().enabled = false;
				blurOn = false;
			}
		}
	}
}
}
