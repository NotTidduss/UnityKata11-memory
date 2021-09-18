using UnityEngine;
using UnityEngine.SceneManagement;

public class Memory_Init : MonoBehaviour
{
    void Awake() {
        initializePlayerPrefs();
        SceneManager.LoadScene("1_Memory");
    }

    private void initializePlayerPrefs() {
        // STRING memory_uncoveredTileValue: value of the latest flipped tile
        PlayerPrefs.SetString("memory_uncoveredTileValue", "");
    }
}
