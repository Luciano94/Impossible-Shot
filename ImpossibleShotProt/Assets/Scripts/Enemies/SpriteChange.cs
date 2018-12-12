using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour {

	[SerializeField] Sprite enemyIdle;
	[SerializeField] Sprite enemyScreaming;
	private SpriteRenderer enemySprite;
	private bool isScreaming;

	public bool IsScreaming{
		get{return isScreaming;}
	}

	private void Start() {
		enemySprite = GetComponentInChildren<SpriteRenderer>();
		isScreaming = false;
	}

	public void ChangeSpriteIdle(){

		enemySprite.sprite = enemyIdle;
		isScreaming = false;
	}

		public void ChangeSpriteScream(){
			enemySprite.sprite = enemyScreaming;
		isScreaming = true;
	}
}
