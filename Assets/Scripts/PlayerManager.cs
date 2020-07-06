using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [PunRPC]
    public void AssignFiguresForPlayer(bool isPlayer1White)
    {
        if (isPlayer1White)
        {
            GlobalSettings.SetPlayerColors(ChessFiguresColor.White, ChessFiguresColor.Black);
        }
        else
        {
            GlobalSettings.SetPlayerColors(ChessFiguresColor.Black, ChessFiguresColor.White);
        }

        Text yourColor = GameObject.Find("YourColor").GetComponent<Text>();
        yourColor.text = "Deine Farbe ist: " + GlobalSettings.GetPlayerColor().ToString();
    }
}
