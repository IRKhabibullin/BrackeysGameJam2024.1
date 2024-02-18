using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipSubtitles")]
public class ClipsSubtitles : ScriptableObject
{
    public List<Subtitle> subtitles;

    public string GetTextByKey(string key)
    {
        return subtitles.First(s => s.subKey == key).subtitles;
    }
}

[Serializable]
public class Subtitle
{
    public string subKey;
    public string subtitles;
}