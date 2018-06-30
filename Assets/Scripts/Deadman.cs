using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadman : MonoBehaviour {
    float health_;
    public Hero hero;
    public State state;
    public ISkillManager skillManager;
    private void Awake()
    {
        hero = GetComponent<Hero>();
        state = GetComponent<State>();
        skillManager = GetComponent<ISkillManager>();
    }
    void Start () {
        StartCoroutine(watchDog());
	}
	
	// Update is called once per frame
	void Update () {
        if (state.Health <= 0)
            state.Health = 0;
        if (Input.GetKey(KeyCode.D))
        {
            transform.position -= new Vector3(0.1f, 0, 0);
        }
    }

    IEnumerator watchDog()
    {
        if (state.Health == state.MaxHealth)
        {
            yield return new WaitForEndOfFrame();
            StartCoroutine(watchDog());
        }
        else if(state.Health == health_)
        {
            StartCoroutine(recover());
        }
        else
        {
            health_ = state.Health;
            yield return new WaitForSeconds(5);
            StartCoroutine(watchDog());
        }
    }
    IEnumerator recover()
    {
        if(state.Health == state.MaxHealth)
        {
            StartCoroutine(watchDog());
            yield return null;
        }
        else
        {
            yield return new WaitForEndOfFrame();
            state.Health += 2;
            StartCoroutine(recover());
        }
    }
}
