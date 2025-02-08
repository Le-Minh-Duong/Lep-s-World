using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeActionItem : MonoBehaviour
{
    [SerializeField] private TypeResourcesItem typeResourcesItem;
    [SerializeField] private float time;
    private float timeCurrent;

    private void Start()
    {
        TimeItemManager.Instance.ActiveItem(true,typeResourcesItem);
        ResetTime();
    }

    public void ResetTime()
    {
        timeCurrent = time;
    }

    public TypeResourcesItem GetTypeResourcesItem()
    {
        return typeResourcesItem;
    }

    private void FixedUpdate()
    {
        timeCurrent -= Time.fixedDeltaTime;
        TimeItemManager.Instance.ChangeTimeItem(timeCurrent/time,typeResourcesItem);
        if (timeCurrent > 0) return;

        TimeItemManager.Instance.ActiveItem(false, typeResourcesItem);
        Destroy(gameObject);
    }
}
