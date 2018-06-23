using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Registrar))]
[RequireComponent(typeof(InputListener))]

public class GameController : MonoBehaviour {
    public static GameController instance;
    static Registrar register;
    static GameObject battleZone;
    static InputListener[] inputListener = new InputListener[2];
    public bool a;

    void Awake()
    {
        instance = this;
        battleZone = this.gameObject;
        register = gameObject.GetComponent<Registrar>();
        inputListener = gameObject.GetComponents<InputListener>();
    }

    public static Registrar Register
    {
        get
        {
            return register;
        }
    }

    public static InputListener LeftInputListener
    {
        get
        {
            return inputListener[0];
        }
    }
    public static InputListener RightInputListener
    {
        get
        {
            return inputListener[1];
        }
    }
}
