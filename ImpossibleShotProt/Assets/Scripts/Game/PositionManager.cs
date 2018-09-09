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

	[SerializeField] private float[,] positions;

	private void Awake() {
		initPositions();
	}
	
	private void initPositions(){
		positions = new float[7,2];
		for(int i= 0;i<7;i++){
			positions[i,0]=0;
			if(i>0)
				positions[i,1]=positions[i-1,1]+1.5f;
			else positions[i,1]=-5;
		}
	}

	public float getPosition(int width){
		bool encontre= false;
		int index = -1;
		float posx = -10;
		for(int i=0;i<7 && !encontre;i++){
			if(positions[i,0] == 0){
				encontre = true;
				positions[i,0] = 1;
				index = i;
				posx = positions[i,1];
			}
		}
		if(width > 1 && encontre && index < 7){
			encontre = false;
			positions[index,0]=0;
			for(int i = index; i<width && !encontre;i++){
				if(positions[i,0]==1)
					break;
				else if(i+1 == width)
					encontre = true;
			}
			if(encontre){
				for(int i=index;i<=width;i++){
					positions[i,0]=1;
				}
			}
		}
		return posx;
	}

	public void freePosition(float pos, int width){
		int index = 0;
		for(int i= 0;i<7;i++){
			if(positions[i,1] == pos){
				index = i;
				break;
			}
		}
		int cont =0;
		for(int i = index;i < 7 && cont<width;i++){
			positions[i,0] = 0;
			cont++;
		}
	}
}
