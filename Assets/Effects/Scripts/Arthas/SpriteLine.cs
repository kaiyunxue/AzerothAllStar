using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpriteLine : SkillItemsBehaviourController
{
    public float moveSpeed = 1;
    public float zigzagRate;
    public override int GetMaxInstance()
    {
        return 3;
    }
    Transform startPos;
    Vector3 endPos;
    public LineRenderer line;
    Material m;
    protected override void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        m = GetComponent<LineRenderer>().material;
    }
    public void SetLine(Transform startPos, Vector3 endPos)
    {
        this.startPos = startPos;
        this.endPos = endPos;
    }
    public void SetLine(Transform startPos, Vector3 endPos, float zigzagRate, float speed = 0)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.moveSpeed = speed;
        this.zigzagRate = zigzagRate;
    }
    private void FixedUpdate()
    {
        line.SetPosition(0, startPos.position);
        line.SetPosition(1, endPos);
    }
    private void Update()
    {
        m.mainTextureOffset += new Vector2(Time.deltaTime, 0) * moveSpeed;
    }
}
