using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Memory_Master : MonoBehaviour
{
    [Header ("Scene References")]
    [SerializeField] private Memory_UI ui;

    private Memory_GameState gameState;
    private List<Memory_Tile> tiles;
    private List<Memory_PlayerProfile> playerProfiles;
    private int activePlayerId;
    private string uncoveredTileValue1;
    private string uncoveredTileValue2;


    void Start() {
        gameState = Memory_GameState.GAME_START;
        StartCoroutine("Play");
    } 


    IEnumerator Play() {
        while(true) {
            switch (gameState) {
                case Memory_GameState.GAME_START: initialize(); break;
                case Memory_GameState.ZERO_UNCOVERED: handleTileUncovering(ref uncoveredTileValue1, Memory_GameState.ONE_UNCOVERED); break;
                case Memory_GameState.ONE_UNCOVERED: handleTileUncovering(ref uncoveredTileValue2, Memory_GameState.TWO_UNCOVERED); break;
                case Memory_GameState.TWO_UNCOVERED: checkTiles(); break;
                case Memory_GameState.GAME_END: terminate(); break;
            }
            
            yield return null;
        }
    }


    public void addPlayerProfile(Memory_PlayerProfile playerProfile) => playerProfiles.Add(playerProfile);
    public void addTile(Memory_Tile tile) => tiles.Add(tile);


    private void initialize() {
        // set essential game variables to initial values
        playerProfiles = new List<Memory_PlayerProfile>();
        tiles = new List<Memory_Tile>();
        PlayerPrefs.SetString("memory_uncoveredTileValue", "");

        // load PlayerProfiles and MemoryTiles into the game
        ui.initialize();

        // move on
        gameState = Memory_GameState.ZERO_UNCOVERED;
    }

    // When a tile is uncovered, store its value and adjust essential values
    // @param REF STRING valueUncoveredTile: the value to be stored in uncoveredTileValueX
    // @param Memory_GameState nextState: the next state of the game
    private void handleTileUncovering(ref string uncoveredTileValue, Memory_GameState nextState) {
        if (PlayerPrefs.GetString("memory_uncoveredTileValue") != "") {
            uncoveredTileValue = PlayerPrefs.GetString("memory_uncoveredTileValue");
            PlayerPrefs.SetString("memory_uncoveredTileValue", "");
            gameState = nextState;
        }
    }

    // Check the values of flipped Tiles and act accordingly. Also, switch state based on tiles left.
    private void checkTiles() {
        // TODO: create list of tiles, extract ui methods to master
        if (uncoveredTileValue1 == uncoveredTileValue2) {
            playerProfiles[activePlayerId].addPoint();
            ui.destroyUncoveredTiles();
        }
        else {
            ui.coverTiles();
        }

        if (ui.areTilesLeft) gameState = Memory_GameState.ZERO_UNCOVERED;
        else gameState = Memory_GameState.GAME_END;
    }

    private void terminate() {}
}
