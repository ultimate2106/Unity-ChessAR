using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessFieldManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentFigure;

    public GameObject CurrentFigure { get => _currentFigure; set => _currentFigure = value; }

    #region Trigger Event Methods

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<FigureManager>().LastEnteredField = gameObject;
    }

    #endregion

    #region Placing a figure

    public bool IsMoveAllowed()
    {
        if(CurrentFigure != null)
        {
            if (CurrentFigure.GetPhotonView().IsMine)
            {
                return false;
            }
        }
        return true;
    }

    public void PlaceFigure(GameObject newFigure)
    {
        if (CurrentFigure != null)
        {
            Destroy(CurrentFigure);
        }

        CurrentFigure = newFigure;
    }

    #endregion
}
