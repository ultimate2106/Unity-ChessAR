using Lean.Touch;
using Photon.Pun;
using UnityEngine;

public class FigureManager : MonoBehaviour
{
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private LeanSelectable _selectable;

    private PhotonView _view;

    private float _zAxisStart;

    // Start is called before the first frame update
    void Start()
    {
        _view = gameObject.GetComponent<PhotonView>();
        _zAxisStart = transform.localPosition.z;
    }

    // Update is called once per frame
    void Update()
    {
        float currentX = transform.localPosition.x;
        float currentY = transform.localPosition.y;
        transform.localPosition = new Vector3(currentX, currentY, _zAxisStart);
    }

    public void OnFigureMoved(LeanFinger leanFinger)
    {
        if (IsActionAllowed())
        {
            Debug.LogWarning(gameObject.name);
            Debug.Log("OnFigureMoved called..");
            Vector3 position = gameObject.transform.position;
            _view.RPC("MoveFigureToNewPosition", RpcTarget.All, position);
        }
    }

    // Test if private is possible
    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition)
    {
        Debug.Log("New position: " + newPosition.x + ", " + newPosition.y);
        gameObject.transform.position = newPosition;
        _gameManager.EndTurn();
    }

    public void OnFigureSelect(LeanFinger finger)
    {
        if (!IsActionAllowed())
        {
            _selectable.Deselect();
        }
    }

    public bool IsActionAllowed()
    {
        return (_gameManager.IsMyTurn() && _view.IsMine);
    }
}
