using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[Serializable]
public class ScoreEntry
{
    public string name;
    public float time;
    public int level;

    public ScoreEntry(string name, float time, int level)
    {
        this.name = name;
        this.time = time;
        this.level = level;
    }

    //public override string ToString()
    //{
    //    return $"{name} died at {time} and reached level {level}";
    //}
}

public class ScoreData
{
    public List<ScoreEntry> scoreEntries = new();


}
