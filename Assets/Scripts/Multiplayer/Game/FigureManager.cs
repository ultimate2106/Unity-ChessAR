using Photon.Pun;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition)
    {
        gameObject.transform.position = newPosition;
    }
}
