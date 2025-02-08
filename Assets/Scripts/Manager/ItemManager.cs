using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;
    public static ItemManager Instance => instance;

    [SerializeField] private List<TypeItem> typeItems = new List<TypeItem>();  
    [SerializeField] private List<TypeItemDisable> typeItemsDisable = new List<TypeItemDisable>();
    [SerializeField] private float rate = 100f;

    [SerializeField] private Animator gold;
    [SerializeField] private Animator seed;

    //[SerializeField] private float goldRotation = 0;
    //[SerializeField] private float addRotation = 1;

    private void Awake()
    {
        if (ItemManager.Instance != null) Debug.LogError("Only 1 Scrips ItemManager Allow Exist !!!");

        instance = this;
    }

    //private void FixedUpdate()
    //{
    //    goldRotation += addRotation;
    //    if(goldRotation >= 360)  goldRotation = 0;

    //    TransformAccessArray transformAccess = new TransformAccessArray(items.Count);

    //    for (int i = items.Count - 1; i >= 0; i--)
    //    {
    //        transformAccess.Add(items[i].transform);
    //    }

    //    UpdateRotation rotation = new UpdateRotation()
    //    {
    //        x = this.goldRotation
    //    };

    //    JobHandle jobHandle = rotation.Schedule(transformAccess);
    //    jobHandle.CompleteSliderUI();
        
    //    transformAccess.Dispose();
    //}

    public void AddItem(Animator newItem)
    {
        newItem.Play("Gold_Action",-1, gold.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    public void RemoveItem(int n, TypeResourcesItem typeResources, Transform item, TypeResourcesParticle typeResourcesParticle, TypeResourcesAudio typeResourcesAudio)
    { 
        UpdateResourcesUI.Instance.UpdateItem(n,typeResources);
        EffectManager.Instance.SpawnParticle(item.position,typeResourcesParticle);
        AudioManager.Instance.SpawnAudio(typeResourcesAudio);
    }

    public void SpawnItem(Vector3 position, float force, TypeResourcesItemInteract tyResources, bool anima,TypeResourcesParticle typeResourcesParticle,TypeResourcesAudio typeResourcesAudio,bool random)
    {
        EffectManager.Instance.SpawnParticle(position, typeResourcesParticle);
        AudioManager.Instance.SpawnAudio(typeResourcesAudio);

        if (tyResources == TypeResourcesItemInteract.No_Resource) return;
        GameObject newGameObject = null;
        if (random)
        {
            float n = UnityEngine.Random.Range(0,rate);
            foreach(var item in typeItems)
            {
                n -= item.rate;
                if(n<=0)
                {
                    tyResources = item.tyResourcesItem;
                    break;
                }
            }
        }

        newGameObject = GetItem(tyResources);
        

        if(newGameObject == null)
        {
            TypeItem game = typeItems.Find(item => item.tyResourcesItem == tyResources);
            if (game.item == null) return;

            newGameObject = Instantiate(game.item, game.itemParent);
        }

        newGameObject.transform.position = position;
        newGameObject.SetActive(true);
        newGameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * force);
        if (!anima || tyResources != TypeResourcesItemInteract.ItemPhysics) return;

        AddItem(newGameObject.GetComponent<Animator>());
    }

    public void DisableItem(GameObject item, TypeResourcesItemInteract tyResources)
    {
        int index = typeItemsDisable.FindIndex(it => it.tyResourcesItem == tyResources);
        if(index < 0)
        {
            typeItemsDisable.Add(new TypeItemDisable {item = new List<GameObject> {item },tyResourcesItem = tyResources});
        }
        else
        {
            typeItemsDisable[index].item.Add(item);
        }
        item.SetActive(false);
    }

    private GameObject GetItem(TypeResourcesItemInteract tyResources)
    {
        int index = typeItemsDisable.FindIndex(item => item.tyResourcesItem == tyResources);
        if(index < 0) return null;
        if (typeItemsDisable[index].item.Count == 0) return null;

        GameObject newGameObject = typeItemsDisable[index].item[0];
        typeItemsDisable[index].item.Remove(newGameObject);
        return newGameObject;
    }
}

//public struct UpdateRotation : IJobParallelForTransform
//{
//    public float x;

//    public void Execute(int index, TransformAccess transform)
//    {
//        transform.localRotation = Quaternion.Euler(0,x,0);
//    }
//}

[Serializable]
public struct TypeItem
{
    public GameObject item;
    public Transform itemParent;
    public TypeResourcesItemInteract tyResourcesItem;
    public float rate;
}

//[Serializable]
public struct TypeItemDisable
{
    public List<GameObject> item;
    public TypeResourcesItemInteract tyResourcesItem;
}