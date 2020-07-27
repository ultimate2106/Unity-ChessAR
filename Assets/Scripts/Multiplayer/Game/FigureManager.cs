using Lean.Touch;
using Photon.Pun;
using UnityEditor.UIElements;
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
    private GameObject _lastEnteredField;

    public GameObject LastEnteredField { get => _lastEnteredField; set => _lastEnteredField = value; }

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
                ChessFieldManager leField = _lastEnteredField.GetComponent<ChessFieldManager>();
                if (leField.IsMoveAllowed())
                {
                    _lastPosition = _lastEnteredField.transform.localPosition;
                    _view.RPC("MoveFigureToNewPosition", RpcTarget.All, _lastPosition, leField);
                } else
                {
                    transform.localPosition = _lastPosition;
                }
            }
        }
    }

    // Test if private is possible
    [PunRPC]
    public void MoveFigureToNewPosition(Vector3 newPosition, ChessFieldManager leField)
    {
        leField.PlaceFigure(gameObject);
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
