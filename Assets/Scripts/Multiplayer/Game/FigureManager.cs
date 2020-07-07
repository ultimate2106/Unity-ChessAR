using Lean.Touch;
using Photon.Pun;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    public void OnFigureMoved(LeanFinger leanFinger)
    {
        Debug.Log("OnFigureMoved called..");
        PhotonView view = gameObject.GetComponent<PhotonView>();
        Vector3 position = gameObject.transform.position;
        view.RPC("MoveFigureToNewPosition", RpcTarget.Others, position);
    }

    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition)
    {
        Debug.Log("New position: " + newPosition.x + ", " + newPosition.y);
        gameObject.transform.position = newPosition;
    }
}
