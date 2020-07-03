using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int _multiplayerSceneIndex;

    [Header("Menu Panels")]
    [SerializeField]
    private GameObject _lobbyPanel;
    [SerializeField]
    private GameObject _roomPanel;

    [SerializeField]
    private GameObject _startGameButton;

    [Header("Player related")]
    [SerializeField]
    private Transform _playersContainer;
    [SerializeField]
    private GameObject _playerListingPrefab;

    [SerializeField]
    private Text _roomNameDisplay;

    private void ClearPlayerList()
    {
        for(int i = _playersContainer.childCount - 1; i >= 0; --i)
        {
            Destroy(_playersContainer.GetChild(i).gameObject);
        }
    }

    private void ListPlayers()
    {
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            GameObject tmp = Instantiate(_playerListingPrefab, _playersContainer);
            Text tmpText = tmp.transform.GetChild(0).GetComponent<Text>();
            tmpText.text = player.NickName;
        }
    }

    public override void OnJoinedRoom()
    {
        _roomPanel.SetActive(true);
        _lobbyPanel.SetActive(false);

        _roomNameDisplay.text = PhotonNetwork.CurrentRoom.Name;

        if (PhotonNetwork.IsMasterClient)
        {
            _startGameButton.SetActive(true);
        } else
        {
            _startGameButton.SetActive(false);
        }

        ClearPlayerList();
        ListPlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        ClearPlayerList();
        ListPlayers();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ClearPlayerList();
        ListPlayers();
        if (PhotonNetwork.IsMasterClient)
        {
            _startGameButton.SetActive(true);
        }
    }

    public void startGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(_multiplayerSceneIndex);
        }
    }

    IEnumerator RejoinLobby()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.JoinLobby();
    }

    public void BackOnClick()
    {
        _lobbyPanel.SetActive(true);
        _roomPanel.SetActive(false);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(RejoinLobby());
    }
}
