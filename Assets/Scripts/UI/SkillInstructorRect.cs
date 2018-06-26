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
    public void OnDisable()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
