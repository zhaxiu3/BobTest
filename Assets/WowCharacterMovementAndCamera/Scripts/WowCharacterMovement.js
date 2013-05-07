 /*
*** Script Made by : Hesa ***
 */
// Air Time
public var airTime : float = 0;                 //how long have we been in the air since the last time we touched the ground <--- You can use it to make your character take some damage, if they fall for more of 2 or 3 seconds they die.
public var fallTime : float = 0.5f;             //the length of time we have to be falling before the system knows its a fall
public var jumpTime : float = 1.5f;
//Game Object Of the Character With Animation
public var AnimationObject : GameObject;
// Private variable
private var jumpSpeed:float = 7; // Speed and Heaight of Jump
private var gravity:float = 20; // Gravity of Character in World
private var runSpeed:float = 7.0; // Run Speed
private var walkSpeed:float = 3.0; // Walk Speed
private var rotateSpeed:float = 150.0; // Rotate Speed
private var swimSpeed:float = 4.5;  // Swim Speed
// If the player ends up on a slope which is at least the Slope Limit as set on the character controller, then he will slide down
var slideWhenOverSlopeLimit = false;
var SlideSpeed : float = 10f;
private var hit : RaycastHit;
private var contactPoint : Vector3;
private var slideLimit : float;
private var rayDistance : float;

private var grounded:boolean = false;
private var moveDirection:Vector3 = Vector3.zero;
private var isWalking:boolean = false;
private var moveStatus:String = "idle";
private var jumping:boolean = false;
private var moveSpeed:float = 0.0;
private var steering:boolean = true;
private var HasTriggerBeenUsed : boolean = false; // This is to make sure that the button is not repeatedly pressed.
private var setTrigger : boolean = false;
private var IsInWater : boolean = false;

private var Autorun : boolean = false;

function Start(){
	//AnimationObject.GetComponent(AnimationController) = AnimationObject.GetComponent(AnimationController);
	controller = GetComponent(CharacterController);
	rayDistance = controller.height * .5 + controller.radius;
	slideLimit = controller.slopeLimit - .1;
}

function OnTriggerEnter(other : Collider) {
	if(other.CompareTag("Water")) {
		//here you know the player just entered in water so you can play animation of enter in water
		IsInWater = true;
		gravity = 0;
		AnimationObject.GetComponent(AnimationController).SwimIdle();
	}
}

function OnTriggerExit(other : Collider) {
	if(other.CompareTag("Water")) {
		IsInWater = false;
		gravity = 20;
		//Here you know the player leave the water so you can verify if is  grounded then play your animation of stand up from water
		if(grounded && Input.GetAxis("Horizontal")){
			AnimationObject.GetComponent(AnimationController).Idle ();
		}
	}
}

