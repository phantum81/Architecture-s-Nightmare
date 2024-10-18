using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Vector3 _inputDir;
    public Vector3 InputDir => _inputDir;



    private Dictionary<EUserAction, bool> _inputDic = new Dictionary<EUserAction, bool>();
    public Dictionary<EUserAction, bool> InputDic => _inputDic;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckInputKeys();
        GetInputDir();
    }


    public void Init()
    {
        if (_inputDic.Count > 0)
            return;
        for (int i = 0; i < _inputDic.Count; i++)
        {
            _inputDic.Add((EUserAction)i, false);
        }

    }

    #region 입력검사

    private void GetInputDir()
    {

        float x = 0f;
        float z = 0f;

        x = (_inputDic[EUserAction.MoveRight] ? 1f : 0f) - (_inputDic[EUserAction.MoveLeft] ? 1f : 0f);
        z = (_inputDic[EUserAction.MoveForward] ? 1f : 0f) - (_inputDic[EUserAction.MoveBackward] ? 1f : 0f);

        _inputDir = new Vector3(x, 0, z);

    }
    public void CheckInputKeys()
    {
        _inputDic[EUserAction.MoveForward] = Input.GetKey(KeyCode.W);
        _inputDic[EUserAction.MoveBackward] = Input.GetKey(KeyCode.S);
        _inputDic[EUserAction.MoveRight] = Input.GetKey(KeyCode.D);
        _inputDic[EUserAction.MoveLeft] = Input.GetKey(KeyCode.A);
        _inputDic[EUserAction.Jump] = Input.GetKeyDown(KeyCode.Space);
        _inputDic[EUserAction.Run] = Input.GetKey(KeyCode.LeftShift);
        _inputDic[EUserAction.Interaction] = Input.GetKeyDown(KeyCode.F);

    }
    #endregion
}
