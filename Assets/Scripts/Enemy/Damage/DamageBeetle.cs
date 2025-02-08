using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DamageBeetle : DamageEnemyAB
{
    [Header("DamageBeetle")]
    [SerializeField] private string nameAnimation;
    [SerializeField] private IMoveEnemy moveEnemy;

    protected override void Start()
    {
        base.Start();
        moveEnemy = GetComponent<IMoveEnemy>();
    }

    public override void CheckDestroy()
    {
        if (GetHpCurrent() <= 0)
        {
            ActiveCollider(false);
            ActiveAnimation(nameAnimation,true);
            moveEnemy.DisableScript();
        }
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
