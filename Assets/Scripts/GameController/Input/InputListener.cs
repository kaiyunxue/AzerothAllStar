using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class InputListener : MonoBehaviour,IInputListener
{
    public float longPressTime;
    public float clearTime = 0.3f;
    public List<KeyCode> dirKeys = new List<KeyCode> ();
    public List<KeyCode> skillKeys = new List<KeyCode>();
	public List<KeyCode> inputKeys = new List<KeyCode>();

    public Hashtable keyTime = new Hashtable();
    StringBuilder inputStringBuilder = new StringBuilder(5);
    StringBuilder stringBuilder = new StringBuilder(5);
    IInputTranslator inputTranslator;
    float time1;
    float[] pressTime = new float[7];
    void Awake()
    {
        inputTranslator = new InputTranslator(dirKeys,skillKeys);
    }
	void Start () 
	{
        time1 = 0;
        for (int i = 0; i < 7; i++)
            pressTime[i] = 0;
    }
	void Update () 
	{
        if (GetInput())
        {
			time1 = Time.time;
        }
		else if(Time.time - time1 > clearTime)
        {
			inputKeys.Clear();
        }
		if(inputKeys.Count > 5)
		{
			inputKeys.RemoveRange (0, inputKeys.Count - 5);
		}
        foreach(KeyCode key in dirKeys)
        {
            if (Input.GetKeyUp(key))
                keyTime.Remove(key);
            if (Input.GetKey(key))
            {
                if(keyTime.Contains(key))
                {
                    keyTime[key] = (float)keyTime[key] + Time.deltaTime;
                }
                else
                {
                    keyTime.Add(key, 0f);
                }
            }
        }
        foreach (KeyCode key in skillKeys)
        {
            if (Input.GetKeyUp(key))
                keyTime.Remove(key);
            if (Input.GetKey(key))
            {
                if (keyTime.Contains(key))
                {
                    keyTime[key] = (float)keyTime[key] + Time.deltaTime;
                }
                else
                {
                    keyTime.Add(key, 0f);
                }
            }
        }

    }
    bool GetInput()
    {
		foreach(KeyCode key in dirKeys)
		{
            inputStringBuilder.Remove(0, inputStringBuilder.Length);
			if(Input.GetKeyDown(key))
			{
				inputKeys.Add (key);
				return true;
			}
		}
        foreach(KeyCode key in skillKeys)
        {
            if(Input.GetKeyDown(key))
            {
                inputKeys.Add (key);
                inputTranslator.Translate(inputKeys, inputStringBuilder);
                inputKeys.Clear();
                return true;
            }
        }
        inputStringBuilder.Remove(0, inputStringBuilder.Length);
        return false;
    }
    
    public static bool GetKey(KeyCode key)
    {
        return Input.GetKey(key);
    }
    public static bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }
    public static bool GetKeyUp(KeyCode key)
    {
        return Input.GetKeyUp(key);
    }
    public string GetInputString()
    {
        return inputStringBuilder.ToString();
    }

    public bool GetSkill(string skillFormula)
    {
        if(skillFormula == inputStringBuilder.ToString())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool isLongPress(KeyCode key)
    {
        if (keyTime.Contains(key))
        {
            if ((float)keyTime[key] >= longPressTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public bool isLongPress(KeyCode key, out float time)
    {
        if (keyTime.Contains(key))
        {
            if ((float)keyTime[key] >= longPressTime)
            {
                time = (float)keyTime[key];
                return true;
            }
            else
            {
                time = 0;
                return false;
            }
        }
        else
        {
            time = 0;
            return false;
        }
    }
}
