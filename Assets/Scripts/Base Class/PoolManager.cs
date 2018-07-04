using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    public static PoolManager instance;

    Dictionary<string, Stack<KOFItem>> objectPool = new Dictionary<string, Stack<KOFItem>>();

    public void PushToPool<T>(T item) where T: KOFItem
    {
        if (item.gameObject.activeSelf)
            Debug.LogError("The item hasn't been disabled!");
        string className = item.GetType().FullName;
        int maxN = item.GetMaxInstance();
        if (maxN < 0)
            Debug.LogError("No Set The Max Instance Number in class:" + className);
        if(objectPool.ContainsKey(className))
        {
            Stack<KOFItem> pool;
            objectPool.TryGetValue(className, out pool);
            if (pool.Count < maxN)
            {
                item.transform.SetParent(transform);
                pool.Push(item);
            }
            else
            {
                Debug.Log("Destory the item by the POOL");
                Destroy(item.gameObject);
            }
        }
        else
        {
            Stack<KOFItem> pool = new Stack<KOFItem>();
            item.transform.SetParent(transform);
            pool.Push(item);
            objectPool.Add(className, pool);
        }
    }
    public T PopFromPool<T>(T item) where T:KOFItem
    {
        Stack<KOFItem> pool;
        if (objectPool.TryGetValue(item.GetType().FullName, out pool) && pool.Count > 0)
        {
            T t = (T)pool.Pop();
            return t;
        }
        else
        {
            T t = Instantiate(item);
            t.gameObject.SetActive(false);
            return t;
        }
    }
    public void PushToPool(GameObject item, string name)
    {
        if (item.gameObject.activeSelf)
            Debug.LogError("The item hasn't been disabled!");
        if (objectPool.ContainsKey(name))
        {
            Stack<KOFItem> pool;
            objectPool.TryGetValue(name, out pool);
            item.transform.SetParent(transform);
            pool.Push(item.AddComponent<KOFItem>());

        }
        else
        {
            Stack<KOFItem> pool = new Stack<KOFItem>();
            item.transform.SetParent(transform);
            pool.Push(item.AddComponent<KOFItem>());
            objectPool.Add(name, pool);
        }
    }
    public KOFItem PopFromPool(string name)
    {
        Stack<KOFItem> pool;
        if (objectPool.TryGetValue(name, out pool) && pool.Count > 0)
        {
            KOFItem t = pool.Pop();
            return t;
        }
        else
        {
            return null;
        }
    }
    private void Awake()
    {
        instance = this;
        gameObject.name = "PoolManager";
    }
    // Use this for initialization
    void Start ()
    {
	}
	

	// Update is called once per frame
	void Update () {
		
	}
}
