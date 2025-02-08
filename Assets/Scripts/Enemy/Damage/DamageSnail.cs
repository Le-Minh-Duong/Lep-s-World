using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSnail : DamageEnemyAB
{
    [Header("DamageSnail")]
    [SerializeField] private Collider2D colisionPhysic;
    [SerializeField] private string nameAnimation;
    [SerializeField] private string nameAnimationDestroy;
    [SerializeField] private float addHP;
    [SerializeField] private float setDamage;
    [SerializeField] private float velocity;
    [SerializeField] private IMoveEnemy moveEnemy;

    protected override void Start()
    {
        base.Start();
        moveEnemy = GetComponent<IMoveEnemy>();
    }

    public override void CheckDestroy()
    {
        SetDamage(-setDamage);
        moveEnemy.DisableScript();

        if(GetHpCurrent() > 0)
        {
            ActiveAnimation(nameAnimation, true); 
        }
        else
        {
            ActiveCollider(false);
            ActiveAnimation(nameAnimationDestroy, true);
            moveEnemy.Other(velocity);
            colisionPhysic.enabled = false;
            Destroy(gameObject,2f);
        }
    }

    private void CheckGameObject()
    {
        GetDamage(addHP);
        ActiveAnimation(nameAnimation, false);
        moveEnemy.EnableScript();
        SetDamage(setDamage);
    }
}
