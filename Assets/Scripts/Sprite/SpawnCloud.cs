using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using UnityEngine.PlayerLoop;

public class SpawnCloud : MonoBehaviour
{
    [Header("Initialization")]
    [SerializeField] private List<GameObject> spawn = new List<GameObject>();
    [SerializeField] private List<Clouds> clouds = new List<Clouds>();
    [SerializeField] private List<Clouds> cloudsDisable = new List<Clouds>();
    [SerializeField] private float rateSpawn;
    [SerializeField] private Vector2 nInitialization;

    [Header("Update")]
    [SerializeField] private float speed;
    [SerializeField] private float ratespeed;
    [SerializeField] private Vector2 ratePositionX;
    [SerializeField] private Vector2 ratePositionY;
    [SerializeField] private float destroy;
    [SerializeField] private float ratescale;

    private void Start()
    {
        int count = UnityEngine.Random.Range((int)nInitialization.x, (int)nInitialization.y);

        for (int i = 0; i < count; i++)
        {
            Initialization(UnityEngine.Random.Range(-ratePositionX.x, ratePositionX.y));
        }
    }

    private void FixedUpdate()
    {
        Spawn();
        DestoyCloud();
        Move();
    }

    private void DestoyCloud()
    {
        for (int i = clouds.Count - 1; i >= 0; i--)
        {
            if (clouds[i].cloud.transform.localPosition.x < destroy)
            {
                clouds[i].cloud.SetActive(false);
                cloudsDisable.Add(clouds[i]);
                clouds.RemoveAt(i);
            }
        }
    }

    private void Move()
    {
        for (int i = clouds.Count - 1; i >= 0; i--)
        {
            clouds[i].cloud.transform.localPosition += Vector3.left * clouds[i].speed * Time.fixedDeltaTime;
        }
    }

    private void Spawn()
    {
        if (UnityEngine.Random.Range(0f,100f) > rateSpawn) return;

        if(cloudsDisable.Count == 0)
        {
            Initialization(UnityEngine.Random.Range(ratePositionX.x,ratePositionX.y));
        }
        else
        {
            ActiveCloud(UnityEngine.Random.Range(ratePositionX.x, ratePositionX.y));
        }
    }

    private void Initialization(float x)
    {
        GameObject newGameObject = Instantiate(spawn[UnityEngine.Random.Range(0, spawn.Count)]);
        newGameObject.transform.SetParent(transform);
        Vector3 position = new Vector3(x, UnityEngine.Random.Range(ratePositionY.x, ratePositionY.y), 0);
        newGameObject.transform.localPosition = position;
        newGameObject.transform.localScale = Vector3.one * UnityEngine.Random.Range((1 / ratescale), ratescale);
        newGameObject.SetActive(true);
        Clouds cloud = new Clouds
        {
            cloud = newGameObject,
            speed = UnityEngine.Random.Range(speed - ratespeed, speed + ratespeed)
        };
        clouds.Add(cloud);
    }

    private void ActiveCloud(float x)
    {
        int index = UnityEngine.Random.Range(0, cloudsDisable.Count);
        Vector3 position = new Vector3(x, UnityEngine.Random.Range(ratePositionY.x, ratePositionY.y), 0);
        cloudsDisable[index].cloud.transform.localPosition = position;
        cloudsDisable[index].cloud.transform.localScale = Vector3.one * UnityEngine.Random.Range((1 / ratescale), ratescale);
        cloudsDisable[index].cloud.SetActive(true);
        clouds.Add(cloudsDisable[index]);
        cloudsDisable.RemoveAt(index);

        //Clouds cloud = cloudsDisable[UnityEngine.Random.Range(0, cloudsDisable.Count)];
        //cloudsDisable.Remove(cloud);
        //Vector3 position = new Vector3(x, UnityEngine.Random.Range(ratePositionY.x, ratePositionY.y), 0);
        //cloud.cloud.transform.localPosition = position;
        //cloud.cloud.transform.localScale = Vector3.one * UnityEngine.Random.Range((1 / ratescale), ratescale);
        //cloud.cloud.SetActive(true);
        //clouds.Add(cloud);
    }
}

[Serializable]
public struct Clouds
{
    public GameObject cloud;
    public float speed;
}
