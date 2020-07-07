using Photon.Pun;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    public void OnFigureMoved(Vector3 newPosition)
    {
        PhotonView view = gameObject.GetComponent<PhotonView>();
    }

    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
    }
}
