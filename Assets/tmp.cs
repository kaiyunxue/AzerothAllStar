using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tmp : MonoBehaviour {
    public GameObject aim;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        
	}
	
	// Update is called once per frame
	void Update () {
        agent.SetDestination(aim.transform.position);
        NavMeshPath path = agent.path;
    }
}
