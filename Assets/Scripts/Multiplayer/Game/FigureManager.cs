using Lean.Touch;
using Photon.Pun;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private PhotonView view;
    [SerializeField]
    private LeanSelectable _selectable;

    public void OnFigureMoved(LeanFinger leanFinger)
    {
        Debug.Log("OnFigureMoved called..");
        Vector3 position = gameObject.transform.position;
        view.RPC("MoveFigureToNewPosition", RpcTarget.Others, position);
    }

    // Test if private is possible
    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition)
    {
        Debug.Log("New position: " + newPosition.x + ", " + newPosition.y);
        gameObject.transform.position = newPosition;
    }

    public void OnFigureSelect()
    {
        if (!_gameManager.IsMyTurn() || !view.IsMine)
        {
            _selectable.Deselect();
        }
    }
}
