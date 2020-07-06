using Photon.Pun;
using UnityEngine;

public enum ChessFiguresColor
{
    White,
    Black
}

public static class GlobalSettings
{
    static int playerId = 0;

    static ChessFiguresColor player1Color;
    static ChessFiguresColor player2Color;

    public static void SetPlayerId(bool isPlayer1)
    {
        if (isPlayer1)
            playerId = 1;
        else
            playerId = 2;
    }

    public static void ResetPlayerId()
    {
        playerId = 0;
    }

    public static int GetPlayerId()
    {
        return playerId;
    }

    public static ChessFiguresColor GetPlayerColor()
    {
        if(playerId == 1)
        {
            return player1Color;
        } else
        {
            return player2Color;
        }
    }
    public static void SetPlayerColors(ChessFiguresColor p1, ChessFiguresColor p2)
    {
        player1Color = p1;
        player2Color = p2;
    }

    public static void InitGame()
    {
        PhotonView photonView = GameObject.Find("PlayerManager").GetComponent<PhotonView>();
        InitPlayerSettings(photonView);
    }

    static void InitPlayerSettings(PhotonView photonView)
    {
        System.Random rnd = new System.Random();
        bool isPlayer1White = rnd.Next(0, 2) == 0;
        photonView.RPC("AssignFiguresForPlayer", RpcTarget.All, isPlayer1White);
    }
}