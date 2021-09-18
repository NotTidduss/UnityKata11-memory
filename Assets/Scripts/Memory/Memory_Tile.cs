using UnityEngine;
using UnityEngine.UI;

public class Memory_Tile : MonoBehaviour
{
    [Header ("Prefab References")]
    [SerializeField] private GameObject tileContent;
    [SerializeField] private Button tileButton;
    [SerializeField] private Text tileValueText;

    [Header ("Properties")]
    public string tileValue;


    public void uncoverContent() {
        toggleVisibility();
        PlayerPrefs.SetString("memory_uncoveredTileValue", tileValue);
    }

    public void coverContent() => toggleVisibility();


    private void toggleVisibility() {
        tileContent.SetActive(!tileContent.activeInHierarchy);
        tileButton.interactable = !tileButton.interactable;
    }
}
