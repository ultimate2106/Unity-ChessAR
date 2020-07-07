﻿using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _whiteFigures;
    [SerializeField]
    private GameObject _blackFigures;

    [SerializeField]
    private Text _yourColorText;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
        ManageOwnership();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player..");
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }

    private void ManageOwnership()
    {
        if(GlobalSettings.GetPlayerId() != 0)
        {
            Debug.Log("Manage wird aufgerufen..");
            ChessFiguresColor myColor = GlobalSettings.GetPlayerColor();
            _yourColorText.text = "Deine Farbe: " + myColor.ToString();

            if (myColor == ChessFiguresColor.White)
            {
                PhotonView[] figureViews = _whiteFigures.GetComponentsInChildren<PhotonView>();
                GetOwnership(figureViews);
            } else
            {
                PhotonView[] figureViews = _blackFigures.GetComponentsInChildren<PhotonView>();
                GetOwnership(figureViews);
            }
        }
    }

    //TODO: Eventuell lieber ein RequestOwnership
    private void GetOwnership(PhotonView[] figureViews)
    {
        Debug.Log("Get Ownership");
        PhotonView playerView = player.GetComponent<PhotonView>();
        foreach (PhotonView view in figureViews)
        {
            view.TransferOwnership(playerView.Owner);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
