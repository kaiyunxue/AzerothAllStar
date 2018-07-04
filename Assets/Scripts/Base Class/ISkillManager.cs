using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkillManager
{
    ISkill GetSkillByName(string name);
    void StartSkill(Animator animator , string name);
    void StopSkill(Animator animator, string name);
    ISkill GetCurrentSkill();
}


