﻿using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

    public class RoomListEntry : MonoBehaviour
    {
        public Text RoomNameText;
        public Text RoomPlayersText;
        public Button JoinRoomButton;

        private string roomName;

        public void Start()
        {
            JoinRoomButton.onClick.AddListener(() =>
            {
                if (PhotonNetwork.InLobby)
                {
                    PhotonNetwork.LeaveLobby();
                }

                GlobalSettings.SetPlayerId(false);

                PhotonNetwork.JoinRoom(roomName);
            });
        }

        public void Initialize(string name, byte currentPlayers, byte maxPlayers)
        {
            roomName = name;

            RoomNameText.text = name;
            RoomPlayersText.text = currentPlayers + " / " + maxPlayers;

        if (currentPlayers == 2)
        {
            JoinRoomButton.interactable = false;
        }
        else 
        {
            JoinRoomButton.interactable = true;
        }
        }
    }