using UnityEngine;
using UnityEngine.UI;

public class Memory_PlayerProfile : MonoBehaviour
{
    [Header("Prefab References")]
    [SerializeField] private Image background; 
    [SerializeField] private Text playerNameText; 
    [SerializeField] private Text pointsText; 

    private int id;
    private string playerName;
    private int points;


    public void setPlayerProfile(int id, string playerName, int points) {
        setId(id);
        setPlayerName(playerName);
        setPoints(points);
    }

    public void addPoint() => setPoints(points+1);

    public string toString() {
        return "Player Profile #" + id
                + ", Player Name is " + playerName
                + ", Current Points: " + points;
    }


    private void setId(int id) => this.id = id;
    private void setPlayerName(string playerName) {
        this.playerName = playerName;
        updatePlayerNameText();
    } 
    private void setPoints(int points) {
        this.points = points;
        updatePointsText();
    } 
    private void updatePlayerNameText() => playerNameText.text = playerName;
    private void updatePointsText() => pointsText.text = points.ToString();
}
