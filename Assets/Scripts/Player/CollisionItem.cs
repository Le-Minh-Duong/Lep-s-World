using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionItem : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private TypeResourcesTag typeResourcesTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(typeResourcesTag.ToString())) return;
        playerController.SetVelocity();
        collision.GetComponent<SpawnItem>().Spawn();    
    }
}
