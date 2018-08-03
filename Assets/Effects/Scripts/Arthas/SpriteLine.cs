using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteLine : SkillItemsBehaviourController
{
    Transform startPos;
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
    public void SetLine(Transform startPos, Vector3 endPos)
    {
        this.startPos = startPos;
        this.endPos = endPos;
    }
    private void FixedUpdate()
    {
        line.SetPosition(0, startPos.position);
        line.SetPosition(1, endPos);
    }
}
