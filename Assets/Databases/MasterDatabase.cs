using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MasterDatabase : MonoBehaviour
{
    private static MasterDatabase instance;
    private static Keyblade currentKeyblade;
    public KeybladeDatabase keyblades;

    private void Awake() {
        if (instance == null) {
            instance = this;
            keyblades.loadKeyblades(); //load up all the keyblades in the resources folder
            SetCurrentKeyblade(keyblades.GetKeyblade(1));
            DontDestroyOnLoad(gameObject); //makes sure it doesn't get destroyed moving between scenes
        } else {
            Destroy(gameObject);
        }
    }

    public static Keyblade GetKeybladeByID(int id) {
        return instance.keyblades.GetKeyblade(id);
    }

    public static Keyblade GetKeybladeByNameAndGame(string name, string game) {
        return instance.keyblades.GetKeyblade(name, game);
    }

    public static List<Keyblade> GetKeybladesByName(string name) {
        return instance.keyblades.GetKeybladesByName(name);
    }

    public static List<Keyblade> GetKeybladesByGame(string game) {
        return instance.keyblades.GetKeybladesByGame(game);
    }

    public static List<string> GetGamesWithKeybladeName(string name) {
        return instance.keyblades.GetGamesWithKeybladeName(name);
    }

    public static Keyblade GetCurrentKeyblade() {
        return currentKeyblade;
    }

    public static void SetCurrentKeyblade(Keyblade keyblade) {
        currentKeyblade = keyblade;
    }
}
