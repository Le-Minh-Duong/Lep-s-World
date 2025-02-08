using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMountainBackGround : MonoBehaviour
{
    [SerializeField] private new Transform camera;
    [SerializeField] private float distance;

    [SerializeField] private GameObject gameObjectDefault_1;
    [SerializeField] private GameObject gameObjectDefault_2;

    private void Update()
    {
        if(camera.position.x > gameObjectDefault_1.transform.position.x + distance)
        {
            gameObjectDefault_1.transform.position += Vector3.right * 2f * distance;
        }
        else if (camera.position.x < gameObjectDefault_1.transform.position.x - distance)
        {
            gameObjectDefault_1.transform.position -= Vector3.right * 2f * distance;
        }
        else if(camera.position.x > gameObjectDefault_2.transform.position.x + distance)
        {
            gameObjectDefault_2.transform.position += Vector3.right * 2f * distance;
        }
        else if (camera.position.x < gameObjectDefault_2.transform.position.x - distance)
        {
            gameObjectDefault_2.transform.position -= Vector3.right * 2f * distance;
        }
    }

    public Vector2 PositionLight_1()
    {
        return gameObjectDefault_1.transform.position;
    }

    public Vector2 PositionLight_2()
    {
        return gameObjectDefault_2.transform.position;
    }
}
