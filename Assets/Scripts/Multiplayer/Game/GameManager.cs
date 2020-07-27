using DG.Tweening;
using Photon.Pun;
using System.Collections;
using System.ComponentModel;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Figures and FiguresHolder (Player)
    [SerializeField]
    private GameObject _whitePlayer;
    [SerializeField]
    private GameObject _blackPlayer;

    [SerializeField]
    private GameObject[] _whiteFigures;
    [SerializeField]
    private GameObject[] _blackFigures;

    #endregion

    #region Turn System

    #region Message

    [SerializeField]
    private GameObject _messageHolder;
    [SerializeField]
    private Text _messageText;

    #endregion
    private ChessFiguresColor _whoseTurn = ChessFiguresColor.White;

    #endregion

    #region Boardbounds

    [SerializeField]
    private float _minXY = -0.028f;
    [SerializeField]
    private float _maxXY = 0.028f;

    #endregion

    #region Chessfield organization

    [SerializeField]
    private float _fieldDistance = 0.00745f;
    [SerializeField]
    private Vector3 _firstField = new Vector3(-0.0263f, 0.0263f, 0.0456f);
    [SerializeField]
    private GameObject _chessFieldPrefab;

    #endregion

    [SerializeField]
    private GameObject _board;

    private GameObject _player;

    #region Properties

    public float MinXY { get => _minXY; }
    public float MaxXY { get => _maxXY; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Init game..
        CreatePlayer();
        InitChessfields();
        ManageOwnership();
        ShowActivePlayerMsg();        
    }

    private void CreatePlayer()
    {
        _player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }

    #region Ownership Handling Methods

    private void ManageOwnership()
    {
        if(GlobalSettings.GetPlayerId() != 0)
        {
            ChessFiguresColor myColor = GlobalSettings.GetPlayerColor();
            
            if (myColor == ChessFiguresColor.White)
            {
                PhotonView[] figureViews = _whitePlayer.GetComponentsInChildren<PhotonView>();
                GetOwnership(figureViews);
            } else
            {
                PhotonView[] figureViews = _blackPlayer.GetComponentsInChildren<PhotonView>();
                GetOwnership(figureViews);
            }
        }
    }

    //TODO: Eventuell lieber ein RequestOwnership
    private void GetOwnership(PhotonView[] figureViews)
    {
        PhotonView playerView = _player.GetComponent<PhotonView>();
        foreach (PhotonView view in figureViews)
        {
            view.TransferOwnership(playerView.Owner);
        }
    }
    
    #endregion

    #region Turnsystem Methods

    public bool IsMyTurn()
    {
        Debug.LogWarning("PlayerColor: " + GlobalSettings.GetPlayerColor() + ", " + _whoseTurn);
        return GlobalSettings.GetPlayerColor() == _whoseTurn;
    }

    public ChessFiguresColor GetActivePlayer()
    {
        return _whoseTurn;
    }

    public void EndTurn()
    {
        switch (_whoseTurn)
        {
            case ChessFiguresColor.White:
                _whoseTurn = ChessFiguresColor.Black;
                break;
            case ChessFiguresColor.Black:
                _whoseTurn = ChessFiguresColor.White;
                break;
        }

        ShowActivePlayerMsg();
    }

    public void ShowActivePlayerMsg()
    {
        if (_whoseTurn == GlobalSettings.GetPlayerColor())
        {
            ShowMessage("Your turn", 2f);
        }
        else
        {
            ShowMessage("Enemy turn", 2f);
        }
    }

    public void ShowMessage(string message, float duration)
    {
        StartCoroutine(ShowMassageCoroutine(message, duration));
    }

    private IEnumerator ShowMassageCoroutine(string message, float duration)
    {
        _messageText.text = message;

        _messageHolder.transform.DOScale(1f, 1.5f);

        yield return new WaitForSeconds(duration);

        _messageHolder.transform.DOScale(0f, 1.5f);

        yield return new WaitForSeconds(1.5f);
    }

    #endregion

    #region Init Methods

    //Creating and placing the chessfields(with Colliders) at the desired positions
    private void InitChessfields()
    {
        float x = _firstField.x;
        float y = _firstField.y;
        GameObject figure = new GameObject();
        for (int i = 0; i < 8; i++)
        {
            y = _firstField.y - _fieldDistance * i;
            for(int j = 0; j < 8; j++)
            {
                x = _firstField.x + _fieldDistance * j;
                GameObject go = Instantiate(_chessFieldPrefab, _board.transform);
                go.name = $"{i}{j}";
                go.transform.localPosition = new Vector3(x, y, _firstField.z);
                InitFigure(go);
            }
        }
    }
    private void InitFigure(GameObject field)
    {
        //White figures
        switch (field.name)
        {
            case "00":
                //Rook  white  8
                ConnectFigureToField(_whiteFigures[8], field);
                break;
            case "07":
                //Rook  white  9
                ConnectFigureToField(_whiteFigures[9], field);
                break;
            case "01":
                //Knight white 10
                ConnectFigureToField(_whiteFigures[10], field);
                break;
            case "06":
                //Knight white 11
                ConnectFigureToField(_whiteFigures[11], field);
                break;
            case "02":
                //Bishop white 12
                ConnectFigureToField(_whiteFigures[12], field);
                break;
            case "05":
                //Bishop white 13
                ConnectFigureToField(_whiteFigures[13], field);
                break;
            case "03":
                //king white 14
                ConnectFigureToField(_whiteFigures[14], field);
                break;
            case "04":
                //queen white 15
                ConnectFigureToField(_whiteFigures[15], field);
                break;
            case "10":
                ConnectFigureToField(_whiteFigures[0], field);
                break;
            case "11":
                ConnectFigureToField(_whiteFigures[1], field);
                break;
            case "12":
                ConnectFigureToField(_whiteFigures[2], field);
                break;
            case "13":
                ConnectFigureToField(_whiteFigures[3], field);
                break;
            case "14":
                ConnectFigureToField(_whiteFigures[4], field);
                break;
            case "15":
                ConnectFigureToField(_whiteFigures[5], field);
                break;
            case "16":
                ConnectFigureToField(_whiteFigures[6], field);
                break;
            case "17":
                // pawns white 0
                ConnectFigureToField(_whiteFigures[7], field);
                break;
        }

        //Black figures
        switch (field.name)
        {
            case "70":
                ConnectFigureToField(_blackFigures[8], field);
                break;
            case "77":
                //Rook  black  
                ConnectFigureToField(_blackFigures[9], field);
                break;
            case "71":
                ConnectFigureToField(_blackFigures[10], field);
                break;
            case "76":
                //Knight black
                ConnectFigureToField(_blackFigures[11], field);
                break;
            case "72":
                ConnectFigureToField(_blackFigures[12], field);
                break;
            case "75":
                //Bishop black
                ConnectFigureToField(_blackFigures[13], field);
                break;
            case "73":
                //queen black
                ConnectFigureToField(_blackFigures[15], field);
                break;
            case "74":
                //king black
                ConnectFigureToField(_blackFigures[14], field);
                break;
            case "60":
                ConnectFigureToField(_blackFigures[0], field);
                break;
            case "61":
                ConnectFigureToField(_blackFigures[1], field);
                break;
            case "62":
                ConnectFigureToField(_blackFigures[2], field);
                break;
            case "63":
                ConnectFigureToField(_blackFigures[3], field);
                break;
            case "64":
                ConnectFigureToField(_blackFigures[4], field);
                break;
            case "65":
                ConnectFigureToField(_blackFigures[5], field);
                break;
            case "66":
                ConnectFigureToField(_blackFigures[6], field);
                break;
            case "67":
                // pawns black
                ConnectFigureToField(_blackFigures[7], field);
                break;
        }
    }

    private void ConnectFigureToField(GameObject figure, GameObject field)
    {
        figure.transform.localPosition = field.transform.localPosition;
        figure.GetComponent<FigureManager>().LastEnteredField = field;
        field.GetComponent<ChessFieldManager>().CurrentFigure = figure;
    }

    #endregion
}
