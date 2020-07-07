using Lean.Touch;
using Photon.Pun;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    public void OnFigureMoved(LeanFinger leanFinger)
    {
        Debug.Log("OnFigureMoved called..");
        PhotonView view = gameObject.GetComponent<PhotonView>();
        Vector3 position = gameObject.transform.localPosition;
        view.RPC("MoveFigureToNewPosition", RpcTarget.Others, position);
    }

    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition)
    {
        Debug.Log("MoveFigureToNewPosition called..");
        gameObject.transform.position = newPosition;
    }
}
