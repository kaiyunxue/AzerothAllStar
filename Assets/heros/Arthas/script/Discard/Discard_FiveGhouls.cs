using UnityEngine;
using System.Collections;

public class FiveGhouls : MonoBehaviour {
    GameObject[] ghouls;
    public GameObject ghoul;
    public GameObject blade_edge;
    public LineRenderer[] linesoul;
    public bool isborn = true;
    // Use this for initialization
    public void StartFiveGhouls () {
        ghouls = new GameObject[5];
        linesoul = new LineRenderer[5];
        isborn = true;
        for (int i = 0; i < 5; i ++)
        {
            float A, B;
            while (true)
            {
                if (Mathf.Abs(A = Random.Range(-1.4f, 1.4f)) > 0.7f)
                    break;
            }
            while (true)
            {
                if (Mathf.Abs(B = Random.Range(-1.4f, 1.4f)) > 0.7f)
                    break;
            }
            Vector3 randomposition = new Vector3(A, 0, B) + transform.position;
            ghouls[i] = (GameObject)Instantiate(ghoul, randomposition, Quaternion.Euler(0, 0, 0));
            linesoul[i] = ghouls[i].GetComponent<LineRenderer>();
            linesoul[i].enabled = true;
            linesoul[i].SetPosition(0, blade_edge.transform.position);
            linesoul[i].SetPosition(1, ghouls[i].transform.position);
        }
        gameObject.GetComponent<Animator>().CrossFade("ChannelCastOmni [26]", 0.5f);
        StartCoroutine(loop());
    }
    IEnumerator loop()
    {
        isborn = ghouls[0].transform.GetChild(1).GetComponent<GhoulMovement>().IsBorn;
        if (isborn)
        {
            for (int i = 0; i < 5; i++)
            {
                linesoul[i].SetPosition(0, blade_edge.transform.position);
                linesoul[i].materials[0].mainTextureOffset += new Vector2(0.05f, 0);
            }
            yield return null;
            StartCoroutine(loop());
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                linesoul[i].enabled = false;
            }
            gameObject.GetComponent<Animator>().CrossFade("Ready2H [14]", 0.5f);
        }
        yield return null;
    }

 }
