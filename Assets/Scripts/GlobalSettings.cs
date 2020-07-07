using Photon.Pun;
using UnityEngine;

public enum ChessFiguresColor
{
    White,
    Black
}

public static class GlobalSettings
{
    static int _playerId = 0;

    static ChessFiguresColor _player1Color;
    static ChessFiguresColor _player2Color;

    public static void SetPlayerId(bool isPlayer1)
    {
        if (isPlayer1)
            _playerId = 1;
        else
            _playerId = 2;
    }

    public static void ResetPlayerId()
    {
        _playerId = 0;
    }

    public static int GetPlayerId()
    {
        return _playerId;
    }

    public static ChessFiguresColor GetPlayerColor()
    {
        if(_playerId == 1)
        {
            return _player1Color;
        } else
        {
            return _player2Color;
        }
    }
    public static void SetPlayerColors(ChessFiguresColor p1, ChessFiguresColor p2)
    {
        _player1Color = p1;
        _player2Color = p2;
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