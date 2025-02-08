using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    [SerializeField] private int targetFrame;

    //private const string _keyAudioBackground = "AudioBackground";
    //private const string _keyAudioEffect = "AudioEffect";
    private const string _nameFolderAudio = "/Audio.txt";
    private const string _nameFolderLevel = "/Level.txt";
    private const string _nameFolderSelectPlayer = "/Player.txt";

    [Serializable]
    private struct AudioData
    {
        public float effect;
        public float background;
    }

    private void Start()
    {
        PlayerDefault();
        Application.targetFrameRate = targetFrame;
    }

    private void Awake()
    {
        if (GameManager.Instance != null) Debug.LogError("Only 1 Scrips GameManager Allow Exist !!!");
        instance = this;
    }

    private void PlayerDefault()
    {
        string namePath = Application.persistentDataPath + _nameFolderSelectPlayer;

        if (!File.Exists(namePath))
        {
            IntData floatData = new IntData() { floatValue = 0 };
            string data = JsonUtility.ToJson(floatData);
            File.WriteAllText(namePath, data);
        }
    }

    public void SaveDataAudio(float data, int n)
    {
        string namePath = Application.persistentDataPath + _nameFolderAudio;
        SetAudioDefault(namePath);

        AudioData getAudio = JsonUtility.FromJson<AudioData>(File.ReadAllText(namePath));

        if(n == 0)
        {
            getAudio.effect = data;
        }
        else
        {
            getAudio.background = data;
        }
        string json = JsonUtility.ToJson(getAudio);
        File.WriteAllText(namePath, json);
    }
    
    public float LoadDataAudio(int n)
    {
        string namePath = Application.persistentDataPath + _nameFolderAudio;
        SetAudioDefault(namePath);

        AudioData getAudio = JsonUtility.FromJson<AudioData>(File.ReadAllText(namePath));

        if(n == 0)
        {
            return getAudio.effect;
        }
        return getAudio.background;
    }

    private void SetAudioDefault(string namePath)
    {

        if (!File.Exists(namePath))
        {
            AudioData audioDefault = new AudioData { effect = 1f, background = 1f};
            string jsonDefault = JsonUtility.ToJson(audioDefault);
            File.WriteAllText(namePath, jsonDefault);
        }
    }

    public string GetNamePathLevel()
    {
        return _nameFolderLevel;
    }
    
    public string GetNamePathSelectPlayer()
    {
        return _nameFolderSelectPlayer;
    }
    
    //public void SaveDataFloat(float data, string key)
    //{
    //    PlayerPrefs.SetFloat(key, data);
    //    PlayerPrefs.Save();
    //}

    //public float LoadDataFloat(string key)
    //{
    //    if(!PlayerPrefs.HasKey(key))
    //    {
    //        PlayerPrefs.SetFloat (key, 1f);
    //    }

    //    return PlayerPrefs.GetFloat(key);
    //}

    //public string GetKeyAudioBackground()
    //{
    //    return _keyAudioBackground;
    //}

    //public string GetKeyAudioEffect()
    //{
    //    return _keyAudioEffect;
    //}

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
    
    public void LoadLobbyScene()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void LoadSelectPlayerScene()
    {
        SceneManager.LoadScene("SelectPlayer");
    }
}
