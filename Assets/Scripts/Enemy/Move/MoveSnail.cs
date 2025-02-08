using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveSnail : MonoBehaviour, IMoveEnemy
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 moveTarget;

    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(speed, rigid.velocity.y);

        if (transform.localPosition.x < moveTarget.x)
        {
            speed = Mathf.Abs(speed);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (transform.localPosition.x > moveTarget.y)
        {
            speed = -Mathf.Abs(speed);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void DisableScript()
    {
        this.enabled = false;
        rigid.velocity = Vector3.zero;
    }

    public void EnableScript()
    {
        this.enabled = true;
    }

    public void Other(float n)
    {
        float direction = UnityEngine.Random.Range(-45f, 45f);
        transform.localRotation = Quaternion.Euler(0,0,direction);
        rigid.AddForce(new Vector2(direction/90f,1) * n);
        rigid.AddTorque(direction/180f * n);
    }
}
