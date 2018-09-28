using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCircle : MonoBehaviour {
    public static CreatureCircle Instacne;
    [SerializeField]
    private GameObject circle;
    private void Awake()
    {
        Instacne = this;
    }
    public void InstitateCircle(GameObject go)
    {
        GameObject instance =  GameObject.Instantiate(circle);
        instance.transform.SetParent(go.transform);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
