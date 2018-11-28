using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class UIAnimatedElement : MonoBehaviour {

	[SerializeField] private float speed = 1;
	[SerializeField] private bool moves;
	[SerializeField] private float initialX;
	[SerializeField] private float initialY;


	private Vector2 initialPositionVector;
	private Vector2 targetPositionVector;
	private RectTransform rectTransform;
	private Button button;
	private bool wasInteractable;
	private bool animating;
	private float lerpState;

	private UIElementAnimation director;
	private int elementID;
	void Start () {
		button = GetComponent<Button>();
		if(button){ wasInteractable = button.interactable; button.interactable = false;}
	

		rectTransform = GetComponent<RectTransform>();
		if(moves){
			initialPositionVector = new Vector2(initialX * 100, initialY * 100);
			targetPositionVector = rectTransform.anchoredPosition;
			rectTransform.anchoredPosition = initialPositionVector;
		}
		animating = false;
		lerpState = 0;
	}

	void Update () {
		if(animating){
			lerpState += Time.unscaledDeltaTime * speed * 2;
			if(lerpState >= 1){Stop(); return;}
			if(moves){
				rectTransform.anchoredPosition = Vector3.Lerp(initialPositionVector,targetPositionVector,lerpState);
			}
		}
	}

	public void Animate(){
		animating = true;
	}

	void Stop(){
		director.animationDone(elementID);
		animating = false;
		rectTransform.anchoredPosition = targetPositionVector;
		if(button){ button.interactable = wasInteractable;}
	}

	public void SetAnimation(UIElementAnimation animator, int ID){
		director = animator;
		elementID = ID;
	}

	public void Reset(){
		lerpState = 0;
		if(moves){ rectTransform.anchoredPosition = initialPositionVector;}
		if(button){ wasInteractable = button.interactable; button.interactable = false;}
	}
}
