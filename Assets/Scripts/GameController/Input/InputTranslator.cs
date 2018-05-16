using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InputTranslator: IInputTranslator
{
    List<KeyCode> listenedKeys = new List<KeyCode>();
    public InputTranslator(List<KeyCode> dirKeys, List<KeyCode> skillKeys)
    {
        foreach(KeyCode key in dirKeys)
        {
            listenedKeys.Add(key);
        }
        foreach(KeyCode key in skillKeys)
        {
            listenedKeys.Add(key);
        }
    }
    public void Translate(List<KeyCode> keyStream, StringBuilder inputStringBuilder)
    {
        foreach(KeyCode key in keyStream)
        {
            if(listenedKeys.Contains(key))
            {
                inputStringBuilder.Append(key.ToString());
            }
        }
    } 
}
