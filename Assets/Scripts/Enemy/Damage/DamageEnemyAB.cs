using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageEnemyAB : MonoBehaviour
{
    [Header("DamageEnemyAB")]
    [SerializeField] private Collider2D collider2d;
    [SerializeField] private Transform position;
    [SerializeField] private TypeResourcesTag typeResourcesTag;
    [SerializeField] private EnemyOB enemyOB;
    [SerializeField] private Animator animator;
    [SerializeField] private float hpCurrent;
    [SerializeField] private float damageCurrent;

    protected virtual void Start()
    {
        hpCurrent = enemyOB.hp;
        damageCurrent = enemyOB.damage;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(typeResourcesTag.ToString())) return;

        DamagePlayer damagePlayer = collision.gameObject.GetComponent<DamagePlayer>();

        if (damagePlayer == null) return;

        if (damagePlayer.GetPositionY() > position.position.y)
        {
            hpCurrent -= damagePlayer.SendDamage();
            CheckDestroy();
        }
        else
        {
            damagePlayer.GetDamage(damageCurrent);
        }
        
    }

    public virtual void CheckDestroy()
    {

    }

    protected void ActiveAnimation(string name,bool active)
    {
        animator.SetBool(name, active);
    }

    protected void ActiveCollider(bool check)
    {
        collider2d.enabled = check;
    }

    public void GetDamage(float n)
    {
        hpCurrent += n;
    }
    
    protected void SetDamage(float n)
    {
        damageCurrent += n;
    }

    protected float GetHpCurrent()
    {
        return hpCurrent;
    }
}
