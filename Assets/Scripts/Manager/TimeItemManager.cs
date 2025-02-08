using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeItemManager : MonoBehaviour
{
    private static TimeItemManager instance;
    public static TimeItemManager Instance => instance;

    private void Awake()
    {
        if (TimeItemManager.Instance != null) Debug.LogError("Only 1 Scrips TimeItemManager Allow Exist !!!");
        instance = this;
    }

    [Serializable]
    private struct TimeItem
    {
        public GameObject item;
        public Image slider;
        public TypeResourcesItem typeResourcesItem;
    }

    [SerializeField] private List<TimeItem> timeItems = new List<TimeItem>();

    public void ActiveItem(bool active,TypeResourcesItem typeResourcesItem)
    {
        timeItems.Find(item => item.typeResourcesItem == typeResourcesItem).item.SetActive(active);
        timeItems.Find(item => item.typeResourcesItem == typeResourcesItem).slider.fillAmount = 1f;
    }
    
    public void ChangeTimeItem(float value,TypeResourcesItem typeResourcesItem)
    {
        timeItems.Find(item => item.typeResourcesItem == typeResourcesItem).slider.fillAmount = value;
    }
}
