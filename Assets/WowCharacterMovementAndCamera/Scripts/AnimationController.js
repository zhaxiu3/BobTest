//Animation
public var idleAnimName = "";
public var idleRightAnimName = "";
public var idleLeftAnimName = "";
public var walkAnimName = "";
public var walkRightAnimName = "";
public var walkLeftAnimName = "";
public var walkBackwardAnimName = "";
public var runAnimName = "";
public var runRightAnimName = "";
public var runLeftAnimName = "";
public var runBackwardName = "";
public var jumpAnimName = "";
public var swimIdleAnimName = "";
public var swimRightAnimName = "";
public var swimLeftAnimName = "";
public var swimForwardAnimName = "";
public var swimBackwardAnimName = "";
public var fallAnimName = "";

function Start () {
	Initialize();
}

function Initialize(){
        if(!GetComponent(Animation)) return;
        animation.Stop();//stop any animations that might be set to play automatically
        animation.wrapMode = WrapMode.Loop;//set all animations to loop by default
        //Start Idle Animation
        if(idleAnimName != ""){
            animation.Play(idleAnimName);   
        }
}
//Animation Here
//Idle animation
public function Idle () {
	if(idleAnimName != ""){
        animation.CrossFade(idleAnimName);
	}
}
function IdleLeft() {
	if(idleLeftAnimName != ""){
        animation.CrossFade(idleLeftAnimName);
	}
}
function IdleRight() {
	if(idleRightAnimName != ""){
        animation.CrossFade(idleRightAnimName);
	}
}
       
//walk animation
function Walk() {
	if(walkAnimName != ""){
		animation.CrossFade(walkAnimName);
		}
}
function WalkRight() {
	if(walkRightAnimName != ""){
		animation.CrossFade(walkRightAnimName);
		}
}
function WalkLeft() {
	if(walkLeftAnimName != ""){
		animation.CrossFade(walkLeftAnimName);
		}
}
function WalkBackward() {
	if(walkAnimName != ""){       
        animation.CrossFade(walkBackwardAnimName);
	}
}

//run animation
function Run() {
	if(runAnimName != ""){
        animation.CrossFade(runAnimName);
	}
}
function RunRight() {
	if(runRightAnimName != ""){
        animation.CrossFade(runRightAnimName);
	}
}
function RunLeft() {
	if(runLeftAnimName != ""){
        animation.CrossFade(runLeftAnimName);
	}
}
//run animation
function RunBackWard() {
	if(runAnimName != ""){
        animation.CrossFade(runBackwardName);
	}
}
       
//jump animation
function Jump() {
	if(jumpAnimName != ""){
		animation.CrossFade(jumpAnimName);
	}
}
//fall animation
function Fall() {
	if(fallAnimName != ""){            
		animation.CrossFade(fallAnimName);
	}
}
function SwimIdle() {
	if(swimIdleAnimName != ""){
		animation.CrossFade(swimForwardAnimName);
	}
}
function SwimRight() {
	if(swimRightAnimName != ""){
		animation.CrossFade(swimRightAnimName);
	}
}
function SwimLeft() {
	if(swimLeftAnimName != ""){
		animation.CrossFade(swimLeftAnimName);
	}
}
function SwimForward() {
	if(swimForwardAnimName != ""){
		animation.CrossFade(swimForwardAnimName);
	}
}
function SwimBackward() {
	if(swimBackwardAnimName != ""){
		animation.CrossFade(swimBackwardAnimName);
	}
}