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
        if (go.layer == 8)
            instance.GetComponent<MaterialController>().SetColor(Color.red);
        else if (go.layer == 9)
            instance.GetComponent<MaterialController>().SetColor(Color.blue);
    }
    public void InstitateCircle(GameObject go, Vector3 localPos, float scale)
    {
        GameObject instance = GameObject.Instantiate(circle);
        instance.transform.SetParent(go.transform);
        instance.transform.localPosition = Vector3.zero;
        instance.transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (go.layer == 8)
            instance.GetComponent<MaterialController>().SetColor(Color.red);
        else if (go.layer == 9)
            instance.GetComponent<MaterialController>().SetColor(Color.blue);
        instance.transform.localPosition = localPos;
        instance.transform.localScale = new Vector3(scale, scale, scale);
    }
}
