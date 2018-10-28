using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour {

	private static bool wasHit = false;
	private static bool registerOnce = true;

	

	public static bool WasHit(){
		return wasHit;
	}

	[SerializeField] private Color ShineColor;
	[SerializeField] private int Sharpness;
	private Color initColor;

	private float lerpState = 0;
	private bool shiningUp = false;
	private bool shiningDown = false;
	private SpriteRenderer sprite;

	private void Awake(){
		wasHit = false;
		registerOnce = true;
	}
	private void Start(){
		sprite = GetComponentInChildren<SpriteRenderer>();
		initColor = sprite.color;
	}

	private void Update(){
		if(shiningUp){
			lerpState += Time.deltaTime * Sharpness;
			if(lerpState >= 1){lerpState = 1; shiningUp = false; shiningDown = true;}
			sprite.color = Color.Lerp(initColor, ShineColor, lerpState);
		} else if(shiningDown){
			lerpState -= Time.deltaTime * Sharpness;
			if(lerpState <= 0){lerpState = 0; shiningDown = false;}
			sprite.color = Color.Lerp(initColor, ShineColor, lerpState);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if(registerOnce && other.gameObject.tag == "TutorialCollider"){
			TutorialManager.Instance.TutorialEnemyEnter();
			Shine();
			registerOnce = false;
		}

		if(other.gameObject.tag == "Bullet"){
			wasHit = true;
		}
	}

	private void Shine(){
		shiningUp = true;
	}

	
}
