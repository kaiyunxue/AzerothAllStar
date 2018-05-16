using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDetect : MonoBehaviour {
    public UnityEvent e;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "right")
        {
            e.Invoke();
        }
    }
}
