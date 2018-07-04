using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillsManager : MonoBehaviour , ISkillManager
{
    public GameObject skillsContainer;
    private ISkill currentSkill;
    Dictionary<string,ISkill> skills = new Dictionary<string, ISkill>();

    void Awake()
    {
        StateBehaveInterface[] behaveInterfaces = GetComponent<Animator>().GetBehaviours<StateBehaveInterface>();
        if(gameObject.layer == 8)
        {
            foreach(var i in behaveInterfaces)
            {
                i.isLeft = true;
            }
        }
        else
        {
            foreach (var i in behaveInterfaces)
            {
                i.isLeft = false;
            }
        }
        var skillPrefabs = skillsContainer.GetComponents<ISkill>();
        foreach(ISkill skill in skillPrefabs)
        {
            skills.Add(skill.GetType().ToString(), skill);
        }
    }

    public ISkill GetSkillByName(string name)
    {
        ISkill returnSkill;
        skills.TryGetValue(name , out returnSkill);
        return returnSkill;
    }
    public void StartSkill(Animator animator , string name)
    {
        ISkill skill = GetSkillByName(name);
        if(skill == null)
        {
            Debug.LogWarning( name + " unfound!!!Insert the skill script first!");
        }
        else
        {
            skill.StartSkill(animator);  
        }
    }
    public void StopSkill(Animator animator, string name)
    {
        ISkill skill = GetSkillByName(name);
        if(skill == null)
        {
            Debug.LogWarning(name + "Skill unfound!!!Insert the skill script first!");
        }
        else
        {
            skill.StopSkill(animator);   
        }
    }

    public ISkill GetCurrentSkill()
    {
        return currentSkill;
    }

    public void SetCurrentSkill(ISkill skill)
    {
        currentSkill = skill;
    }
}
