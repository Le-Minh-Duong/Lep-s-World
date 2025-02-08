using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Player",menuName = "ScriptableObject/Player")]
public class PlayerOB : ScriptableObject
{
    public float hp;
    public float damage;
    public float speed;
    public float heightJump;
    public float heightDoubleJump;
    public float heightJumping;
    public float timeJumping;
}
