using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private bool checkReceiveDamage = false;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private int n;
    [SerializeField] private float time;
    [SerializeField] private float timeEnable;
    [SerializeField] private PlayerController controller;
    [SerializeField] private float rateJump;
    [SerializeField] private float hpCurrent;
    [SerializeField] private float damageCurrent;

    private void Start()
    {
        hpCurrent = controller.HpPlayer();
        damageCurrent = controller.DamagePlayer();
        StartCoroutine(EnableController());
        UpdateResourcesUI.Instance.GetDamagePlayer(this);
    }

    private IEnumerator EnableController()
    {
        yield return new WaitForSeconds(timeEnable);
        controller.enabled = true;
    }

    public float SendDamage()
    {
        if (hpCurrent <= 0) return 0;
        controller.GetJump(rateJump);
        return damageCurrent;
    }

    public void GetDamage(float damage)
    {
        if (checkReceiveDamage || damage == 0 || hpCurrent <= 0) return;
        hpCurrent -= damage;
        UpdateResourcesUI.Instance.UpdateHP(-(int)damage);
        Warning();
        controller.AudioWarningPlay();
        if(hpCurrent <= 0)
        {
            GetLose();
            EndGameUI.Instance.Lose();
        }
    }


    public float GetPositionY()
    {
        return controller.GetGroundY();
    }

    private void Warning()
    {
        if (checkReceiveDamage) return;
        Color colorx = Color.white;
        colorx.a = 0.2f;
        this.checkReceiveDamage = true;
        spriteRenderer.DOColor(colorx,time).SetLoops(n, LoopType.Yoyo).onComplete += ()=> checkReceiveDamage = false;
    }

    public void GetLose()
    {
        UpdateResourcesUI.Instance.UpdateHP(-(int)hpCurrent);
        controller.DisableAction();
        Color colorx = Color.white;
        colorx.a = 0f;
        spriteRenderer.DOColor(colorx, time).SetLoops(9, LoopType.Yoyo);
    }

    public void GetHP(float n)
    {
        hpCurrent += n;
    }

    public void GetWin()
    {
        controller.DisableAction();
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
