using Photon.Pun;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    public void OnFigureMoved(Vector3 newPosition)
    {
        Debug.Log("OnFigureMoved called..");
        PhotonView view = gameObject.GetComponent<PhotonView>();
        view.RPC("MoveFigureToNewPosition", RpcTarget.Others, newPosition);
    }

    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition)
    {
        Debug.Log("MoveFigureToNewPosition called..");
        gameObject.transform.position = newPosition;
    }
}
