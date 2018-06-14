using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class HeroIcon : ScriptableObject {
    public Texture avatar;
    public Texture sideAvatar;
    public Sprite background;
    public Material material;
    public string heroName;
}
