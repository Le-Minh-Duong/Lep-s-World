using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private TypeResourcesTag typeResourcesTag;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private float time;
    [SerializeField] private float timeActive = 0f;
    private bool check = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(typeResourcesTag.ToString())) return;
        if (check) return;
        check = true;
        StartCoroutine(Active(collision.gameObject));
    }

    private IEnumerator Active(GameObject game)
    {
        yield return new WaitForSeconds(timeActive);
        game.GetComponent<PlayerController>().DisableAction();
        tutorial.SetActive(true);
        StartCoroutine(DestroyObject(game));
    }

    private IEnumerator DestroyObject(GameObject gameObj)
    {
        yield return new WaitForSeconds(time);
        gameObj.GetComponent<PlayerController>().enabled = true;
        tutorial.SetActive(false);
        Destroy(gameObject);
    }
}
