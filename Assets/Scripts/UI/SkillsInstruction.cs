using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SkillInstruction : ScriptableObject
{
    public Sprite skillIcon;
    public string nameAndFormula;
    public float cd;
    public float mana;
    public string instruction;
}
