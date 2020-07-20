using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessFieldManager : MonoBehaviour
{
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

}
