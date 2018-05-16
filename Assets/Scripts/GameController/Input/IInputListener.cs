using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputListener
{
    string GetInputString();
    bool GetSkill(string skillFormula);
    bool isLongPress(KeyCode key, out float time);
}
