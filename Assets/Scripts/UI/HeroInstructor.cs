using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroInstructor : MonoBehaviour {
    public HeroPacket hero;

    public Text heroName;
    public HeroIconCard card;

    public Text race;
    public Text energy;

    public Text attack;
    public Text defence;
    public Text spell;
    public Text summon;
    public Text move;

    public Text instruction;

    public SkillInstructorRect skills;

    private void OnEnable()
    {
        heroName.text = hero.heroName;
        card.icon = hero.icon;
        race.text = hero.race;
        energy.text = hero.energy;
        attack.text = DrawStar(hero.attack);
        defence.text = DrawStar(hero.defence);
        spell.text = DrawStar(hero.spell);
        summon.text = DrawStar(hero.summon);
        move.text = DrawStar(hero.move);
        skills.Load(hero.skillInstructions);
    }
    private string DrawStar(int n)
    {
        string s = "";
        for(int i = 0; i <n; i++)
        {
            s += "★";
        }
        return s;
    }
}
