using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ClipSubtitles")]
public class ClipsSubtitles : ScriptableObject
{
    public List<Subtitle> subtitles;
}

[Serializable]
public class Subtitle
{
    public string clipName;
    public string subtitles;
}