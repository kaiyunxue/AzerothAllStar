using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public interface IInputTranslator  
{
    void Translate(List<KeyCode> key, StringBuilder inputStringBuilder);
}
