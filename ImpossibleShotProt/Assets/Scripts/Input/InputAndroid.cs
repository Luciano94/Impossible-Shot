using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputAndroid : IInput {
	private Direction command;

	private Vector2 fingerDown;
	private Vector2 fingerUp;
	public bool SwipeReleased = true;
	public float MinumumSwipe = 20f;

	public void Awake(){
		command = Direction.None;
	}

	public Direction GetDirection(){
		Direction dir;
		dir = command;
		command = Direction.None;
		return dir;
	}

	public void Update(){
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fingerDown = touch.position;
				fingerUp = touch.position;
			}

			//detecta swipe mientras todavía hace contacto el dedo
			if (touch.phase == TouchPhase.Moved)
			{
					fingerUp = touch.position;
				if (SwipeReleased) {
					checkSwipe ();
				}
			}

			//detecta el swipe cuando termina el contacto
			if (touch.phase == TouchPhase.Ended)
			{
				SwipeReleased = true;
			}
		}
	}

	void checkSwipe()
	{
		//Check if Vertical swipe
		if (verticalMove() > MinumumSwipe && verticalMove() > horizontalValMove())
		{
			//Debug.Log("Vertical");
			if (fingerUp.y - fingerDown.y > 0)//up swipe
			{
				command = Direction.Up;
			}
			else if (fingerUp.y - fingerDown.y < 0)//Down swipe
			{
				command = Direction.Down;
			}
			fingerDown = fingerUp;
			SwipeReleased = false;
		} else

		//Check if Horizontal swipe
		if (horizontalValMove() > MinumumSwipe && horizontalValMove() > verticalMove())
		{
			//Debug.Log("Horizontal");
			if (fingerUp.x - fingerDown.x > 0)//Right swipe
			{
				command = Direction.Right;
			}
			else if (fingerUp.x - fingerDown.x < 0)//Left swipe
			{
				command = Direction.Left;
			}
			fingerDown = fingerUp;
			SwipeReleased = false;
		}
	}
	float verticalMove()
	{
		return Mathf.Abs(fingerUp.y - fingerDown.y);
	}

	float horizontalValMove()
	{
		return Mathf.Abs(fingerUp.x - fingerDown.x);
	}
	//Placeholders do not use
	public void GoUp (){}
	public void GoDown(){}
	public void GoLeft(){}
	public void GoRight(){}
}
