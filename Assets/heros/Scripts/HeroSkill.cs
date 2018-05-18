using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class HeroSkill : Skill, ISkill
{
    
    public Sprite skillIcon;
    protected Hero hero;
    protected ISkillManager skillManager;
    public float manaCost;
    public string formula;
    public int Priority;
    protected override void Awake()
    {
        base.Awake();
        hero = transform.GetComponentInParent<Hero>();
        skillManager = hero.skillManager;
        if(Priority == 0)
            Priority = formula.Length;
        if (Priority == 0)
            Debug.LogWarning(skillName + "in the gameobject: " + name + " doesn't have a proority");
    }
    public virtual string GetFormula()
    {
        return formula;
    }

    public abstract void StartSkill(Animator animator);
    public abstract void StopSkill(Animator animator);
    public abstract bool TryStartSkill(Animator animator);
}
