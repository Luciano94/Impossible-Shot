using UnityEngine;

[System.Serializable]
public class DataManager{

	[System.Serializable] 
	public struct Data{
		public int score;
	}

	public Data data;
	public DataManager(){}

	public void setScore(){
		data.score = GameManager.Instance.Score;
	} 
}
