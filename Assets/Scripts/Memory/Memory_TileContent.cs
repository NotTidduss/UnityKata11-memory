using UnityEngine;

public class Memory_TileContent : MonoBehaviour
{
    public Memory_Tile getMemoryTile() => transform.parent.gameObject.GetComponent<Memory_Tile>();
}
