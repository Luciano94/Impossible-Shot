using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputAndroid : IInput {
	private Direction x;
	private Direction y;

	private void Awake(){
		x = Direction.None;
		y = Direction.None;
	}

	public DirectionVec GetDirection(){
		DirectionVec dir;
		dir.x = x;
		dir.y = y;
		x = Direction.None;
		y = Direction.None;
		return dir;
	}

	public void OnEndDrag(PointerEventData eventData){
		Vector3 dragVec = (eventData.position - eventData.pressPosition).normalized;
		if (dragVec.x != 0){
			x = (dragVec.x > 0) ? Direction.Right : Direction.Left;
		}

		if (dragVec.y != 0){
			y = (dragVec.y > 0) ? Direction.Up : Direction.Down;
		}
	}
}
