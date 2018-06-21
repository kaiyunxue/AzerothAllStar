using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillInstruction : ScriptableObject
{
    public Sprite skillIcon;
    public string skillname;
    public string formula;
    public string cd;
    public string mana;
    public string instruction;
}
