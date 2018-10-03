using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreaturesState : State {
    public float speed;
    public float attack;
    [SerializeField]
    private MobStatusBox box;
    public Vector3 boxLocalPos = new Vector3(0,1,1);
    private void Awake()
    {
        if(box == null)
        {
            box = CreatureStatusUI.Instance.SetHealthBox();
            box.transform.SetParent(transform);
            box.transform.localPosition = boxLocalPos;
        }
        else
        {
        }
    }
    protected override float SetHealth(float value)
    {
         box.ShowHealth(value / MaxHealth);
        return base.SetHealth(value);
    }
    public override void TakeSkillContent(Damage damage)
    {
        if (damage.doSkillEffect != null)
            damage.doSkillEffect();
        Health -= damage.skillDamage;
    }
    public override void TakeSkillContent(Dot dot)
    {
        base.TakeSkillContent(dot);
        if (dot.doSkillEffect != null)
            dot.doSkillEffect();
        StartCoroutine(TakeDot(dot, 0));
    }
    public override void TakeSkillContent(Debuff dot)
    {
        base.TakeSkillContent(dot);
        dot.buffEffect(this);
    }
    IEnumerator TakeDot(Dot dot, float time)
    {
        TakeSkillContent(dot.PerDamage);
        time += dot.Frequency;
        yield return new WaitForSeconds(dot.Frequency);
        if (time > dot.TotalTime && dot.TotalTime > 0)
            yield return null;
        else
            StartCoroutine(TakeDot(dot, time));
    }
    public override void Stun(float time)
    {
        base.Stun(time);
    }
}
