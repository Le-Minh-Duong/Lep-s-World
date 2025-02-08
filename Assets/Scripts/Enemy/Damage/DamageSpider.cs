using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSpider : DamageEnemyAB
{
    [Header("DamageSpider")]
    [SerializeField] private float gravity;
    [SerializeField] private IMoveEnemy moveEnemy;

    protected override void Start()
    {
        base.Start();
        moveEnemy = GetComponent<IMoveEnemy>();
    }

    public override void CheckDestroy()
    {
        moveEnemy.Other(gravity);
        Destroy(transform.parent.gameObject,2f);
    }
}
