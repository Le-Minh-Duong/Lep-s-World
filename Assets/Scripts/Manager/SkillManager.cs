using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    public static SkillManager Instance => instance;

    [SerializeField] private List<TypeSkill> typeSkill = new List<TypeSkill>();
    [SerializeField] private List<TypeSkillDisable> typeSkillDisable = new List<TypeSkillDisable>();

    private void Awake()
    {
        if (SkillManager.Instance != null) Debug.LogError("Only 1 Scrips SkillManager Allow Exist !!!");
        instance = this;
    }

    public void ActiveEffect(Vector3 position, TypeResourcesParticle resourcesParticle, TypeResourcesAudio resourcesAudio)
    {
        EffectManager.Instance.SpawnParticle(position,resourcesParticle);
        AudioManager.Instance.SpawnAudio(resourcesAudio);
    }

    public void SpawnSkill(Vector3 position, Vector3 rotation,TypeResourcesSkill typeResource)
    {
        GameObject newGameObject = GetItem(typeResource);

        if (newGameObject == null)
        {
            TypeSkill game = typeSkill.Find(item => item.typeResourcesSkill == typeResource);
            if (game.item == null) return;
            newGameObject = Instantiate(game.item, game.itemParent);
        }

        newGameObject.transform.position = position;
        newGameObject.transform.rotation = Quaternion.Euler(rotation);
        newGameObject.SetActive(true);
    }

    public void DisableItem(GameObject item, TypeResourcesSkill tyResources)
    {
        int index = typeSkillDisable.FindIndex(it => it.typeResourcesSkill == tyResources);
        if (index < 0)
        {
            typeSkillDisable.Add(new TypeSkillDisable { item = new List<GameObject> { item }, typeResourcesSkill = tyResources });
        }
        else
        {
            typeSkillDisable[index].item.Add(item);
        }
        item.SetActive(false);
    }

    private GameObject GetItem(TypeResourcesSkill tyResources)
    {
        int index = typeSkillDisable.FindIndex(item => item.typeResourcesSkill == tyResources);
        if (index < 0) return null;
        if (typeSkillDisable[index].item.Count == 0) return null;

        GameObject newGameObject = typeSkillDisable[index].item[0];
        typeSkillDisable[index].item.Remove(newGameObject);
        return newGameObject;
    }
}

[Serializable]
public struct TypeSkill
{
    public GameObject item;
    public Transform itemParent;
    public TypeResourcesSkill typeResourcesSkill;
}

[Serializable]
public struct TypeSkillDisable
{
    public List<GameObject> item;
    public TypeResourcesSkill typeResourcesSkill;
}
