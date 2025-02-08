using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] private TypeAudioNormal audioSource;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();

    [SerializeField] private List <TypeAudioNormal> audioBackground = new List<TypeAudioNormal>(); 
    //[SerializeField] private List <TypeAudioNormal> audioEffect = new List<TypeAudioNormal>(); 
    [SerializeField] private List<TypeAudio> typeResourcesAudios = new List<TypeAudio>();

    private void Awake()
    {
        if (AudioManager.Instance != null) Debug.LogError("Only 1 Scrips AudioManager Allow Exist !!!");
        instance = this;
    }

    private void Start()
    {
        SettingAudioBackGround(GameManager.Instance.LoadDataAudio(1));
        SettingAudioEffect(GameManager.Instance.LoadDataAudio(0));
    }

    private void FixedUpdate()
    {
        Task.Run(() => 
        {
            typeResourcesAudios.ForEach(item =>
            {
                if (item.timeCurrent < 100f) item.timeCurrent += 0.02f;
            });
        });
    }
      
    public void ActiveAudioVirtual(int index)
    {
        audioSource.audioSource.PlayOneShot(audioClips[index]);
    }

    public void SpawnAudio(TypeResourcesAudio type)
    {
        foreach (var item in typeResourcesAudios)
        {
            if (item.typeResourcesAudio == type)
            {
                if (item.timeCurrent < item.timeMax) return;

                item.timeCurrent = 0f;
                item.audioSource.Play();

                return;
            }
        }
    }

    public void SettingAudioBackGround(float value)
    {
        audioBackground.ForEach(item => item.audioSource.volume = value * item.maxVolume);
    }

    public void SettingAudioEffect(float value)
    {
        //audioEffect.ForEach(item => item.audioSource.volume = (value * item.maxVolume));
        typeResourcesAudios.ForEach(item => item.audioSource.volume = value * item.maxVolume);
        if (this.audioSource.audioSource == null) return;
        audioSource.audioSource.volume = value * audioSource.maxVolume;
    }
}

[Serializable]
public class TypeAudio
{   
    [Range(0f,1f)]
    public float maxVolume;
    public float timeCurrent;
    public float timeMax;
    public AudioSource audioSource;
    public TypeResourcesAudio typeResourcesAudio;
}

[Serializable]
public class TypeAudioNormal
{
    [Range(0f, 1f)]
    public float maxVolume;
    public AudioSource audioSource;
}
