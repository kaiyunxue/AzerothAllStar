using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoltenSeed : SkillItemsBehaviourController
{
    static int maxInstanceNum = 2;
    public GameObject seed;
    public GameObject[] targets;
    public FireElement2 fireElement;
    GameObject[] seed_copy = new GameObject[10];
    public int Num;
    public float speed;
    public float Radius;
    Coroutine[] coroutines = new Coroutine[7];

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < 7; i++)
        {
            seed_copy[i] = Instantiate(seed, transform, false);
            seed_copy[i].SetActive(false);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        for (int i = 0; i < 7; i++)
        {
            if (i < Num)
            {
                seed_copy[i].SetActive(true);
            }
            else
            {
                seed_copy[i].SetActive(false);
            }
        }
        StartCoroutine(SeedsRotate());
    }
    public void SeedForward()
    {
        for (int i = 0; i < Num; i++)
        {
            StopCoroutine(coroutines[i]);
            StartCoroutine(SeedGo(i));
        }
    }
    IEnumerator SeedGo(int i)
    {
        yield return new WaitForEndOfFrame(); 
        GameObject go = seed_copy[i];
        go.transform.LookAt(targets[i].transform.position);
        go.transform.position += go.transform.forward * Time.deltaTime * 5;
        float dis = Vector3.Distance(targets[i].transform.position, go.transform.position);
        if (dis >= 0.2f)
            StartCoroutine(SeedGo(i));
        else
        {
            KOFItem.InstantiateByPool(fireElement, targets[i].transform.position += new Vector3(0,0.3f,0), new Quaternion(),GameController.instance.transform, gameObject.layer, true);
            targets[i].GetComponent<FireTrap>().BeTrumped();
            seed_copy[i].SetActive(false);
            seed_copy[i].transform.localPosition = Vector3.zero;
            Num--;
            if(Num == 0)
            {
                StartCoroutine(DestorySelf());
            }
        }
    }
    IEnumerator SeedsRotate()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < Num; i++)
        {
            coroutines[i] = StartCoroutine(SeedRotate(seed_copy[i], i));
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator SeedRotate(GameObject seed, int i)
    {
        yield return new WaitForEndOfFrame();
        if (seed == null)
            yield break;
        int n = seed_copy.Length;
        int angle_1 = 360 / n * i;
        int angle_2 = 180 / n * i;
        float dis = Vector3.Distance(seed.transform.position, transform.position);
        //Debug.Log(dis);
        if (dis <= 0.0001)
        {
            seed.transform.position += new Vector3(0, Mathf.Sin(angle_2), Mathf.Cos(angle_2)).normalized * Time.deltaTime;
            seed.transform.RotateAround(transform.position, new Vector3(0, -Mathf.Cos(angle_2), Mathf.Sin(angle_2)), speed);
            coroutines[i] = StartCoroutine(SeedRotate(seed, i));
        }
        else if (dis <= Radius)
        {
            Vector3 dir = transform.position - seed.transform.position;
            seed.transform.position -= dir.normalized * Time.deltaTime;
            seed.transform.RotateAround(transform.position, new Vector3(0, -Mathf.Cos(angle_2), Mathf.Sin(angle_2)), speed);
            coroutines[i] = StartCoroutine(SeedRotate(seed, i));
        }
        else
        {
            seed.transform.RotateAround(transform.position, new Vector3(0, -Mathf.Cos(angle_2), Mathf.Sin(angle_2)), speed);
            coroutines[i] = StartCoroutine(SeedRotate(seed, i));
        }

    }
    public override int GetMaxInstance()
    {
        return maxInstanceNum;
    }
    public static MoltenSeed InstantiateByPool(MoltenSeed item, Transform parent, int layer, int seedNum, GameObject[] targets)
    {
        MoltenSeed instance = InstantiateByPool(item);
        instance.Num = seedNum;
        instance.targets = targets;
        instance.gameObject.layer = layer;
        instance.transform.SetParent(parent, false);
        instance.gameObject.transform.localPosition = Vector3.zero;
        instance.gameObject.SetActive(true);
        return instance;
    }
}


