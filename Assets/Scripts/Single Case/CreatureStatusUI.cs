using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureStatusUI : MonoBehaviour {
    public static CreatureStatusUI Instance;
    [SerializeField]
    MobStatusBox UI;
    private void Awake()
    {
        Instance = this;
    }
    public MobStatusBox SetHealthBox()
    {
        return Instantiate(UI);
    }
}
