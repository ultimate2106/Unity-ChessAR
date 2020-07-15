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
    private Vector3 _currentPosition;
    private Vector3 _lastPosition;


    // Start is called before the first frame update
    void Start()
    {
        _view = gameObject.GetComponent<PhotonView>();
        _zAxisStart = transform.localPosition.z;
        _currentPosition = transform.localPosition;
        _lastPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Try to move this to Lean Drag Translate!
        _currentPosition.x = transform.localPosition.x;
        _currentPosition.y = transform.localPosition.y;

        // TODO: Execute only if the current position has changed
        transform.localPosition = _currentPosition;
    }

    public void OnFigureMoved(LeanFinger leanFinger)
    {
        if (IsActionAllowed())
        {
            Vector3 position = gameObject.transform.localPosition;
            if((position.x > _gameManager.MaxXY || position.x < _gameManager.MinXY) || 
                position.y > _gameManager.MaxXY || position.y < _gameManager.MinXY) {
                transform.localPosition = _lastPosition;
            } else
            {
                _lastPosition = transform.localPosition;
                _view.RPC("MoveFigureToNewPosition", RpcTarget.All, position);
            }
        }
    }

    // Test if private is possible
    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition)
    {
        Debug.Log("New position: " + newPosition.x + ", " + newPosition.y);
        gameObject.transform.localPosition = newPosition;
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
