using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planks : MonoBehaviour
{
    [SerializeField] private Transform plank;
    [SerializeField] private float position;
    [SerializeField] private float time;
    [SerializeField] private float delay;

    private void Start()
    {
        plank.DOLocalMoveY(position, time).SetLoops(-1, LoopType.Yoyo);
        //DOTween.To(() => plank.localPosition,targetValue => plank.localPosition= targetValue,Vector3.up*position,time).SetLoops(-1,LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
