using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDBar : MonoBehaviour {

    [SerializeField]
    protected Transform startPos;
    [SerializeField]
    protected Transform endPos;
    [SerializeField]
    protected Transform pathLine;
    [SerializeField]
    protected SpriteFrame frame;
    Queue<SpriteFrame> instances = new Queue<SpriteFrame>();


    private void Awake()
    {
        for (int i = 0; i < 20; i++){
            instances.Enqueue(Instantiate(frame, startPos.position, Quaternion.Euler(Vector3.zero), pathLine));
        }

    }
    public virtual void StartCooling(Sprite logo ,float time)
    {
        if (instances.Count == 0)
            Debug.LogError("No enough space for frame!");
        SpriteFrame instance = instances.Dequeue();
        instance.gameObject.SetActive(true);
        instance.SetSprite(logo);
        Vector3 speed = (endPos.position - startPos.position) / time * Time.fixedDeltaTime;
        StartCoroutine(CoolingBehave(instance, speed, time));
    }
    IEnumerator CoolingBehave(SpriteFrame frame, Vector3 speed, float maxTime, float time = 0)
    {
        frame.transform.position += speed;
        time += Time.fixedDeltaTime;
        if (time >= maxTime)
        {
            frame.gameObject.SetActive(false);
            frame.transform.position = startPos.position;
            instances.Enqueue(frame);
            yield return null;
        }
        else
        {
            yield return new WaitForFixedUpdate();
            StartCoroutine(CoolingBehave(frame, speed, maxTime, time));
        }
    }
}
