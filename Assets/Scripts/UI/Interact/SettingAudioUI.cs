using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingAudioUI : MonoBehaviour
{
    [Serializable]
    struct SettingAudio
    {
        public Button button;
        public Slider slider;
        public Sprite on;
        public Sprite off;
    }

    [SerializeField] private SettingAudio audioBackground;
    [SerializeField] private SettingAudio audioEffect;

    private void Start()
    {
        float vauleBackground = GameManager.Instance.LoadDataAudio(1);
        audioBackground.slider.value = 
            audioBackground.slider.maxValue * vauleBackground;
        SetImageBackground(vauleBackground);

        float vauleEffect = GameManager.Instance.LoadDataAudio(0);
        audioEffect.slider.value =
            audioEffect.slider.maxValue * vauleEffect;
        SetImageEffect(vauleEffect);

        audioBackground.button.onClick.AddListener(SettingButtonAudioBackground);
        audioEffect.button.onClick.AddListener(SettingButtonAudioEffect);

        audioBackground.slider.onValueChanged.AddListener(SettingSliderAudioBackground);
        audioEffect.slider.onValueChanged.AddListener(SettingSliderAudioEffect);
    }

    private void SettingButtonAudioBackground()
    {
        if(audioBackground.slider.value > 0)
        {
            audioBackground.slider.value = 0f;
            SettingAuidoBackground(0f);
        }
        else
        {
            audioBackground.slider.value = audioBackground.slider.maxValue;
            SettingAuidoBackground(1f);
        }
    }
    
    private void SettingSliderAudioBackground(float value)
    {
        SettingAuidoBackground(value / audioBackground.slider.maxValue);
    }
    
    private void SettingSliderAudioEffect(float value)
    {
        SettingAuidoEffect(value / audioEffect.slider.maxValue);
    }

    private void SettingButtonAudioEffect()
    {
        if(audioEffect.slider.value > 0)
        {
            audioEffect.slider.value = 0f;
            SettingAuidoEffect(0f);
        }
        else
        {
            audioEffect.slider.value = audioEffect.slider.maxValue;
            SettingAuidoEffect(1f);
        }
    }

    private void SettingAuidoBackground(float value)
    {
        GameManager.Instance.SaveDataAudio(value,1);
        SetImageBackground(value);
        AudioManager.Instance.SettingAudioBackGround(value);
    }
    
    private void SettingAuidoEffect(float value)
    {
        GameManager.Instance.SaveDataAudio(value,0);
        SetImageEffect(value);
        AudioManager.Instance.SettingAudioEffect(value);
    }

    private void SetImageBackground(float x)
    {
        if (x == 0f)
        {
            audioBackground.button.image.sprite = audioBackground.off;
        }
        else
        {
            audioBackground.button.image.sprite = audioBackground.on;
        }
    }
    
    private void SetImageEffect(float x)
    {
        if (x == 0f)
        {
            audioEffect.button.image.sprite = audioEffect.off;
        }
        else
        {
            audioEffect.button.image.sprite = audioEffect.on;
        }
    }
}
