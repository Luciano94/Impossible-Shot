using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class ScoreFileManager{
    
    public static DataManager dataManager = new DataManager();

    public static void NewGame(){
        dataManager.data.score = 0;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/impShot.data");
        bf.Serialize(file, dataManager);
        file.Close();
    }

    public static void SaveScore(){
        dataManager.setScore();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite (Application.persistentDataPath + "/impShot.data");
        bf.Serialize(file, dataManager);
        file.Close();
    }

    public static int LoadScore(){
        if(File.Exists(Application.persistentDataPath + "/impShot.data")){
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/impShot.data", FileMode.Open);
            dataManager = (DataManager)bf.Deserialize(file);
            file.Close();
        }else{
            NewGame();
        }
        return dataManager.data.score;
    }
}
