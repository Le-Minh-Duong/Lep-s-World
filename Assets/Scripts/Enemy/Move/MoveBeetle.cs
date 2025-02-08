using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeetle : MonoBehaviour, IMoveEnemy
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
        throw new NotImplementedException();
    }

    public void Other(float n)
    {
        throw new NotImplementedException();
    }
}
