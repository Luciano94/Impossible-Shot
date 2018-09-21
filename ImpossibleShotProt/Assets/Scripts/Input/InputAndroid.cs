using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputAndroid : IInput {
	private Direction x;
	private Direction y;

	private Vector2 fingerDown;
	private Vector2 fingerUp;
	public bool SwipeReleased = true;
	public float SWIPE_THRESHOLD = 20f;

	public void Awake(){
		x = Direction.None;
		y = Direction.None;
	}

	public DirectionVec GetDirection(){
		DirectionVec dir;
		dir.x = x;
		dir.y = y;
		x = Direction.None;
		y = Direction.None;
		//Debug.Log (x.ToString() + " " + y.ToString());
		return dir;
	}

	public void Update(){
		foreach (Touch touch in Input.touches)
		{
			if (touch.phase == TouchPhase.Began)
			{
				fingerUp = touch.position;
				fingerDown = touch.position;
			}

			//detecta swipe mientras todavía hace contacto el dedo
			if (touch.phase == TouchPhase.Moved)
			{
					fingerDown = touch.position;
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
		if (verticalMove() > SWIPE_THRESHOLD && verticalMove() > horizontalValMove())
		{
			//Debug.Log("Vertical");
			if (fingerDown.y - fingerUp.y > 0)//up swipe
			{
				y = Direction.Up;
			}
			else if (fingerDown.y - fingerUp.y < 0)//Down swipe
			{
				y = Direction.Down;
			}
			fingerUp = fingerDown;
			SwipeReleased = false;
		} else

		//Check if Horizontal swipe
		if (horizontalValMove() > SWIPE_THRESHOLD && horizontalValMove() > verticalMove())
		{
			//Debug.Log("Horizontal");
			if (fingerDown.x - fingerUp.x > 0)//Right swipe
			{
				x = Direction.Right;
			}
			else if (fingerDown.x - fingerUp.x < 0)//Left swipe
			{
				x = Direction.Left;
			}
			fingerUp = fingerDown;
			SwipeReleased = false;
		}
	}
	float verticalMove()
	{
		return Mathf.Abs(fingerDown.y - fingerUp.y);
	}

	float horizontalValMove()
	{
		return Mathf.Abs(fingerDown.x - fingerUp.x);
	}
}
