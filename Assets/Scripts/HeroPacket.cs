using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class HeroPacket : ScriptableObject {

    public int id;

    public HeroIcon icon;
    public Hero heroPrefab;
    public string heroName; //has repetition
    public string race;
    public string energy;
    public int attack;
    public int spell;
    public int summon;
    public int defence;
    public int move;
    public int control;
    public string instruction;
    public SkillInstruction[] skillInstructions;
}
