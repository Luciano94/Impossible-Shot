using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObstacle : MonoBehaviour {
	private static bool registerOnce = true;


	private float speed;

	private Product product;
	private GameManager gM;

	private bool shiningUp = false;
	private bool shiningDown = false;
	private Color[] initColors;
	private Material[] mats;

	[SerializeField] private Color ShineColor;
	[SerializeField] private int Sharpness;

	private float lerpState;
	void Start(){
		registerOnce = true;
		gM = GameManager.Instance;
		speed = gM.TerrainSpeed;
		product = GetComponent<Product> ();

		MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
		mats = new Material[meshes.Length];
		initColors = new Color[meshes.Length];

		for(int i = 0; i < meshes.Length; i++){
			mats[i] = meshes[i].material;
			initColors[i] = mats[i].GetColor("_Color");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(registerOnce && other.gameObject.tag == "TutorialCollider"){
			TutorialManager.Instance.TutorialObstacleEnter();
			shiningUp = true;
			registerOnce = false;
		}

		if(other.gameObject.tag == "Bullet"){
			TutorialManager.Instance.PlayerHitTutorialObstacle();

		}
	}

	private void Update(){
		if(shiningUp){
			lerpState += Time.deltaTime * Sharpness;
			if(lerpState >= 1){lerpState = 1; shiningUp = false; shiningDown = true;}
			for(int i = 0; i < mats.Length; i++){
				mats[i].SetColor("_Color",Color.Lerp(initColors[i],ShineColor,lerpState));
			}
		} else if(shiningDown){
			lerpState -= Time.deltaTime * Sharpness;
			if(lerpState <= 0){lerpState = 0; shiningDown = false;}
			for(int i = 0; i < mats.Length; i++){
				mats[i].SetColor("_Color",Color.Lerp(initColors[i],ShineColor,lerpState));
			}
		}
	}
	void LateUpdate () {
		if (product.IsActive()){
			speed = gM.TerrainSpeed;
			transform.Translate (Vector3.back * speed * Time.deltaTime);
		}
	}
}
