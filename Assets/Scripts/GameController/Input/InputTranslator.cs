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
    public void Translate(List<KeyCode> keyStream, StringBuilder inputStringBuilder, bool isLeft = true)
    {
        if(isLeft)
        {
            foreach (KeyCode key in keyStream)
            {
                if (listenedKeys.Contains(key))
                {
                    inputStringBuilder.Append(key.ToString());
                }
            }
        }
        else
        {
            foreach (KeyCode key in keyStream)
            {
                if(key == listenedKeys[0])
                {
                    inputStringBuilder.Append("W");
                }
                else if(key == listenedKeys[1])
                {
                    inputStringBuilder.Append("S");
                }
                else if (key == listenedKeys[2])
                {
                    inputStringBuilder.Append("A");
                }
                else if (key == listenedKeys[3])
                {
                    inputStringBuilder.Append("D");
                }
                else if (key == listenedKeys[4])
                {
                    inputStringBuilder.Append("J");
                }
                else if (key == listenedKeys[5])
                {
                    inputStringBuilder.Append("K");
                }
                else if (key == listenedKeys[6])
                {
                    inputStringBuilder.Append("L");
                }
            }
        }
    } 
}
