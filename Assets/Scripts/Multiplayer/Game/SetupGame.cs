using Photon.Pun;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    [SerializeField]
    private GameObject _chessBoard;
    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }

    public void CreatePlayer()
    {
        Debug.Log("Creating Player..");
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
        player.transform.SetParent(_chessBoard.transform);
        player.transform.localPosition = new Vector3(0, 0, 0.075f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
