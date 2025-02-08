using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private TypeResourcesTag tagPlayer;
    [SerializeField] private TypeResourcesAudio typeResourcesAudio;
    [SerializeField] private float velocity;
    [SerializeField] private float time;
    [SerializeField] private float timeSlow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(tagPlayer.ToString())) return;
        GameObject player = collision.gameObject;
        player.transform.position = transform.position;
        player.transform.localScale = Vector3.one;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = transform.right.normalized * velocity;
        AudioManager.Instance.SpawnAudio(typeResourcesAudio);
        StartCoroutine(EnablePlayerController(player));
    }

    private IEnumerator EnablePlayerController(GameObject player)
    {
        yield return new WaitForSeconds(time);
        Rigidbody2D rigidbody2d = player.GetComponent<Rigidbody2D>();
        DOVirtual.Float(rigidbody2d.velocity.x,0, timeSlow,x => rigidbody2d.velocity = Vector2.right * x).onComplete += 
            () => player.GetComponent<PlayerController>().enabled = true;
        
    }
}
