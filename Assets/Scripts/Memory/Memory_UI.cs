using System.Collections.Generic;
using UnityEngine;

public class Memory_UI : MonoBehaviour
{
    [Header ("Scene References")]
    [SerializeField] private Memory_Master master;
    [SerializeField] private Memory_System sys;

    [Header ("Prefab References")]
    [SerializeField] private GameObject playerProfilePrefab;
    [SerializeField] private GameObject memoryTilePrefab;

    [Header ("Custom UI Objects")]
    [SerializeField] private GameObject playerProfilePositionsParent;
    [SerializeField] private GameObject tileTable;

    public bool areTilesLeft = true;


    public void initialize() {
        loadPlayerProfileAreas(sys.playerNames);
        loadMemoryTiles(sys.numberOfPairs);
    }

    public void destroyUncoveredTiles() => findUncoveredTiles().ForEach(tile => Destroy(tile.gameObject));
    public void coverTiles() => findUncoveredTiles().ForEach(tile => tile.coverContent());


    private void loadPlayerProfileAreas(List<string> playerNames) {
        Transform[] playerProfilePositions = playerProfilePositionsParent.GetComponentsInChildren<Transform>();
        int id = 1;

        playerNames.ForEach(name => {
            GameObject playerAreaGameObject = Instantiate(playerProfilePrefab, playerProfilePositions[id]);
            Memory_PlayerProfile playerProfile = playerAreaGameObject.GetComponent<Memory_PlayerProfile>();
            playerProfile.setPlayerProfile(id, name, 0);
            master.addPlayerProfile(playerProfile);
            id++;
        });
    }

    private void loadMemoryTiles(int pairCount) {
        int tileCount = pairCount * 2;

        int distanceBetweenTiles = 180;

        int rowCount = getRowCountFromGivenTileCount(tileCount);
        int columnCount = tileCount; 

        int tileOffset = columnCount % 2 == 0 ? -distanceBetweenTiles/2 : 0;
        
        List<GameObject> tilePositions = fillTilePositionList(new List<GameObject>(), tileCount);

        int currentRow = 0;
        int currentColumn = 0;
        
        int tileDistance = 0;

        tilePositions.ForEach(tilePosition => {
            // TODO: calculate rowCount based on tileCount; then calculate columnCount based on rowCount

            if (currentColumn % 2 == 0) 
                tileDistance = -tileDistance;
            else 
                tileDistance += distanceBetweenTiles * currentColumn;
            
            currentColumn++;
            tilePosition.transform.position = new Vector2(tileDistance + tileOffset, currentRow);
            Instantiate(tilePosition, tileTable.transform);
        });
        
        List<Transform> tilePositionTransforms = new List<Transform>(tileTable.GetComponentsInChildren<Transform>());

        tilePositionTransforms.ForEach(tilePositionTransform => {
            if (!(tilePositionTransform.gameObject.name == tileTable.name)) {
                GameObject memoryTile = Instantiate(memoryTilePrefab, tilePositionTransform);
                master.addTile(memoryTile.GetComponent<Memory_Tile>());
            }
        });
    }

    private int getRowCountRec(int tileCount, int rowCount) => tileCount > 9 ? getRowCountRec(tileCount-9, rowCount+1) : rowCount;

    private int getRowCountFromGivenTileCount(int tileCount) => getRowCountRec(tileCount, 1);

    private List<GameObject> fillTilePositionList(List<GameObject> tilePositionList, int numberOfTiles) {
        switch (numberOfTiles) {
            case 0: return tilePositionList;
            default: 
                GameObject tilePosition = new GameObject();
                tilePosition.name = "tilePos" + numberOfTiles;
                tilePositionList.Add(tilePosition);

                return fillTilePositionList(tilePositionList, --numberOfTiles);
        }
    }

    private List<Memory_Tile> findUncoveredTiles() => fillUncoveredTilesList(new List<Memory_Tile>(), new List<Memory_TileContent>(tileTable.GetComponentsInChildren<Memory_TileContent>()));
    private List<Memory_Tile> fillUncoveredTilesList(List<Memory_Tile> tiles, List<Memory_TileContent> tileContents) {
        tileContents.ForEach(tileContent => tiles.Add(tileContent.getMemoryTile()));
        return tiles;
    } 
}