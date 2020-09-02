using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesignChanger : MonoBehaviour
{
    [SerializeField]
    private Texture _design;

    private GameObject _chessBoard;

    private Renderer _renderer;

    private Vector3 _localPos = new Vector3(0, 0.45f, 0);
    private Quaternion _localRotation = Quaternion.Euler(-89.98f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        _chessBoard = GameObject.Find("Board");

        if (_chessBoard == null)
        {
            Debug.LogError("Chess Board is null!");
        }

        _renderer = _chessBoard.GetComponent<Renderer>();

        if (_renderer == null)
        {
            Debug.LogError("Renderer is null!");
        }
    }

    public void ChangeDesign()
    {
        if (!_chessBoard.transform.IsChildOf(gameObject.transform))
        {
            _chessBoard.transform.SetParent(transform);
            _chessBoard.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z); // kP ob notwendig
            _chessBoard.transform.localPosition = _localPos;
            _chessBoard.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
            _chessBoard.transform.localRotation = _localRotation;

            _renderer.material.mainTexture = _design;
        }
    }
}
