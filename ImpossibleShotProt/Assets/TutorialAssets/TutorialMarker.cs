using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMarker : MonoBehaviour {
	private Vector3 initPos;
	private bool shouldMove = false;
	private bool doneMoving = false;
	private float targetZ;

	private bool touched = false;
	
	private float lerpState;
	private float transitionSharpness;
	void Update () {
		if(shouldMove){
			lerpState += Time.deltaTime * transitionSharpness;
			if(lerpState >= 1){lerpState = 1; shouldMove = false;doneMoving = true;}
			transform.position = new Vector3(initPos.x,initPos.y, Mathf.Lerp(initPos.z,targetZ,lerpState));
		}
	}

	public bool DoneMoving(){return doneMoving;}
	public void Move(float sharpness){
		doneMoving = false;
		transitionSharpness = sharpness;
		initPos = transform.position;
		lerpState = 0;
		shouldMove = true;
	}

	public void setTargetZ(float Z){ targetZ = Z;}

	private void OnTriggerEnter(Collider other){
		gameObject.transform.position = Vector3.one * 60;
		touched = true;
	}

	public bool GetPlayerTouched(){
		return touched;
	}
}
