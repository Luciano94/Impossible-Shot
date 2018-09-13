using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance {
        get {
            instance = FindObjectOfType<GameManager>();
            if(instance == null) {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
            }
            return instance;
        }
    }

    [SerializeField] GameObject spawnPattern;
    [SerializeField] float terrainSpeed = 50f;
    [SerializeField] float fOVPerLevel = 5f;
    [SerializeField] float maxFOV = 80f;

    public float TerrainSpeed {
        get { return terrainSpeed; }
        set { terrainSpeed += value; }
    }

    public void Death() {
        SceneManager.LoadScene(0);
    }

    private void Awake() {
        spawnPattern.GetComponent<SpawnPattern>().Spawn();
    }
}
