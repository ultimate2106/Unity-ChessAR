using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _sizeText;

    private string _roomName;
    private int _roomSize;
    private int _playerCount;

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(_roomName);
    }

    public void SetRoom(string nameInput, int sizeInput, int countInput)
    {
        _roomName = nameInput;
        _playerCount = countInput;
        _nameText.text = nameInput;
        _sizeText.text = countInput + "/" + sizeInput;
    }
}
