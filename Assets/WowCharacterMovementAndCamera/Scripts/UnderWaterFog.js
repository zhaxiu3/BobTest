//This script enables underwater effects. Attach to main camera.

//The scene's default fog settings
private var defaultFog;
private var defaultFogColor;
private var defaultFogDensity;
private var defaultSkybox ;
private var noSkybox : Material;
private var UseFogEffect : boolean;

function Start () {
	defaultFog = RenderSettings.fog;
	defaultFogColor = RenderSettings.fogColor;
	defaultFogDensity = RenderSettings.fogDensity;
	defaultSkybox = RenderSettings.skybox;	
	UseFogEffect = true;
}

function OnTriggerEnter (other : Collider) {
if(UseFogEffect == true){
	if(other.name =="UnderWaterSurface") {
		RenderSettings.fog = true;
        RenderSettings.fogColor = Color (0, 0.2, 0.3, 0);
        RenderSettings.fogDensity = 0.01;
	}
	}
}
function OnTriggerExit (other : Collider) {
if(UseFogEffect == true){
	if(other.name =="UnderWaterSurface") {
		RenderSettings.fog = defaultFog;
        RenderSettings.fogColor = defaultFogColor;
        RenderSettings.fogDensity = defaultFogDensity;
	}
	}
}