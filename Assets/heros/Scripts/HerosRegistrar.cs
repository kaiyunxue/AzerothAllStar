using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerosRegistrar : MonoBehaviour {
    Dictionary<string, List<GameObject>> itemsRegistrar = new Dictionary<string, List<GameObject>>();
    public void Remove<T>(T t) where T: KOFItem
    {
        List<GameObject> list;
        if (itemsRegistrar.TryGetValue(t.GetType().FullName, out list))
            list.Remove(t.gameObject);
        else
            Debug.LogWarning("Didn't enroll first");
    }
    public void Enroll<T>(T item) where T: KOFItem
    {
        if(itemsRegistrar.ContainsKey(item.GetType().FullName))
        {
            List<GameObject> list;
            itemsRegistrar.TryGetValue(item.GetType().FullName, out list);
            list.Add(item.gameObject);
        }
        else
        {
            List<GameObject> list = new List<GameObject>
            {
                item.gameObject
            };
            itemsRegistrar.Add(item.GetType().FullName, list);
        }
    }
    public GameObject[] SearchCompontent(string name)
    {
        List<GameObject> list;
        if (itemsRegistrar.TryGetValue(name, out list))
        {

            return list.ToArray();
        }
        else
        {
            return null;
        }
    }
    private void Awake()
    {

    }

    public GameObject[] GetAllGameItems()
    {
        List<GameObject> list = new List<GameObject>();
        foreach(List<GameObject> l in itemsRegistrar.Values)
        {
            list.AddRange(l);
        }
        return list.ToArray();
    }
    private void Update()
    {
        foreach(var v in GetAllGameItems())
        {
            Debug.Log(v.name);
        }
    }
}
