using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CompleteSliderUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI percent;
    [SerializeField] private Image image;
    [SerializeField] private float rate = 0;

    private void Start()
    {
        image.sprite = LevelManager.Instance.GetSprite();
    }

    private void FixedUpdate()
    {
        rate = CameraFollow.Instance.RateDistance() * 100f;
        slider.value = rate;
        percent.text = $"{(int)rate} %";
    }

}
