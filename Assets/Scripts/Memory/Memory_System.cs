using System.Collections.Generic;
using UnityEngine;

public class Memory_System : MonoBehaviour
{
    // TODO: rework - enable adding player via menu
    public List<string> playerNames = new List<string>() { "Player 1", "2P" };

    [Range(1,30)]
    public int numberOfPairs = 1;
}

public enum Memory_GameState {
    GAME_START,
    ZERO_UNCOVERED,
    ONE_UNCOVERED,
    TWO_UNCOVERED,
    GAME_END
}