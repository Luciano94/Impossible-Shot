using UnityEngine;
public class TrailColorTransition : MonoBehaviour {

	[SerializeField] private Color InitialColor;
	[SerializeField] private Color TargetColor;

	[Header ("Particulas por estado")]
	private ParticleSystem pSystem;
	private ParticleSystem.MainModule colorSystem;
	private ParticleSystem.EmissionModule emissionSystem;
	private float lerpState;
	private int emmision = 1;

	void Start () {
		pSystem = GetComponent<ParticleSystem>();
		colorSystem = pSystem.main;
		lerpState = 0.0f;
		emissionSystem = pSystem.emission;
		emissionSystem.rateOverTime = 1;
	}

	public void ColorChange(float _lerpState){
		emmision ++;
		emissionSystem.rateOverTime = emmision;
		lerpState = _lerpState;
		if(lerpState < 0){ 
			lerpState = 0;
		}else if(lerpState > 1){ 
			lerpState = 1;
		}
		colorSystem.startColor = Color.Lerp(InitialColor,TargetColor,lerpState);
	}

	public void DesactivePwUp(){
		emmision = 1;
		emissionSystem.rateOverTime = emmision;
	}
}
