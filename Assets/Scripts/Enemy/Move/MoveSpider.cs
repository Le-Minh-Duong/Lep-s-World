using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSpider : MonoBehaviour,IMoveEnemy
{
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private Collider2D collision;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer silk;
    [SerializeField] private string nameAnimation;
    [SerializeField] private float speed;
    [SerializeField] private float timeDelay;
    [SerializeField] private Vector2 moveTarget;
    [SerializeField] private int id = 0;
    private bool check = false;

    private void Start()
    {
        transform.localPosition = Vector3.up * moveTarget.y;
        silk.size = new Vector2(silk.size.x,Mathf.Abs(moveTarget.y));
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (check) return;

        MovePosition();
    }

    private void MovePosition()
    {
        check = true;
        animator.SetBool(nameAnimation, check);
        DOVirtual.Float(transform.localPosition.y, moveTarget.x, speed, x =>
        { 
            transform.localPosition = Vector3.up * x; silk.size = new Vector2(silk.size.x, Mathf.Abs(x));
        }).SetId(id).onComplete +=() => 
            {
                DOVirtual.Float(transform.localPosition.y, moveTarget.y, speed, x =>
                { 
                    transform.localPosition = Vector3.up * x; silk.size = new Vector2(silk.size.x, Mathf.Abs(x)); 
                }).SetId(id).SetDelay(timeDelay).onComplete += ()=> 
                {
                    if (collision.IsTouchingLayers())
                    {
                        DOVirtual.DelayedCall(timeDelay/2f,() => { MovePosition(); }).SetId(id);
                    }
                    else
                    {
                        check = false;
                        animator.SetBool(nameAnimation, check);
                    }
                };
            };
    }

    private void OnDisable()
    {
        DOTween.Kill(id);
    }

    public void DisableScript()
    {

    }

    public void EnableScript()
    {
        throw new System.NotImplementedException();
    }

    public void Other(float n)
    {
        silk.size = Vector2.zero;
        animator.SetBool(nameAnimation, false);
        rigid.gravityScale = n;
        this.enabled = false;
    }
}
