using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobStatusBox : MonoBehaviour {

    public Image healthBar;
    Color oriColor = Color.green;
    Color endColor = Color.red;
    public Gradient gradient;
    public Vector3 uiPos;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.parent.position + uiPos;
        transform.rotation = Quaternion.Euler(0, 0, 0);
	}
    public void ShowHealth(float health)
    {
        healthBar.fillAmount = health;
        healthBar.color = gradient.Evaluate(health);
    }
}
