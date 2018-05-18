using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagnarosFireStorm : HeroSkill, ISkill
{
    public AudioClip word;
    public FireWind fireStormParticle;
    public Vector3 pos;
    public Vector3 rot;

    public override void StartSkill(Animator animator)
    {
        hero.state.Mana -= manaCost;
        KOFItem.InstantiateByPool(fireStormParticle, pos, rot, Camera.main.transform, gameObject.layer);
        StartCdColding();
        hero.statusBox.cdBar.StartCooling(skillIcon, cd);
    }

    public override void StopSkill(Animator animator)
    {
    }
    public override bool IsReady()
    {
        if (!Lock)
            return false;
        if (!GameController.LeftInputListener.GetSkill(formula))
            return false;
        if (hero.state.Mana < manaCost)
            return false;
        hero.audioCtrler.PlaySound(word);
        return true;
    }
    public override bool TryStartSkill(Animator animator)
    {
        if (IsReady())
        {
            animator.SetTrigger("FireStorm");
            return true;
        }
        else
            return false;
    }
}

