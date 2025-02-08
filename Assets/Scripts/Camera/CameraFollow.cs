using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private bool checkX = false;
    private bool checkY = false;

    private static CameraFollow instance;
    public static CameraFollow Instance => instance;

    [SerializeField] private CinemachineVirtualCamera m_Camera;
    [SerializeField] private Transform posPlayer;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private void Awake()
    {
        if (CameraFollow.Instance != null) Debug.LogError("Only 1 Scrips CameraFollow Allow Exist !!!");
        instance = this;
    }

    public void GetPosiotionPlayer(Transform player)
    {
        posPlayer = player;
        m_Camera.Follow = player;
    }

    private void LateUpdate()
    {
        if (posPlayer == null)
        {
            return;
        }

        if(posPlayer.position.x <= minX || posPlayer.position.x >= maxX)
        {
            if (!checkX)
            {
                m_Camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 2f;
                checkX = true;
            }
        }
        else
        {
            if (checkX)
            {
                m_Camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneWidth = 0f;
                checkX = false;
            }
        }

        if (posPlayer.position.y <= minY || posPlayer.position.y >= maxY)
        {
            if (!checkY)
            {

                m_Camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 2f;
                checkY = true;
            }
        }
        else
        {
            if (checkY)
            {
                m_Camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_DeadZoneHeight = 0f;
                checkY = false;
            }
        }
    }

    public float RateDistance()
    {
        return transform.position.x / (maxX - minX);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
