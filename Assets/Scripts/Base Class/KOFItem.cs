using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KOFItem : MonoBehaviour
{
    public KOFItem speller;
    int[] kofID;
    public virtual int GetMaxInstance() 
    {
        return -1;

    }
    protected virtual void Awake()
    {
    }
    protected virtual void OnEnable()
    {
        if (gameObject.layer == 8)
        {
            if (GameController.instance != null)
            {
                GameController.Register.LeftHero.HeroRegister.Enroll(this);
            }
        }
        if (gameObject.layer == 9)
        {
            if (GameController.instance != null)
            {
                GameController.Register.RightHero.HeroRegister.Enroll(this);
            }
        }
    }
    protected void OnDisable()
    {
        if (gameObject.layer == 8)
        {
            if (GameController.instance != null)
            {
                GameController.Register.LeftHero.HeroRegister.Enroll(this);
            }
            else
            {
                Debug.LogWarning("This is not a game scene");
            }
        }
        if (gameObject.layer == 9)
        {
            if (GameController.instance != null)
            {
                GameController.Register.RightHero.HeroRegister.Enroll(this);
            }
            else
            {
                Debug.LogWarning("This is not a game scene");
            }
        }
    }
    protected static T InstantiateByPool<T>(T item) where T: KOFItem
    {
        if(PoolManager.instance == null)
        {
            Debug.LogWarning("No pool manager in scene!");
            T t = Instantiate(item);
            return t;
        }
        else
        {
            T t = PoolManager.instance.PopFromPool(item);
            return t;
        }
    }
    public static T InstantiateByPool<T>(T item, Transform parent, int layer) where T : KOFItem
    {
        T instance = InstantiateByPool(item);
        instance.gameObject.layer = layer;
        instance.transform.SetParent(parent, false);
        instance.gameObject.transform.localPosition = Vector3.zero;
        instance.gameObject.SetActive(true);
        return instance;
    }
    public static T InstantiateByPool<T>(T item, Transform parent, KOFItem speller, int layer) where T : SkillItemsBehaviourController
    {
        T instance = InstantiateByPool(item);
        instance.speller = speller;
        instance.gameObject.layer = layer;
        instance.transform.SetParent(parent, false);
        instance.gameObject.transform.localPosition = Vector3.zero;
        instance.gameObject.SetActive(true);
        return instance;
    }
    public static T InstantiateByPool<T>(T item, Vector3 pos, Transform parent, int layer) where T : KOFItem
    {
        T instance = InstantiateByPool(item);
        instance.gameObject.layer = layer;
        instance.transform.SetParent(parent, false);
        instance.transform.localPosition = pos;
        instance.transform.rotation = Quaternion.Euler(Vector3.zero);
        instance.gameObject.SetActive(true);
        return instance;
    }
    public static T InstantiateByPool<T>(T item, Vector3 pos, Quaternion rot, Transform parent, int layer) where T : KOFItem
    {
        T instance = InstantiateByPool(item);
        instance.gameObject.layer = layer;
        instance.transform.SetParent(parent, false);
        instance.transform.localPosition = pos;
        instance.transform.localRotation = rot;
        instance.gameObject.SetActive(true);
        return instance;
    }
    public static T InstantiateByPool<T>(T item, Vector3 worldPos, Quaternion worldRot, Transform parent, int layer, bool isKeepWorld) where T : KOFItem
    {
        if(isKeepWorld)
        {
            T instance = InstantiateByPool(item);
            instance.gameObject.layer = layer;
            instance.transform.SetParent(parent, false);
            instance.transform.position = worldPos;
            instance.transform.rotation = worldRot;
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            return InstantiateByPool(item, worldPos, worldRot, parent, layer);
        }
    }
    public static T InstantiateByPool<T>(T item, Vector3 pos, Vector3 rot, Transform parent, int layer) where T : KOFItem
    {
        T instance = InstantiateByPool(item);
        instance.gameObject.layer = layer;
        instance.transform.SetParent(parent, false);
        instance.transform.localPosition = pos;
        instance.transform.localRotation = Quaternion.Euler(rot);
        instance.gameObject.SetActive(true);
        return instance;
    }
    public static void DestoryByPool<T>(T item) where T:KOFItem
    {
        if(PoolManager.instance == null)
        {
            Debug.LogWarning("No pool manager in scene!");
            Destroy(item);
        }
        else
        {
            item.gameObject.SetActive(false);
            PoolManager.instance.PushToPool(item);
        }
    }
    public static void DestoryByPool(GameObject item, string name)
    {
        if (PoolManager.instance == null)
        {
            Debug.LogWarning("No pool manager in scene!");
            Destroy(item);
        }
        else
        {
            item.gameObject.SetActive(false);
            item.AddComponent<KOFItem>();
            PoolManager.instance.PushToPool(item, name);
        }
    }
}