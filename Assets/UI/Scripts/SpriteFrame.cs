using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteFrame : MonoBehaviour {
    public Image image;
    private void OnEnable()
    {
        image.sprite = null;
    }
    public virtual void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}
