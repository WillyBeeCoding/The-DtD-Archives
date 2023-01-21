using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Keyblade", menuName = "Assets/Keyblades")]
public class Keyblade : ScriptableObject
{
    // Vars used to search
    public int keybladeID;
    public string keybladeName;
    public string keybladeGame;

    // Vars used to display
    public string gameQuote;
    public string desc;
    public Sprite image;
    public Sprite stats;
    public string strengh;
    public string magic;
    public string critrate;
    public VideoClip footage;
}
