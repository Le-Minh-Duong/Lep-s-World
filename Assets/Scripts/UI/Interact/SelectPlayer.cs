using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectPlayer : MonoBehaviour
{

    [SerializeField] private int count;
    [SerializeField] private RectTransform content;
    [SerializeField] private float distance;
    [SerializeField] private Button buttonPrevious;
    [SerializeField] private Button buttonNext;

    private int indexCurrent = 0;

    private void Start()
    {
        LoadPlayer();
        buttonNext.onClick.AddListener(ChangePlayerNext);
        buttonPrevious.onClick.AddListener(ChangePlayerPrevious);
    }

    private void LoadPlayer()
    {
        string namePath = Application.persistentDataPath + GameManager.Instance.GetNamePathSelectPlayer();
        if (!File.Exists(namePath))
        {
            IntData newdata = new IntData() { floatValue = 0 };
            string data = JsonUtility.ToJson(newdata);
            File.WriteAllText(namePath, data);
        }
        IntData floatData = JsonUtility.FromJson<IntData>(File.ReadAllText(namePath));
        indexCurrent = floatData.floatValue;
        content.localPosition = Vector3.left * (distance * indexCurrent);
    }

    private void ChangePlayerNext()
    {
        string namePath = Application.persistentDataPath + GameManager.Instance.GetNamePathSelectPlayer();
        indexCurrent++;

        if (indexCurrent >= count)
        {
            indexCurrent = 0;
        }

        content.localPosition = Vector3.left * (distance * indexCurrent);
        string data = JsonUtility.ToJson(new IntData() {floatValue = indexCurrent });
        File.WriteAllText(namePath,data);
    }
    
    private void ChangePlayerPrevious()
    {
        string namePath = Application.persistentDataPath + GameManager.Instance.GetNamePathSelectPlayer();
        indexCurrent--;

        if (indexCurrent < 0)
        {
            indexCurrent = count - 1;
        }

        content.localPosition = Vector3.left * (distance * indexCurrent);
        string data = JsonUtility.ToJson(new IntData() {floatValue = indexCurrent });
        File.WriteAllText(namePath,data);
    }
}

[System.Serializable]
public struct IntData
{
    public int floatValue;
}