using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScenesName
{
    public static string Instruction = "Instructions";
    public static string ChoseHero = "ChoseHero";
    public static string Attention = "Attention";
    public static string MainTitle = "MainTitle";
    public static string FightingScene = "firelandDemo";
}
public enum m_Scene
{
    Attention,
    MainMenu,
    ChooseHero,
    Instructions,
    FightScene
}
public abstract class _SceneManager : MonoBehaviour {
    public abstract void TurnFrom(m_Scene scene);
}
