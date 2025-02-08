using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStart : MonoBehaviour
{
    [SerializeField] private MoveMountainBackGround move;
    [SerializeField] private ParticleSystem particleSystemm;
    [SerializeField] private List<Vector2> positions = new List<Vector2>();
    [SerializeField] private float per;
    [SerializeField] private float rate;

    private void FixedUpdate()
    {
        SpawnStarts();
    }

    private void SpawnStarts()
    {
        if (Random.Range(0f,100f) > per) return;
        int index = Random.Range(0, positions.Count);
        Vector2 newPosition = positions[index];
        newPosition.x += Random.Range(-rate, rate);
        newPosition.y += Random.Range(-rate, rate) + 7f;
        if(index < 13)
        {
            newPosition += move.PositionLight_1();
        }
        else
        {
            newPosition.x -= 22.7f;
            newPosition += move.PositionLight_2();
        }
        particleSystemm.transform.position = newPosition;
        particleSystemm.Play();
    }
}
