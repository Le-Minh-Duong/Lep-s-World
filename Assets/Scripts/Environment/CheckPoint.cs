using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private TypeResourcesTag typeResourcesTag;
    [SerializeField] private bool win;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(typeResourcesTag.ToString())) return;

        Check(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(typeResourcesTag.ToString())) return;

        Check(collision.gameObject);
    }

    public void Check(GameObject gameObj)
    {
        if (win)
        {
            gameObj.GetComponent<DamagePlayer>().GetWin();
            EndGameUI.Instance.Win();
        }
        else
        {
            gameObj.GetComponent<DamagePlayer>().GetLose();
            EndGameUI.Instance.Lose();
        }
    }
}
