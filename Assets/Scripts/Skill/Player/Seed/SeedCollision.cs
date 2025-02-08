using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedCollision : MonoBehaviour
{
    [SerializeField] private TypeResourcesTag typeResourcesTag;
    [SerializeField] private TypeResourcesTag typeResourcesTagDestroy;
    [SerializeField] private TypeResourcesParticle typeResourcesParticle;
    [SerializeField] private TypeResourcesSkill typeResourcesSkill;
    [SerializeField] private TypeResourcesAudio typeResourcesAudio;
    [SerializeField] private Rigidbody2D rigidbody2d;
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private SeedMove seedMove;
    [SerializeField] private float damage;
    [SerializeField] private float force;
    [SerializeField] private float timeDisable;
    private bool check = true;

    private void OnEnable()
    {
        EnableObject();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!check) return;
        if(collision.gameObject.CompareTag("Untagged"))
        {
            DisableObject();
        }
        else if(collision.gameObject.CompareTag(typeResourcesTagDestroy.ToString()))
        {
            collision.GetComponent<SpawnItem>().Spawn();

            DisableObject();
        }
        else if(collision.gameObject.CompareTag(typeResourcesTag.ToString()))
        {
            DamageEnemyAB damageEnemyAB = collision.gameObject.GetComponent<DamageEnemyAB>();

            if (damageEnemyAB == null) return;

            damageEnemyAB.GetDamage(-damage);
            damageEnemyAB.CheckDestroy();

            DisableObject();
        }
    }

    private void EnableObject()
    {
        check = true;
        seedMove.enabled = true;
        collider2d.enabled = true;
        rigidbody2d.gravityScale = 0;
    }

    private void DisableObject()
    {
        check = false;
        collider2d.enabled = false;
        seedMove.enabled = false;
        rigidbody2d.gravityScale = 3f;
        rigidbody2d.AddRelativeForce(Vector2.left * force);
        SkillManager.Instance.ActiveEffect(transform.position,typeResourcesParticle,typeResourcesAudio);
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(timeDisable);
        SkillManager.Instance.DisableItem(gameObject, typeResourcesSkill);
    }
}
