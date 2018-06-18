using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillInstructor : MonoBehaviour
{
    public Image icon;
    public Text skillName;
    public Text formula;
    public Text cd;
    public Text mana;
    public Text instruction;

    public void Load(SkillInstruction packet)
    {
        icon.sprite = packet.skillIcon;
        skillName.text = packet.skillname;
        formula.text = packet.formula;
        cd.text = "" + packet.cd;
        mana.text = "" + packet.mana;
        instruction.text = packet.instruction;
    }
}
