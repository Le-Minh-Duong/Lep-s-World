using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UpdateResourcesUI : MonoBehaviour
{
    private static UpdateResourcesUI instance;
    public static UpdateResourcesUI Instance => instance;

    [SerializeField] private List<TypeItemUI> typeItemUIs = new List<TypeItemUI>();
    [SerializeField] private List<GameObject> hp = new List<GameObject>();
    [SerializeField] private DamagePlayer player;
    [SerializeField] private List<GameObject> gold = new List<GameObject>();

    private void Awake()
    {
        if (UpdateResourcesUI.Instance != null) Debug.LogError("Only 1 Scrips UpdateResourcesUI Allow Exist !!!");
        instance = this;
    }

    public void UpdateItem(int n,TypeResourcesItem typeResources)
    {
        if (typeResources == TypeResourcesItem.GoldSpecialUI)
        {
            UpdateGold(n);
            return;
        }
        else if(typeResources == TypeResourcesItem.HpUI)
        {
            UpdateHP(n);
            player.GetHP((float)n);
            return;
        }
        int index = typeItemUIs.FindIndex(item => item.typeResourcesItem == typeResources);
        if (index < 0) return;
        typeItemUIs[index].count += n;
        typeItemUIs[index].textMeshProUGUI.text = $"{typeItemUIs[index].count}";
    }

    public int GetCountItem(TypeResourcesItem typeResources)
    {
        int index = typeItemUIs.FindIndex(item => item.typeResourcesItem == typeResources);
        if (index < 0) return 0;

        return typeItemUIs[index].count;
    }

    private void UpdateGold(int n)
    {
        for (int i = 0; i < gold.Count; i++)
        {
            if (!gold[i].activeSelf)
            {
                if (n <= 0) break;
                gold[i].SetActive(true);
                n--;
            }
        }
    }

    public int GetNGold()
    {
        return gold.TakeWhile(item => item.activeSelf).Count();
    }

    public int GetNHp()
    {
        return hp.TakeWhile(item => item.activeSelf).Count();
    }

    public void UpdateHP(int n)
    {
        if(n > 0)
        {
            if (hp[hp.Count- 1].activeSelf) return;

            for (int i = 0; i < hp.Count; i++)
            {
                if (!hp[i].activeSelf)
                {
                    if (n <= 0) break;
                    hp[i].SetActive(true);
                    n--;
                }
            }
            return;
        }

        for (int i = hp.Count - 1; i >= 0; i--)
        {
            if (hp[i].activeSelf)
            {
                if (n >= 0) break;
                hp[i].SetActive(false);
                n++;
            }
        }
    }

    public void GetDamagePlayer(DamagePlayer damagePl)
    {
        this.player = damagePl;
    }
}

[Serializable]
public class TypeItemUI
{
    public int count = 0;
    public TextMeshProUGUI textMeshProUGUI;
    public TypeResourcesItem typeResourcesItem;
}
