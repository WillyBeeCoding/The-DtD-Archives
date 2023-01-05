using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Keyblade Database", menuName = "Assets/Databases/Keyblade Database")]
public class KeybladeDatabase : ScriptableObject
{
    public List<Keyblade> allKeyblades;

    public void loadKeyblades() { //called by the Master Database when it wakes up
        Object[] resources = Resources.LoadAll("Keyblades", typeof(Keyblade));
        allKeyblades = new List<Keyblade>();
        foreach (Keyblade k in resources)
            allKeyblades.Add(k);
    }

    public Keyblade GetKeyblade(int id) {
        return allKeyblades.Find(keyblade => keyblade.keybladeID == id);
    }

    public Keyblade GetKeyblade(string name, string gameName) {
        return allKeyblades.Find(keyblade => keyblade.keybladeName == name && keyblade.keybladeGame == gameName);
    }

    public List<Keyblade> GetKeybladesByName(string name) {
        return allKeyblades.FindAll(keyblade => keyblade.keybladeName == name);
    }

    public List<Keyblade> GetKeybladesByGame(string gameName) {
        return allKeyblades.FindAll(keyblade => keyblade.keybladeGame == gameName);
    }

    public List<string> GetGamesWithKeybladeName(string name) {
        List<string> games = new List<string>();
        foreach (Keyblade k in GetKeybladesByName(name)) games.Add(k.keybladeGame);
        return games;
    }
}
