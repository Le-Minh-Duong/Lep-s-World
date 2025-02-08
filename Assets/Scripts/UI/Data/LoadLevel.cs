using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private Level getLevel;
    [SerializeField] private TypeResourcesLevel loadLevel;
    [SerializeField] private Button button;
    [SerializeField] private GameObject key;
    [SerializeField] private List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        getLevel = LevelManager.Instance.LoadDataLevel(getLevel.level);
        if (getLevel == null) return;
        key.SetActive(false);
        for (int i = 0; i < getLevel.start; i++)
        {
            gameObjects[i].SetActive(true);
        }
        button.interactable = true;
        button.onClick.AddListener(LoadLevelX);
    }

    private void LoadLevelX()
    {
        LevelManager.Instance.ActiveLoading(loadLevel);
    }
}