using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPC : IInput {
	public DirectionVec GetDirection()
	{
		DirectionVec dir;
		dir.y = Direction.None;
		dir.x = Direction.None;
		if (Input.GetAxis("Vertical") != 0){
			if (Input.GetAxis ("Vertical") > 0) {
				dir.y = Direction.Up;
			} else {
				dir.y = Direction.Down;
			}
		}

		if (Input.GetAxis("Horizontal") != 0){
			if (Input.GetAxis ("Horizontal") > 0) {
				dir.x = Direction.Right;
			} else {
				dir.x = Direction.Left;
			}
		}

		return dir;
	}
}
