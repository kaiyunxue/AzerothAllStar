using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkill
{  
    void StartSkill(Animator animator);
    void StopSkill(Animator animator);
    bool IsReady();
    bool TryStartSkill(Animator animator);
    string GetFormula();
}