function Update () {
	var inputModifyFactor = (Input.GetAxis("Horizontal") != 0.0 && Input.GetAxis("Vertical") != 0.0 )? .7071 : 1.0;
	if (steering) {
		// Only allow movement and jumps while grounded
		if(grounded && !IsInWater){
			if(moveStatus == "idle" ){
				AnimationObject.GetComponent(AnimationController).Idle();
				if(Input.GetAxis("Horizontal") > 0){//Right
					AnimationObject.GetComponent(AnimationController).IdleRight();
				}
				if(Input.GetAxis("Horizontal") < 0){//Left
					AnimationObject.GetComponent(AnimationController).IdleLeft();
				}
				
		}
	}
	if(!grounded && IsInWater){
		if(moveStatus == "idle" ){
			AnimationObject.GetComponent(AnimationController).SwimIdle();
		}
	}
	if(grounded || IsInWater) {
	
		if(Input.GetKeyUp(KeyCode.Numlock)){
			if(Autorun == true){
				Autorun = false;
			}else{
				Autorun = true;
			}
		}
		moveStatus = "idle";
		airTime = 0;
		var sliding = false;
		// See if surface immediately below should be slid down. We use this normally rather than a ControllerColliderHit point,
		// because that interferes with step climbing amongst other annoyances
		if (Physics.Raycast(transform.position, -Vector3.up, hit, rayDistance)) {
			if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
				sliding = true;
			}
		// However, just raycasting straight down from the center can fail when on steep slopes
		// So if the above raycast didn't catch anything, raycast down from the stored ControllerColliderHit point instead
		else {
			Physics.Raycast(contactPoint + Vector3.up, -Vector3.up, hit);
			if (Vector3.Angle(hit.normal, Vector3.up) > slideLimit)
				sliding = true;
			}
		// If sliding (and it's allowed)
		if ( (sliding && slideWhenOverSlopeLimit && !IsInWater) ) {
			var hitNormal = hit.normal;
			moveDirection = Vector3(hitNormal.x, -hitNormal.y, hitNormal.z);
			Vector3.OrthoNormalize (hitNormal, moveDirection);
			moveDirection *= SlideSpeed;
			GetComponent(CharacterController).Move(moveDirection * Time.deltaTime);
		}
		
		
		var MoveForward = transform.TransformDirection(Vector3.forward);
		//If get Autorun and we are not sliding and we dont get key W or S then move forward with autorun
		if(Autorun&& !sliding&&!Input.GetAxis("Vertical")){
			hitNormal = hit.normal;
			Vector3.OrthoNormalize (hitNormal, MoveForward);
			MoveForward *= moveSpeed;
			GetComponent(CharacterController).Move(MoveForward * Time.deltaTime);
			if(isWalking){
				moveStatus = "walking";
				AnimationObject.GetComponent(AnimationController).Walk();
           }else{
				moveStatus = "running"; 
				AnimationObject.GetComponent(AnimationController).Run();
           }
		}
		//If get moose both button then move forward
		if(Input.GetMouseButton(0)&&Input.GetMouseButton(1)&&!sliding&&!Input.GetAxis("Vertical")){
			Autorun = false;
			hitNormal = hit.normal;
			Vector3.OrthoNormalize (hitNormal, MoveForward);
			MoveForward *= moveSpeed;
			GetComponent(CharacterController).Move(MoveForward * Time.deltaTime);
			if(isWalking){
				moveStatus = "walking";
				AnimationObject.GetComponent(AnimationController).Walk();
           }else{
				moveStatus = "running"; 
				AnimationObject.GetComponent(AnimationController).Run();
           }
    }
	if(Input.GetAxis("Vertical")){
		Autorun = false;
	}
	if(Input.GetAxis("Horizontal") || Input.GetAxis("Vertical")){		
		if(IsInWater){
			moveStatus = "swimming";
			if(Input.GetAxis("Vertical") > 0){
				AnimationObject.GetComponent(AnimationController).SwimForward();
            }
			if(Input.GetAxis("Vertical") < 0){
                AnimationObject.GetComponent(AnimationController).SwimBackward();

            }
        }else{
			if(isWalking){
				moveStatus = "walking";
                if(Input.GetAxis("Vertical") > 0){
                    AnimationObject.GetComponent(AnimationController).Walk();									
                }
				if(Input.GetAxis("Vertical") < 0){
					AnimationObject.GetComponent(AnimationController).WalkBackward();
				}
			}else{
				moveStatus = "running";
				if(Input.GetAxis("Vertical") > 0){
					AnimationObject.GetComponent(AnimationController).Run();
				}
				if(Input.GetAxis("Vertical") < 0){
                    AnimationObject.GetComponent(AnimationController).RunBackWard();
				}

			}

        }

    }
    moveDirection = new Vector3((Input.GetMouseButton(1) ? Input.GetAxis("Horizontal") : 0),0,Input.GetAxis("Vertical"));
	// if moving forward and to the side at the same time, compensate for distance
	// TODO: may be better way to do this?
	if(Input.GetMouseButton(1) && Input.GetAxis("Horizontal") && Input.GetAxis("Vertical")) {
		moveDirection *= .7;
		if(isWalking){
			if(Input.GetAxis("Horizontal") > 0){//Right
				AnimationObject.GetComponent(AnimationController).WalkRight();
			}
			if(Input.GetAxis("Horizontal") < 0){//Left
				AnimationObject.GetComponent(AnimationController).WalkLeft();
			}
		}
		if(moveStatus == "running"){
			if(Input.GetAxis("Horizontal") > 0){//Right
				AnimationObject.GetComponent(AnimationController).RunRight();
			}
			if(Input.GetAxis("Horizontal") < 0){//Left
				AnimationObject.GetComponent(AnimationController).RunLeft();
			}
		}
		if(moveStatus == "swimming"){
			if(Input.GetAxis("Horizontal") > 0){//Right
				AnimationObject.GetComponent(AnimationController).SwimRight();
			}
			if(Input.GetAxis("Horizontal") < 0){//Left
				AnimationObject.GetComponent(AnimationController).SwimLeft();
			}
		}
	}   
	GetSpeed();
	moveDirection = transform.TransformDirection(moveDirection);
	moveDirection *= moveSpeed;
	// Jump!
	if(Input.GetKey("space") && !HasTriggerBeenUsed) {
		if(airTime < jumpTime) {
			AnimationObject.GetComponent(AnimationController).Jump();
			moveDirection.y = jumpSpeed;
			setTrigger = true;
		}

	}else if (Input.GetKeyDown("space") && HasTriggerBeenUsed) {
		setTrigger = true;
	}
	if (setTrigger) { HasTriggerBeenUsed = !HasTriggerBeenUsed; }   

	}else{
		airTime += Time.deltaTime;  //increase the airTime     
		if(airTime > fallTime) { //if we have been in the air to long
			AnimationObject.GetComponent(AnimationController).Fall(); //play the fall animation
			}
		}
		// Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.
		if(Input.GetMouseButton(1)) {
			if(IsInWater){
				transform.rotation = Quaternion.Euler(Camera.main.transform.eulerAngles.x,Camera.main.transform.eulerAngles.y,0);
			}else{
				transform.rotation = Quaternion.Euler(0,Camera.main.transform.eulerAngles.y,0);
			}
		} else {
			transform.Rotate(0,Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);
		}
       // Toggle walking/running with the KeypadDivide key
       if(Input.GetKeyUp(KeyCode.KeypadDivide))
			isWalking = !isWalking;
			//Apply gravity
			moveDirection.y -= gravity * Time.deltaTime;
			//Move controller
			var controller:CharacterController = GetComponent(CharacterController);
			var flags = controller.Move(moveDirection * Time.deltaTime);
			grounded = (flags & CollisionFlags.Below) != 0;
		}
}

function GetSpeed () {
	if (moveStatus == "idle"){
		moveSpeed = 0;
	}      
	if (moveStatus == "walking"){
		moveSpeed = walkSpeed;
	}
	if (moveStatus == "running"){
		moveSpeed = runSpeed;
	}
	if(moveStatus == "swimming"){
		moveSpeed = swimSpeed;
	}
	return moveSpeed;

}

function IsJumping () {
	return jumping;
}

function GetWalkSpeed () {
	return walkSpeed;
}

// Store point that we're in contact
function OnControllerColliderHit (hit : ControllerColliderHit) {
	contactPoint = hit.point;
}
@script RequireComponent(CharacterController)