using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessFieldManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _currentFigure;

    public GameObject CurrentFigure { get => _currentFigure; set => _currentFigure = value; }

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

    public bool PlaceFigure(GameObject newFigure)
    {
        if (IsMoveAllowed())
        {
            if (CurrentFigure != null)
            {
                Destroy(CurrentFigure);
            }
            CurrentFigure = newFigure;
            return true;
        }
        return false;
    }
}
