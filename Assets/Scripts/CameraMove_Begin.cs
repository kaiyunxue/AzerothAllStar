using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove_Begin : MonoBehaviour {
    public AnimationCurve curve;
    float time = 0;
    public GameObject go;
    public AudioSource[] sources;

    private void Awake()
    {
        sources = go.GetComponentsInChildren<AudioSource>();
    }

    private void Update()
    {
        transform.position = new Vector3(curve.Evaluate(time), 0.78f, 0);
        time += Time.deltaTime;
        foreach(var s in sources)
        {
            if(Vector3.Distance(transform.position, s.transform.position) <= 3)
            {
                Debug.Log(s.name);
                if(!s.isPlaying)
                {
                    s.Play();
                }
            }
        }
        if(time >= 10)
        {
            SceneManager.LoadScene(ScenesName.MainTitle);
        }
    }
}
