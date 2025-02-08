using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    [SerializeField] private float maxX;
    [SerializeField] private float minX;

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.right * (minX + ((maxX - minX) * CameraFollow.Instance.RateDistance()));
    }
}
