using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteLine : SkillItemsBehaviourController
{
    Vector3 startPos;
    Vector3 endPos;
    public LineRenderer line;
    protected override void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
    }
    public void SetLine(Vector3 startPos, Vector3 endPos)
    {
        this.startPos = startPos;
        this.endPos = endPos;
    }
}
