using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInstructorRect : MonoBehaviour {
    public SkillInstructor prefab;
    public void Load(SkillInstruction[] instructions)
    {
        foreach(var ins in instructions)
        {
            var instructor = Instantiate(prefab, transform);
            instructor.Load(ins);
        }
    }
}
