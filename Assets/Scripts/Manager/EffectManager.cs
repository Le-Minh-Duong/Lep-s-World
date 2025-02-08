using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private static EffectManager instance;
    public static EffectManager Instance => instance;

    [SerializeField] private List<TypeParticle> typeParticles = new List<TypeParticle>();

    private void Awake()
    {
        if (EffectManager.Instance != null) Debug.LogError("Only 1 Scrips EffectManager Allow Exist !!!");

        instance = this;
    }

    public void SpawnParticle(Vector3 position, TypeResourcesParticle tyResources)
    {
        //TypeParticle particle = typeParticles.Find(item => item.typeResourcesParticle == tyResources);

        //if (particle.particleSystem == null) return;
        //particle.particleSystem.gameObject.transform.localPosition = position;
        //particle.particleSystem.Play();

        TypeParticle particle = new TypeParticle();

        bool flag = false;

        for (int i = 0; i < typeParticles.Count; i++)
        {
            if (tyResources == typeParticles[i].typeResourcesParticle)
            {
                if (!typeParticles[i].particleSystem.isPlaying)
                {
                    particle.particleSystem = typeParticles[i].particleSystem;
                    flag = true;
                    break;
                }
                else
                {
                    particle.particleSystem = typeParticles[i].particleSystem;
                }
                particle.typeResourcesParticle = typeParticles[i].typeResourcesParticle;
            }
        }

        if (particle.particleSystem != null && flag)
        {
            particle.particleSystem.gameObject.transform.localPosition = position;
            particle.particleSystem.Play();
        }
        else if (particle.particleSystem != null && !flag)
        {
            GameObject newGameObject = Instantiate(particle.particleSystem.gameObject, particle.particleSystem.gameObject.transform.parent);

            TypeParticle newParticle = new TypeParticle()
            {
                particleSystem = newGameObject.GetComponent<ParticleSystem>(),
                typeResourcesParticle = particle.typeResourcesParticle
            };
            newGameObject.transform.localPosition = position;
            newParticle.particleSystem.Play();
            typeParticles.Add(newParticle);
        }
    }
}

[Serializable]
public struct TypeParticle
{
    public ParticleSystem particleSystem;
    public TypeResourcesParticle typeResourcesParticle;
}
