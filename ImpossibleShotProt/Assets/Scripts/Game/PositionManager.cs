using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionManager : MonoBehaviour {

	private static PositionManager instance;

	public static PositionManager Instance{
		get{
			instance = FindObjectOfType<PositionManager>();
			if(instance == null)
			{
				GameObject go = new GameObject("PositionManager");
				instance = go.AddComponent<PositionManager>();
			}
			return instance;
		}
	}

	private bool[] positions;

	private void Awake() {
		positions = new bool[7];
		for(int i= 0;i<7;i++)
			positions[i]= false;
	}

	public int getPosition(int width){
		int pos;
		bool encontre = false;
		do{
			pos = Random.Range(0,6);
			if(width == 1 && !positions[pos])
				encontre = true;
			else if(pos < 6)
					if(!positions[pos] && !positions[pos+1])
						encontre = true;
		}while(!encontre);
		return pos;
	}

	public void freePosition(int pos, int width){
		for(int i = pos;i<width;i++)
			positions[i]=false;
	}
}
