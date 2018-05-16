using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosFlame : SkillItemsBehaviourController {
    public void Stop()
    {
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Stop();
        }
        foreach(var a in GetComponentsInChildren<AudioSource>())
        {
            a.Stop();
        }
    }
    public void Play()
    {
        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
        foreach (var a in GetComponentsInChildren<AudioSource>())
        {
            a.Play();
        }
    }
}
