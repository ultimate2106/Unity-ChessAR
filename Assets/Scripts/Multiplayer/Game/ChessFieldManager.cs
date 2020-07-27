using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessFieldManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentFigure;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Position after Trigger: " + gameObject.transform.position + " LocalPos: " + gameObject.transform.localPosition);
        other.gameObject.GetComponent<FigureManager>().LastEnteredField = gameObject;
    }

    private bool IsMoveAllowed()
    {
        if(_currentFigure != null)
        {
            if (_currentFigure.GetPhotonView().IsMine)
            {
                return false;
            }
        }
        return true;
    }

    public bool PlaceFigure(GameObject newFigure)
    {
        if (IsMoveAllowed())
        {
            if (_currentFigure != null)
            {
                Destroy(_currentFigure);
            }
            _currentFigure = newFigure;
            return true;
        }
        return false;
    }
}
