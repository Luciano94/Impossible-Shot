public enum Direction{
	Up,
	Down,
	Left,
	Right,
	None
}
interface IInput
{
	void Awake();
	Direction GetDirection();
	void Update();
	//android alternative only
	void GoUp ();
	void GoDown();
	void GoLeft();
	void GoRight();
}


