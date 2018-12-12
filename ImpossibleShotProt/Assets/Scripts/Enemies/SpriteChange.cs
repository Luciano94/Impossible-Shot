using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour {

	[SerializeField] Sprite enemyIdle;
	[SerializeField] Sprite enemyScreaming;
	private SpriteRenderer enemySprite;
	private bool isScreaming;

	private void Start() {
		enemySprite = GetComponentInChildren<SpriteRenderer>();
		isScreaming = false;
	}

	public void ChangeSprite(){
		if(isScreaming)
			enemySprite.sprite = enemyIdle;
		else
			enemySprite.sprite = enemyScreaming;
	}

	public void Reset(){
		Invoke("ChangeSprite", 3.0f);
	}
}
