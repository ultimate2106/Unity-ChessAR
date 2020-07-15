using DG.Tweening;
using Photon.Pun;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _whiteFigures;
    [SerializeField]
    private GameObject _blackFigures;

    [SerializeField]
    private GameObject _messageHolder;
    [SerializeField]
    private Text _messageText;

    private GameObject _player;

    private ChessFiguresColor _whoseTurn = ChessFiguresColor.White;

    [SerializeField]
    private float _minXY = -0.028f;
    [SerializeField]
    private float _maxXY = 0.028f;

    public float MinXY { get => _minXY; }
    public float MaxXY { get => _maxXY; }

    // Start is called before the first frame update
    void Start()
    {
        // Init game..
        CreatePlayer();
        ManageOwnership();
        ShowActivePlayerMsg();
    }

    private void CreatePlayer()
    {
        Debug.Log("Creating Player..");
        _player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.zero, Quaternion.identity);
    }

    private void ManageOwnership()
    {
        if(GlobalSettings.GetPlayerId() != 0)
        {
            ChessFiguresColor myColor = GlobalSettings.GetPlayerColor();

            if (myColor == ChessFiguresColor.White)
            {
                PhotonView[] figureViews = _whiteFigures.GetComponentsInChildren<PhotonView>();
                GetOwnership(figureViews);
            } else
            {
                PhotonView[] figureViews = _blackFigures.GetComponentsInChildren<PhotonView>();
                GetOwnership(figureViews);
            }
        }
    }

    //TODO: Eventuell lieber ein RequestOwnership
    private void GetOwnership(PhotonView[] figureViews)
    {
        Debug.Log("Get Ownership of " + GlobalSettings.GetPlayerColor() + "..");
        PhotonView playerView = _player.GetComponent<PhotonView>();
        foreach (PhotonView view in figureViews)
        {
            view.TransferOwnership(playerView.Owner);
        }
    }

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
}
