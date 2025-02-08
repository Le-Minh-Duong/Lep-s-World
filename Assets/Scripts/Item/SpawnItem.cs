using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private TypeResourcesItemInteract typeResourcesItemInteract;
    [SerializeField] private TypeResourcesParticle typeResourcesParticle;
    [SerializeField] private TypeResourcesAudio typeResourcesAudio;
    [SerializeField] private float force;
    [SerializeField] private bool isAnimation = true;
    [SerializeField] private bool random = true;

    public void Spawn()
    {
        ItemManager.Instance.SpawnItem(transform.position, force, typeResourcesItemInteract, isAnimation, typeResourcesParticle, typeResourcesAudio,random);
        Destroy(gameObject);
    }
}
