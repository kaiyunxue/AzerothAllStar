using UnityEngine;
using System.Collections;

public class GhoulMovement : MonoBehaviour {
    public CharacterController cc;
    public GameObject plane;
    public GameObject Ghoul;
    public GameObject target;
    public Animator animator_ctrler;
    public float time;
    public float health;
    public float dis;
    public float speed;
    public Vector3 targetposition;
    public bool IsBorn;
	// Use this for initialization
    void Awake()
    {
        IsBorn = true;
        FindTarget();
        Ghoul.transform.LookAt(target.transform);
    }
	void Start () {
        IsBorn = true;
        plane.SetActive(true);
        time = 20;
        health = 100;
        dis = 0;
        speed = 3;
        animator_ctrler = GetComponent<Animator>();
        float A, B;
        while(true)
        {
            if (Mathf.Abs(A = Random.Range(-1.4f, 1.4f)) > 0.7f)
                break;
        }
        while (true)
        {
            if (Mathf.Abs(B = Random.Range(-1.4f, 1.4f)) > 0.7f)
                break;
        }
        targetposition = new Vector3(A, 0, B) + target.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(IsBorn);
        IsBorn = animator_ctrler.GetCurrentAnimatorStateInfo(0).IsName("Birth [25]");
        if (!IsBorn)
        {
            plane.SetActive(false);
            dis = Vector3.Distance(Ghoul.transform.position, targetposition);
            if (dis >= 0.7f)
            {
                Ghoul.transform.LookAt(targetposition);
                Vector3 dir = Vector3.Normalize(targetposition - transform.position);
                //.transform.position += dir * speed * Time.deltaTime;
                cc.Move(dir * speed * Time.deltaTime);
            }
            else
            {
                targetposition = target.transform.position;
                Ghoul.transform.LookAt(targetposition);
            }
            time -= Time.deltaTime;
        }
        if (time < -3)
            Destroy(Ghoul);
        animator_ctrler.SetFloat("Distance", dis);
        animator_ctrler.SetFloat("health", health);
        animator_ctrler.SetFloat("time", time);      
	}
    void FindTarget()
    {
        target = GameObject.Find("Guldan");
    }
}
