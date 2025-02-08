using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField] private TypeResourcesTag typeResourcesTag;
    [SerializeField] private TypeResourcesItem tyResourcesItem;
    [SerializeField] private TypeResourcesParticle typeResourcesParticle;
    [SerializeField] private TypeResourcesItemInteract typeResourcesItemInteract;
    [SerializeField] private TypeResourcesAudio typeResourcesAudio;
    [SerializeField] private int drop;
    [SerializeField] private bool destroy = true;
    [SerializeField] private float timeDestroy = -1;

    private void OnEnable()
    {
        if (timeDestroy < 0) return;
        StartCoroutine(OnDestroyObject());
    }

    private void OnDisable()
    {
        StopCoroutine(OnDestroyObject());
    }

    private IEnumerator OnDestroyObject()
    {
        yield return new WaitForSeconds(timeDestroy);
        DestroyGameObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(typeResourcesTag.ToString())) return;
        ItemManager.Instance.RemoveItem(drop, tyResourcesItem, transform,typeResourcesParticle, typeResourcesAudio);
        DestroyGameObject();
    }

    private void DestroyGameObject()
    {
        if (destroy)
        {
            Destroy(gameObject);
        }
        else
        {
            ItemManager.Instance.DisableItem(gameObject, typeResourcesItemInteract);
        }
    }
}
