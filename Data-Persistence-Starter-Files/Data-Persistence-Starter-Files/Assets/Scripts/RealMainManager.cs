using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RealMainManager : MonoBehaviour
{
    public static RealMainManager Instance;

    public string nome;
    public string nomeAttuale;

    public int score;
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScore();
    }

    [System.Serializable]

    class SaveData
    {
        public string nome;
        public int score;
    }

    public void saveScore()
    {
        SaveData data = new SaveData();
        data.nome = nome;
        data.score = score;

        string json = JsonUtility.ToJson(data);
  
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            nome = data.nome;
            score = data.score;
        }
        else
        {
            score = 0;
        }
    }
}
