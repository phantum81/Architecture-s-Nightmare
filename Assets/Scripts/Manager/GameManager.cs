using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject _go = GameObject.Find("GameManager");
                if (_go == null)
                {
                    _instance = _go.AddComponent<GameManager>();

                }
                if (_instance == null)
                {
                    _instance = _go.GetComponent<GameManager>();
                }
            }
            return _instance;

        }
    }

    private InputManager _inputMgr;
    public InputManager InputMgr
    {
        get
        {
            if (_inputMgr == null)
            {
                GameObject _go = GameObject.Find("InputManager");
                if (_go == null)
                {
                    _go = new GameObject("InputManager"); 
                    _inputMgr = _go.AddComponent<InputManager>();

                }
                if (_inputMgr == null)
                {
                    _inputMgr = _go.GetComponent<InputManager>();
                }
            }
            return _inputMgr;

        }
    }

    private ItemManager _itemMgr;
    public ItemManager ItemMgr

    {
        get
        {
            if (_itemMgr == null)
            {
                GameObject _go = GameObject.Find("ItemManager");
                if (_go == null)
                {
                    _go = new GameObject("ItemManager");
                    _itemMgr = _go.AddComponent<ItemManager>();

                }
                if (_itemMgr == null)
                {
                    _itemMgr = _go.GetComponent<ItemManager>();
                }
            }
            return _itemMgr;

        }
    }

    private UnitManager _unitMgr;
    public UnitManager UnitMgr
    {
        get
        {
            if (_unitMgr == null)
            {
                GameObject _go = GameObject.Find("UnitManager");
                if (_go == null)
                {
                    _go = new GameObject("UnitManager");
                    _unitMgr = _go.AddComponent<UnitManager>();

                }
                if (_unitMgr == null)
                {
                    _unitMgr = _go.GetComponent<UnitManager>();
                }
            }
            return _unitMgr;

        }
    }
    private CameraManager _cameraMgr;
    public CameraManager CameraMgr
    {
        get
        {
            if (_cameraMgr == null)
            {
                GameObject _go = GameObject.Find("CameraManager");
                if (_go == null)
                {
                    _go = new GameObject("CameraManager");
                    _cameraMgr = _go.AddComponent<CameraManager>();

                }
                if (_cameraMgr == null)
                {
                    _cameraMgr = _go.GetComponent<CameraManager>();
                }
            }
            return _cameraMgr;

        }
    }
    private void Awake()
    {
        InputMgr.Init();
        CameraMgr.Init();

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
