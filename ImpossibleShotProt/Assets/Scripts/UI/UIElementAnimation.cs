using UnityEngine;

public class UIElementAnimation : MonoBehaviour {
	[SerializeField] private UIAnimatedElement[] elements;
	[SerializeField] private bool[] shouldNextWait;
	private bool[] doneAnimating;
	private bool animating = false;

	private void Awake(){
		doneAnimating = new bool[elements.Length];
		for(int i = 0; i < elements.Length; i++){
			doneAnimating[i] = false;
			elements[i].SetAnimation(this,i);
		}
		shouldNextWait[shouldNextWait.Length-1] = false;
	}

	void OnEnable(){
		animating = true;
	}

	void OnDisable(){
		for(int i = 0; i < doneAnimating.Length; i++){
			elements[i].Reset();
			doneAnimating[i] = false;
		}
	}
	
	public void animationDone(int ID){
		doneAnimating[ID] = true;
	}

	private bool AllDone(){
		for(int i = 0; i < elements.Length; i++){
			if(doneAnimating[i] == false){ return false;}
		}
		return true;
	}

	void LateUpdate(){
		if(animating){
			for(int i = 0; i < elements.Length; i++){
				if(!doneAnimating[i]){
					elements[i].Animate();
					if(shouldNextWait[i]){ 
						return;
					}
				}
			}
			if(AllDone()){
				animating = false;
			}
		}
	}
}
