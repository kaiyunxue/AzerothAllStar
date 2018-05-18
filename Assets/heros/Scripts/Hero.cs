using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : KOFItem
{
    public SkillItemsBehaviourController weapon;
    public Animator animator;
    public ISkillManager skillManager;
    public StatusBox statusBox;
    public AudioController audioCtrler;
    [SerializeField]
    protected HerosRegistrar heroRegister;

    public State state;
    public HerosRegistrar HeroRegister
    {
        get
        {
            return this.heroRegister;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        animator = gameObject.GetComponent<Animator>();
        skillManager = gameObject.GetComponent<ISkillManager>();
        heroRegister = gameObject.GetComponent<HerosRegistrar>();
        state = gameObject.GetComponent<State>();
        audioCtrler = GetComponent<AudioController>();
    }
}
