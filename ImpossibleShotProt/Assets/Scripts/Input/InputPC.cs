using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPC : IInput {
	public Direction GetDirection(){
		Direction dir;
		dir = Direction.None;
		if (Input.GetAxis("Vertical") != 0){
			if (Input.GetAxis ("Vertical") > 0) {
				dir = Direction.Up;
			} else {
				dir = Direction.Down;
			}
		} else if (Input.GetAxis("Horizontal") != 0){
			if (Input.GetAxis ("Horizontal") > 0) {
				dir = Direction.Right;
			} else {
				dir = Direction.Left;
			}
		}

		return dir;
	}

	//Placeholders
	public void Update(){}
	public void Awake(){}
	public void GoUp (){}
	public void GoDown(){}
	public void GoLeft(){}
	public void GoRight(){}
}
