public class ReadMe_HowToUse {
	/*
	Well This is really simple to use.
	Let split the task in 4 easy steps.
	- Step 1 (Organize folder) :
		- Move the scripts WowMainCamera FROM Standard Assets folder to "Standard Assets" in the root of assets ( if none exist create one )
	
	- Step 2 ( Character And Camera ) :
		- Place the Camera Prefabs in the scene
		- Place the Character Prefabs in the scene
	
	- Step 3 ( Set The Camera Scripts on the Main Camera ) :
		- Drag the file called "WowMainCamera" on your main camera object.
		- Here you have 2 settings that you can choose ( Optional, I Prefer dont Use them) :
			- If you want your cursor to hide when the user right click the moose and rotate the camera, Then Check The "Hide And Show Cursor" box on your camera.
			- If you want to lock the rotation of the player and camera when right click, Then Check the "Lock Rotation When Right Click" box on your camera.
	
	- Step 4 ( Set the Water Tag And add the required collider to your water) :
		- From here you will need a new tag named "Water" so you are gonna click on : Edit / Project Settings / Tags , There you toggle the tag, increment the size ( +1 ) and name the new element : "Water"
		- Now go on your water object and set the tag to Water
		- Click on : Component / Physics / Box Collider
		- Chek the box IsTrigger on box collider component and set the height of your water, if your height is 20 then u will need to Center the axe Y of the collider to remove alf so it will be -10 , but if u let it like that
		... your character is gonna swim hover the water and not in water so set the Center axe Y to -10.5
		So , Box colliser Size Y = 20 & Center Y = -10.5
		You can set your own height for that mine is just for exemple
		- Check UseBlurEffect to get a nice blur effect underwater
		- Check UseFogEffect to get a nice fog effect underwater
		
		- Place the UnderWaterSurface Prefabs at the exact same position of your water and put it the same size.
	*/
}
