using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private float moveX;

    private bool jumping = false;
    private bool doubleJump = true;
    private float timeJump = 0;
    //private float timeDoubleJump = 0;

    [Header("Move")]
    [SerializeField] private Transform direction;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private float speed;

    [Header("Check IsGround")]
    [SerializeField] private Transform ground;
    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask layerMask;

    [Header("Jump")]
    [SerializeField] private float heightJump;
    [SerializeField] private float heightDoubleJump;
    [SerializeField] private float heightJumping;
    [SerializeField] private float timeStopJumping;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private string nameMove;
    [SerializeField] private string nameJump;
    [SerializeField] private int movePara = 0;
    [SerializeField] private int jumpPara = 0;

    [Header("Effect")]
    [SerializeField] private ParticleSystem particleSystemRun;
    [SerializeField] private ParticleSystem particleSystemJump;
    [SerializeField] private ParticleSystem particleSystemDoubleJump;

    [Header("Attack")]
    [SerializeField] private TypeResourcesItem typeResourcesItem;
    [SerializeField] private TypeResourcesSkill typeResourcesSkill;

    [Header("Audio")]
    [SerializeField] private TypeResourcesAudio attackAudio;
    [SerializeField] private TypeResourcesAudio jumpAudio;
    [SerializeField] private TypeResourcesAudio warningAudio;

    [Header("Infor")]
    [SerializeField] private PlayerOB playerOB;
    //[SerializeField] private AudioSource jumpAudio;
    //[SerializeField] private AudioSource warningAudio;

    private void Start()
    {
        speed = playerOB.speed;
        heightJump = playerOB.heightJump;
        heightDoubleJump = playerOB.heightDoubleJump;
        heightJumping = playerOB.heightJumping;
        timeStopJumping = playerOB.timeJumping;
        CameraFollow.Instance.GetPosiotionPlayer(transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!IsGround()) return;

        if (jumpPara > 0)
        {
            particleSystemJump.Play();
            //if(collision.transform.position.y < ground.position.y)
        }
        jumpPara = 0;
        //timeDoubleJump = 0.5f;
        doubleJump = true;
    }

    private void Update()
    {
        Move(); 
        Jump();
        Animation();
        Attack();
    }

    private void Move()
    {
        moveX = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(moveX * speed, rigid.velocity.y);
    }

    private void Jump()
    {

        if (jumping && Input.GetKey(KeyCode.Space) && timeJump < timeStopJumping)
        {
            timeJump += Time.deltaTime;
            rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y + (heightJumping * Time.deltaTime));
        }
        else
        {
            jumping = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGround())
            {
                rigid.velocity = new Vector2(rigid.velocity.x, heightJump);

                jumping = true;
                doubleJump = true;

                timeJump = 0;
                jumpPara = 1;
                //timeDoubleJump = 0;
                AudioManager.Instance.SpawnAudio(jumpAudio);
            }
            else if (doubleJump)
            {
                //float per = Mathf.Sqrt(timeDoubleJump / timeStopJumping);
                //if(timeDoubleJump > 1.5f)
                //{
                //    timeDoubleJump = 1.5f;
                //}

                rigid.velocity = new Vector2(rigid.velocity.x, heightDoubleJump);

                jumping = false;
                doubleJump = false;

                jumpPara = 2;

                particleSystemDoubleJump.Play();

                AudioManager.Instance.SpawnAudio(jumpAudio);
            }
        }

        //timeDoubleJump += Time.deltaTime;
    }

    private void Attack()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        if (UpdateResourcesUI.Instance.GetCountItem(typeResourcesItem) <= 0) return;
        UpdateResourcesUI.Instance.UpdateItem(-1, typeResourcesItem);
        SkillManager.Instance.SpawnSkill(transform.position + (Vector3.right * direction.lossyScale.x)
            , new Vector3(0, 0, (direction.localScale.x * 90) - 90), typeResourcesSkill);
    }

    private void Animation()
    {
        if (jumpPara > 0)
        {
            animator.SetInteger(nameJump, jumpPara);
            
            if (!particleSystemRun.isStopped) particleSystemRun.Stop();
        }
        else
        {
            jumpPara = 0;
            if (moveX != 0)
            {
                movePara = 1;

                animator.SetInteger(nameMove,movePara);
                animator.SetInteger(nameJump,jumpPara);
                
                if(!particleSystemRun.isPlaying) particleSystemRun.Play();
            }
            else
            {
                movePara = 0;

                animator.SetInteger(nameMove, movePara);
                animator.SetInteger(nameJump, jumpPara);

                if (!particleSystemRun.isStopped) particleSystemRun.Stop();
            }
        }

        if (moveX > 0)
        {
            direction.localScale = new Vector3(1, 1, 1);
        }
        else if(moveX < 0)
        {
            direction.localScale = new Vector3(-1, 1, 1);
        }
    }

    public float GetGroundY()
    {
        return ground.position.y;
    }

    public void GetJump(float rate)
    {
        rigid.velocity = new Vector2(rigid.velocity.x, heightJump * rate);
        animator.SetInteger(nameJump, 1);
        doubleJump = true;
        jumpPara = 1;
        AudioManager.Instance.SpawnAudio(attackAudio);
        animator.Play("Jump", -1, 0);
    }

    public void DisableAction()
    {
        particleSystemRun.Stop();
        animator.SetInteger(nameJump, 0);
        animator.SetInteger(nameMove, 0);
        rigid.velocity = Vector2.zero;
        this.enabled = false;
    }

    public void AudioWarningPlay()
    {
        AudioManager.Instance.SpawnAudio(warningAudio);
    }

    public float HpPlayer()
    {
        return playerOB.hp;
    }

    public float DamagePlayer()
    {
        return playerOB.damage;
    }

    public void SetVelocity()
    {
        rigid.velocity = new Vector2(rigid.velocity.x,0);
    }

    private bool IsGround()
    {
        return Physics2D.OverlapBox(new Vector2(ground.position.x, ground.position.y), size, 0, layerMask);
    }
}
