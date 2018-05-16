using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagnarosSkillsManager : MonoBehaviour , ISkillManager
{
    public GameObject skillsContainer;
    Dictionary<string,ISkill> skills = new Dictionary<string, ISkill>();

    void Awake()
    {
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
            Debug.LogWarning("Skill unfound!!!Insert the skill script first!");
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
            Debug.LogWarning("Skill unfound!!!Insert the skill script first!");
        }
        else
        {
            skill.StopSkill(animator);   
        }
    }
}
