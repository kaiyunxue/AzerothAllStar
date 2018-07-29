using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Registrar))]
[RequireComponent(typeof(InputListener))]

public class GameController : MonoBehaviour {
    public static GameController instance;
    public static Registrar register;
    public static GameObject battleZone;
    public static InputListener[] inputListener = new InputListener[2];

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
