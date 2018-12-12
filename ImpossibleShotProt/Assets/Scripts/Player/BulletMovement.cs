using UnityEngine;

public class BulletMovement : MonoBehaviour {
	private Vector3 MatrixCenter;
	[SerializeField] private float DistanceToCenter;
	[SerializeField][Range (0f,1f)] private float IgnorableDistance;
	[SerializeField][Range (1,25)] private int TransitionSharpness;
	[SerializeField] private GameObject Bullet; 
	private float LerpState = 0;
	private int x = 0;
	private int y = 0;
	private int yNext = 0;
	private int xNext = 0;
	private bool moving = false;
	private Vector3 InitialPosition;

	private void Awake() {
		MatrixCenter = transform.position;
		InitialPosition = transform.position;
	}
    
	void Update () {
		//si no me estoy ya moviento
		if (!moving) {
			//detecto orden de movimiento
			moveDetection();
			if(xNext != x || yNext != y){
				moving = true; //de ser necesario comienzo a moverme;
				LerpState = 0;
				InitialPosition = transform.position;
			}
		} else {
			moveAction ();
		}
	}
    
	private void moveDetection(){
		Direction dir = InputManager.Instance.GetDirection ();
		switch (dir) {
		case Direction.Up:
			yNext += 1;
			break;
		case Direction.Down:
			yNext -= 1;
			break;
		case Direction.Right:
			xNext += 1;
			break;
		case Direction.Left:
			xNext -= 1;
			break;
		default:
			break;
		}

		if (xNext > 1) {xNext = 1;}
		if (xNext < -1) {xNext = -1;}
		if (yNext > 1) {yNext = 1;}
		if (yNext < -1) {yNext = -1;}
	}

	private void moveAction(){
		float yTarget = MatrixCenter.y + (DistanceToCenter * yNext);
		float xTarget = MatrixCenter.x + (DistanceToCenter * xNext);
		Vector3 Target = new Vector3 (xTarget, yTarget, transform.position.z);

		LerpState += TransitionSharpness * Time.deltaTime * Time.timeScale;
		if (LerpState > 1.0f) {LerpState = 1.0f;}

		transform.position = Vector3.Lerp (InitialPosition, Target, LerpState);
		if(LerpState >= 1.0f){
			y = yNext;
			x = xNext; 
			moving = false;
		}
	}

	public Vector2Int getPositionInts(){
		return new Vector2Int(x,y);
	}

    public Vector3[] getCorners() {
        Vector3[] vec = new Vector3[4];
        vec[0] = new Vector3(MatrixCenter.x + DistanceToCenter, MatrixCenter.y + DistanceToCenter, InitialPosition.z);
        vec[1] = new Vector3(MatrixCenter.x + DistanceToCenter, MatrixCenter.y - DistanceToCenter, InitialPosition.z);
        vec[2] = new Vector3(MatrixCenter.x - DistanceToCenter, MatrixCenter.y + DistanceToCenter, InitialPosition.z);
        vec[3] = new Vector3(MatrixCenter.x - DistanceToCenter, MatrixCenter.y - DistanceToCenter, InitialPosition.z);
        return vec;
    }
}
