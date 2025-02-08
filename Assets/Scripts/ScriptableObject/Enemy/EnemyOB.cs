using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy",menuName = "ScriptableObject/Enemy")]
public class EnemyOB : ScriptableObject
{
    public float hp;
    public float damage;
}
