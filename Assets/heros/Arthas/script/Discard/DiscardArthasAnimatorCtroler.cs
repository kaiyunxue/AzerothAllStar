using UnityEngine;
using System.Collections;
public class ArthasAnimatorCtroler : MonoBehaviour {
    public Animator Animctrlor;
    public bool Begin;
    public CharacterController cc;
    public string inputstream;
    public GameObject target;
    public int ordinaryAttack;
    public bool keyboardlock;


    Skills CallGhouls = new Skills(10);
    public bool sprint;
    public float sprit_time;
    Vector3 moveDirection = new Vector3(0, 0, 0);
    // Use this for initialization
    void Start () {
        sprit_time = 0.65f;
        //inputstream = this.gameObject.GetComponent<InputStream>().getInputString();
        keyboardlock = true;
        Animctrlor = this.gameObject.GetComponent<Animator>();
        cc = this.gameObject.GetComponent<CharacterController>();
        sprint = false;
        Animctrlor.speed = 1;
        ordinaryAttack = 0;
    }

    // Update is called once per frame
    void Update() {
        float z = transform.position.z;
        float y = transform.position.y;
        transform.position = new Vector3(0, y, z);

//        inputstream = this.gameObject.GetComponent<InputStream>().getInputString();
        if (keyboardlock)
        {
            jump();
            if (inputstream == "SDSAJ" && IsGrounded())
            {
                keyboardlock = false;
                Animctrlor.CrossFade("Attack2H [0]", 0);
            }
            //call ghouls
            else if(inputstream.Substring(2) == "SAK" && IsGrounded() && CallGhouls.Ready)
            {
                StartTiming(CallGhouls);
                Debug.Log("Call for ghouls");
                gameObject.GetComponent<FiveGhouls>().StartFiveGhouls();
                keyboardlock = false;
            }
            //
            //sprint
            else if (inputstream.Substring(3) == "DD" || sprint && IsGrounded())
            {
               if(sprit_time>0)
                {
                    sprint = true;
                    Animctrlor.CrossFade("Run [15]", 0);
                    cc.Move(new Vector3(0, 0, -0.1f));
                    sprit_time -= Time.deltaTime;
                }
                else
                {
                    sprint = false;
                    sprit_time = 0.65f;
                    Animctrlor.CrossFade("Ready2H [14]", 0);
      
                }
            }
            //sprint ends
            //doublejumpback
            else if (inputstream.Substring(3) == "AA" && IsGrounded())
            {
                keyboardlock = false;   
                jump(new Vector3(3f, 0, 0), 3);            
            }
            //end
            //ordinaryAttack
            else if (inputstream.Substring(2) == "JJJ" && ordinaryAttack == 2 && IsGrounded())
            {
                Animctrlor.CrossFade("Attack2H [35] 0", 0);
                keyboardlock = false;
                ordinaryAttack = 0;
            }
            else if (inputstream.Substring(3) == "JJ" && ordinaryAttack == 1 && IsGrounded())
            {
                Animctrlor.CrossFade("Attack2H [1] 0", 0);
                ordinaryAttack = 2;
                keyboardlock = false;
            }
            else if (inputstream.Substring(4) == "J" && IsGrounded())
            {   
                Animctrlor.CrossFade("Attack2H [0] 0", 0);
                ordinaryAttack = 1;
                keyboardlock = false;
            }
            //ordinaryAttack ends
            //ordinaryMove
            else if (inputstream.Substring(4) == "J" && !IsGrounded())
            {
                Animctrlor.CrossFade("Attack2H [35] 0", 0);
                keyboardlock = false;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKeyUp(KeyCode.A))
            {
                Animctrlor.CrossFade("Walk [20] 0", 0);
                cc.Move(new Vector3(0, 0, 0.04f));
                if (Input.GetKeyUp(KeyCode.A) && !Input.GetKey(KeyCode.D))
                {
                    Animctrlor.CrossFade("Ready2H [14]", 0.3f);
                }
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKeyUp(KeyCode.D))
            {
                Animctrlor.CrossFade("Walk [20]", 0);
                cc.Move(new Vector3(0, 0, -0.05f));
                if (Input.GetKeyUp(KeyCode.D))
                {
                    Animctrlor.CrossFade("Ready2H [14]", 0.3f);
                }
            }
            //OrdinaryMove ends
        }
        else
        {
            if (Animctrlor.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 || Animctrlor.GetCurrentAnimatorStateInfo(0).IsName("Ready2H [14]")) 
            {
                keyboardlock = true;
                Animctrlor.CrossFade("Ready2H [14]", 0.1f);
            }
        }
        moveDirection.y -= 15 * Time.deltaTime;
        this.gameObject.GetComponent<CharacterController>().Move(moveDirection * Time.deltaTime);
        if (IsGrounded())
        {
            moveDirection = Vector3.zero;
        }
    }
    void jump()
    {
        if (IsGrounded())
        {
            moveDirection = transform.TransformDirection(moveDirection);
            // 空格键控制跳跃  
            if(Input.GetKeyDown(KeyCode.W))
                moveDirection.y = 5;
        }
//        inputstream = this.gameObject.GetComponent<InputStream>().getInputString();
        int input_l = inputstream.Length;
    }
    void jump(Vector3 dir,float height)
    {
        if (IsGrounded())
        {
            moveDirection = dir;
            moveDirection = transform.TransformDirection(moveDirection);
            // 空格键控制跳跃  
            moveDirection.y = height;
        }
        
        int input_l = inputstream.Length;
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.02f);
    }
    IEnumerator timing(Skills skill)
    {
        skill.remainTime -= Time.deltaTime;
        Debug.Log(skill.remainTime);
        if (skill.remainTime <= 0)
        {
            skill.Ready = true;
            skill.remainTime = skill.CoolingTime;
            yield return null;
        }
        else
        {
            yield return null;
            StartCoroutine(timing(skill));
        }
    }
    void StartTiming(Skills skill)
    {
        skill.Ready = false;
        StartCoroutine(timing(skill));
    }
}
class Skills
{
    public float CoolingTime;
    public bool Ready;
    public float remainTime;
    public Skills()
    {
        CoolingTime = 0;
        remainTime = 0;
        Ready = true;
    }
    public Skills(float t)
    {
        Ready = true;
        remainTime = t;
        CoolingTime = t;
    }
    public Skills(float t , bool ready)
    {
        Ready = ready;
        CoolingTime = t;
        remainTime = t;
    }
}
