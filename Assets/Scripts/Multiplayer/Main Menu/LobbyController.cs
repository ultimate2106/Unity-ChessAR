using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _connectToLobbyButton;

    [Header("Menu Panels")]
    [SerializeField]
    private GameObject _mainMenuPanel;
    [SerializeField]
    private GameObject _lobbyPanel;

    public InputField _playerNameInput;

    private string _roomName;

    private List<RoomInfo> _roomList;

    [Header("Room Settings")]
    [SerializeField]
    private Transform _roomsContainer;
    [SerializeField]
    private GameObject _roomPrefab;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        _connectToLobbyButton.SetActive(true);
        _roomList = new List<RoomInfo>();
    }

    public void PlayerNameUpdate(string usernameInput)
    {
        PhotonNetwork.NickName = usernameInput;
    }

    public void JoinLobbyOnClick()
    {
        _mainMenuPanel.SetActive(false);
        _lobbyPanel.SetActive(true);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int tmpIndex = 0;
        foreach(RoomInfo room in roomList)
        {
            if(_roomList != null)
            {
                tmpIndex = _roomList.FindIndex(ByName(room.Name));
            } else
            {
                tmpIndex = -1;
            }

            if(tmpIndex != -1)
            {
                _roomList.RemoveAt(tmpIndex);
                Destroy(_roomsContainer.GetChild(tmpIndex).gameObject);
            }

            if(room.PlayerCount > 0)
            {
                _roomList.Add(room);
                ListRoom(room);
            }
        }
    }

    private static Predicate<RoomInfo> ByName(string roomName)
    {
        return delegate (RoomInfo room)
        {
            return room.Name.Equals(roomName);
        };
    }

    private void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tmpListing = Instantiate(_roomPrefab, _roomsContainer);
            RoomButton tmpButton = tmpListing.GetComponent<RoomButton>();
            tmpButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }

    public void OnRoomNameChanged(string nameInput)
    {
        _roomName = nameInput;
    }

    public void CreateRoom()
    {
        Debug.Log("Creating room now.");
        RoomOptions roomOptions = new RoomOptions()
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = (byte)2
        };
        PhotonNetwork.CreateRoom(_roomName, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Room could not be created.. :(");
    }

    // Paired with Cancel Button to go back to Main Menu
    public void MatchmakingCancel()
    {
        _mainMenuPanel.SetActive(true);
        _lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
}
