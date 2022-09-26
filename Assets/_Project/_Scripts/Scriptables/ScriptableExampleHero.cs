using System;
using UnityEngine;


[CreateAssetMenu(fileName = "New Scriptable Example")]
public class ScriptableExampleHero : ScriptableExampleUnitBase {
    public ExampleHeroType HeroType;
 
}

[Serializable]
public enum ExampleHeroType {
    Tarodev = 0,
    Snorlax = 1
}

