using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("�÷��̾�"), SerializeField]
    private Transform player;
    [Header("�ȴ� �ӷ�"), SerializeField]
    private float walkSpeed = 10f;
    private Rigidbody rigd;
    private Vector3 inputDir;
    private CameraManager cameraMgr;
    private InputManager inputMgr;
    
    private void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputDir = InputLocalize(inputMgr.InputDir);
        Move(inputDir, walkSpeed);
    }
    private void Init()
    {
        cameraMgr = GameManager.Instance.CameraMgr;
        inputMgr = GameManager.Instance.InputMgr;
        rigd = player.GetComponent<Rigidbody>();
    }

    private Vector3 InputLocalize(Vector3 _inputdir)
    {
        Vector3 right = _inputdir.x * player.right;
        Vector3 foward = _inputdir.z * player.forward;
        Vector3 inputDir = (right + foward).normalized;
        return inputDir;
    }

    public void Move(Vector3 _inputDir, float _speed)
    {
        float fallSpeed = rigd.velocity.y;
        if (_inputDir != Vector3.zero)
        {
            _inputDir *= _speed;
        }
        else
            _inputDir = Vector3.zero;

        _inputDir.y = fallSpeed;
        rigd.velocity = _inputDir;
        Rotate();

    }

    public void Rotate() 
    {
        Vector3 cameraForward = cameraMgr.CurCamera.transform.forward;

        // ī�޶��� �� ���� ������ Y�� ������ 0���� �����Ͽ� ��������� ����
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(cameraForward);
        }


    }
}
