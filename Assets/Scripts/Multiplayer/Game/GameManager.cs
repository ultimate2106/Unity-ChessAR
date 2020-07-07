using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _whiteFigures;
    [SerializeField]
    private GameObject _blackFigures;

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
            ChessFiguresColor myColor = GlobalSettings.GetPlayerColor();
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
