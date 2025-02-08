using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : MonoBehaviour
{

    private static LevelManager instance;
    public static LevelManager Instance => instance;

    private LevelData levelData;
    [SerializeField] private GameObject loading;
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image imagex;

    [SerializeField] private List<GetPlayer> getPlayers = new List<GetPlayer>();
    [SerializeField] private bool spawn = false;
    [SerializeField] private int index = 0;

    private void Start()
    {
        imagex.sprite = getPlayers[index].image;
        if (!spawn) return;
        Instantiate(getPlayers[index].player,Vector3.zero,Quaternion.identity);
    }

    private void Awake()
    {
        if (LevelManager.Instance != null) Debug.LogError("Only 1 Scrips LevelManager Allow Exist !!!");
        instance = this;
        levelData = LoadDataLevelStart();

        string namePath = Application.persistentDataPath + GameManager.Instance.GetNamePathSelectPlayer();
        IntData floatData = JsonUtility.FromJson<IntData>(File.ReadAllText(namePath));
        index = floatData.floatValue;
    }

    public Sprite GetSprite()
    {
        return getPlayers[index].spriteUI;
    }

    public void SaveDataLevel(int level, int start)
    {
        string namePath = Application.persistentDataPath + GameManager.Instance.GetNamePathLevel();
        SetLevelDefault(namePath);

        LevelData levelData = JsonUtility.FromJson<LevelData>(File.ReadAllText(namePath));
        if(level > levelData.listLevel.Count)
        {
            levelData.listLevel.Add(new Level {level = level, start = start });
        }
        else
        {
            levelData.listLevel[level - 1].start = start;
        }

        string json = JsonUtility.ToJson(levelData);
        File.WriteAllText(namePath, json);
    }
    
    private LevelData LoadDataLevelStart()
    {
        string namePath = Application.persistentDataPath + GameManager.Instance.GetNamePathLevel();
        SetLevelDefault(namePath);

        return JsonUtility.FromJson<LevelData>(File.ReadAllText(namePath));
    }
    
    public Level LoadDataLevel(int level)
    {
        if (level > levelData.listLevel.Count) return null;
        return levelData.listLevel[level - 1];
    }

    private void SetLevelDefault(string namePath)
    {
        if (!File.Exists(namePath))
        {
            LevelData audioDefault = new LevelData { listLevel = new List<Level> { new Level { level = 1, start = 0 }, } };
            string jsonDefault = JsonUtility.ToJson(audioDefault);
            File.WriteAllText(namePath, jsonDefault);
        }
    }  
    
    public void ActiveLoading(TypeResourcesLevel typeResourcesLevel)
    {
        loading.SetActive(true);
        StartCoroutine(Loading(typeResourcesLevel));
    }

    private IEnumerator Loading(TypeResourcesLevel typeResourcesLevel)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(typeResourcesLevel.ToString());

        float progress = 0f;
        while (!asyncOperation.isDone)
        {
            progress = asyncOperation.progress/0.9f;
            slider.value = progress * 100f;
            text.text = $"{(int)(progress * 100f)} %";
            yield return new WaitForEndOfFrame();
        }
    }
}

[Serializable]
public class LevelData
{
    public List<Level> listLevel;
}

[Serializable]
public class Level
{
    public int level;
    public int start;
}

[Serializable]
public struct GetPlayer
{
    public GameObject player;
    public Sprite spriteUI;
    public Sprite image;
}

