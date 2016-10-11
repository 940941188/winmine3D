#pragma strict
public var currentMount:Transform;
public var speedFactor:float=0.1;
function Awake(){
    
    //Application.LoadLevelAdditiveAsync("2");
}
function Start () {
    //Application.LoadLevelAdditive("1");
}

function Update () {
    transform.position=Vector3.Lerp(transform.position,currentMount.position,speedFactor);
    transform.rotation=Quaternion.Slerp(transform.rotation,currentMount.rotation,speedFactor);
}
function setMount(newMount:Transform){
    currentMount=newMount;
}
    function Onpress(){
        Application.LoadLevel("1");
    }