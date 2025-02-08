using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScale : MonoBehaviour
{
    [SerializeField] private List<Transform> transforms = new List<Transform>();
    [SerializeField] private float time;
    [SerializeField] private float scale;

    private void Start()
    {
        DOVirtual.Float(1f,scale,time ,x => {transforms.ForEach(item => item.localScale = Vector3.one * x) ; }).SetLoops(-1, LoopType.Yoyo);
        //transform.DOScale(Vector3.one * scale,time).SetLoops(-1,LoopType.Yoyo);
    }

    private void OnDisable()
    {
        DOTween.KillAll();
    }
}
