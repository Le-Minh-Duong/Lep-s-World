using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedMove : MonoBehaviour
{
    private float timeCurrent = 0;
    [SerializeField] private float speed;
    [SerializeField] private float timeExist;

    private void Start()
    {
        timeCurrent = 0f;
    }

    private void FixedUpdate()
    {
        timeCurrent += Time.fixedDeltaTime;
        transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
        if (timeCurrent < timeExist) return;
        gameObject.SetActive(false);
    }
}
