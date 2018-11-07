using UnityEngine;
public class TrailColorTransition : MonoBehaviour {

	[SerializeField] private Color InitialColor;
	[SerializeField] private Color TargetColor;

	private ParticleSystem.MainModule pSystem;
	private float lerpState;
	void Start () {
		pSystem = GetComponent<ParticleSystem>().main;
		lerpState = 0.0f;
	}
	public void ColorChange(float _lerpState){
		lerpState = _lerpState;
		if(lerpState < 0){ lerpState = 0;}
		if(lerpState > 1){ lerpState = 1;}
		pSystem.startColor = Color.Lerp(InitialColor,TargetColor,lerpState);
	}
}
