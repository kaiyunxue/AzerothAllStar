﻿using UnityEngine;
using System.Collections;

public class FireBall : SkillItemsBehaviourController
{
    public new RagnarosDamage damage;
    public GameObject fireExplosion;
    public GameObject spellAudio;
    public GameObject collisionAudio;
    public GameObject releaseAudio;
    public GameObject fireball;
    static int maxInstanceNum = 15;
    // Use this for initialization
    protected override void Awake()
    {
        base.Awake();
        spellAudio.GetComponent<AudioSource>().volume = 0.1f;
        collisionAudio.GetComponent<AudioSource>().volume = 0.1f;
    }
    public void WhenBeReleased()
    {
        releaseAudio.SetActive(true);
    }
    public void UpdateVols(float vol)
    {
        spellAudio.GetComponent<AudioSource>().volume = vol + 0.1f;
        collisionAudio.GetComponent<AudioSource>().volume = vol + 0.1f;
    }
    protected override void OnEnable()
    {
        spellAudio.GetComponent<AudioSource>().volume = 0.1f;
        collisionAudio.GetComponent<AudioSource>().volume = 0.1f;
        fireExplosion.SetActive(false);
        fireball.SetActive(true);
        spellAudio.SetActive(true);
        releaseAudio.SetActive(false);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        base.OnEnable();
    }
    protected override IEnumerator DestorySelf()
    {
        spellAudio.GetComponent<AudioSource>().volume = 0.1f;
        collisionAudio.GetComponent<AudioSource>().volume = 0.1f;
        fireball.transform.SetParent(transform,false);
        fireball.transform.localPosition = Vector3.zero;
        fireExplosion.transform.SetParent(transform,false);
        fireExplosion.transform.localPosition = Vector3.zero;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        return base.DestorySelf();
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 9 && col.gameObject.GetComponent<HeroState>() != null)
            {
            spellAudio.SetActive(false);
            StopEmission(fireball);
                fireExplosion.SetActive(true);
             //damage.RunContent(col.GetComponent<State>());
                col.GetComponent<State>().TakeSkillContent(damage);
            }
    }
    void StopEmission(GameObject particle)
    {
        var systems = particle.GetComponentsInChildren<ParticleSystem>();
        fireExplosion.transform.SetParent(GameController.instance.gameObject.transform);
        fireball.transform.SetParent(GameController.instance.gameObject.transform);
        foreach (ParticleSystem system in systems)
        {
            ParticleSystem.CollisionModule cm = system.collision;
            cm.enabled = true;
            system.Stop();
        }
    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
}

