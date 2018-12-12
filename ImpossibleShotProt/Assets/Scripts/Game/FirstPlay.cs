using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstPlay : MonoBehaviour {
	
	private static FirstPlay instance;

    public static FirstPlay Instance {
        get {
            instance = FindObjectOfType<FirstPlay>();
            if(instance == null) {
                GameObject go = new GameObject("FirstPlay");
                instance = go.AddComponent<FirstPlay>();
            }
            return instance;
        }
    }
	
	private bool firstPlay = true;
	private bool restart = false;

	public void play(){
		firstPlay = false;
	}

	public bool Played{
		get{return firstPlay;}
	}

	public void RestartGame(){
		restart = true;
		SoundManager.Instance.StopSound();
		SoundManager.Instance.GameStart(false);
		SceneManager.LoadScene(0);
	}

	private void Awake() {
		DontDestroyOnLoad(gameObject);
	}

	public void OnLevelWasLoaded(int level){
		if(restart){
			MenuManager.Instance.StartGame();
			restart = false;
		}
	}
}
