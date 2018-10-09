using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAndroidAlternative : IInput {

	private Direction command;

	public void Awake(){
		command = Direction.None;
	}

	public Direction GetDirection(){
		Direction dir;
		dir = command;
		command = Direction.None;
		return dir;
	}



	public void GoLeft(){command = Direction.Left;}
	public void GoRight(){command = Direction.Right;}
	public void GoUp(){command = Direction.Up;}
	public void GoDown(){command = Direction.Down;}
	//Placeholder
	public void Update(){}
}
