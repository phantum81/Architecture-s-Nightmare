using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("�÷��̾�"), SerializeField]
    private Transform player;
    public Transform Player => player;
    [Header("�ȴ� �ӷ�"), SerializeField]
    private float walkSpeed = 5f;
    public float WalkSpeed => walkSpeed;
    [Header("�޸��� �ӷ�"), SerializeField]
    private float runSpeed = 10f;
    public float RunSpeed => runSpeed;
    [Header("���� �ӷ�"), SerializeField]
    private float airSpeed = 2f;
    public float AirSpeed => airSpeed;
    [Header("���� ��"), SerializeField]
    private float jumpForce = 10f;
    public float JumpForce => jumpForce;
    [Header("�߶� �ӵ�"), SerializeField]
    private float fallSpeedMultiply = 0.2f;

    [Header("������ üĿ"), SerializeField]
    private SlopChecker slopChecker;
    [Header("��������Ʈ üĿ"), SerializeField]
    private StepHeightChecker stepHeightChecker;
    [Header("�׶��� üĿ"), SerializeField]
    private GroundChecker groundChecker;



    private Rigidbody rigd;
    private Vector3 inputDir;
    public Vector3 InputDir => inputDir;
    private CameraManager cameraMgr;
    private InputManager inputMgr;

    private bool isSlope = false;
    public bool IsSlope => isSlope;
    private bool isGround = false;
    public bool IsGround => isGround;


    #region ����Ƽ �����
    private void Awake()
    {
        Init();
    }
    void Start()
    {
        
    }

    
    void Update()
    {
       
        isGround = groundChecker.IsGround;
        isSlope = slopChecker.CheckSlope();



        inputDir = InputLocalize(inputMgr.InputDir);

        if(isSlope)
            inputDir = slopChecker.AdjustDirectionToSlope(inputDir);

       


    }

    private void FixedUpdate()
    {

       


    }
    #endregion


    #region �ʱ�ȭ
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
    #endregion


    #region ������
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
       

    }

    public void JumpMove(Vector3 _inputDir, float _speed)
    {
        
        Vector3 horizontalForce = _inputDir.normalized * _speed;

        rigd.AddForce(horizontalForce, ForceMode.Force);
        Vector3 horizontalVelocity = new Vector3(rigd.velocity.x, 0, rigd.velocity.z);
        float verticalVelocity = rigd.velocity.y;

        Vector3 velocity = rigd.velocity;
        if (velocity.magnitude > runSpeed)
        {
            rigd.velocity = velocity.normalized * runSpeed;
        }
        rigd.velocity = horizontalVelocity + Vector3.up * verticalVelocity;
    }

    public void Rotate() 
    {

        Vector3 cameraForward = cameraMgr.CinemachineBrain.transform.forward;

        
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(cameraForward);
        }


    }
    public void Jump()
    {
        
        rigd.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    #endregion


    #region ������ٵ� ���� ����

    public void AirGravity()
    {
        rigd.AddForce(Physics.gravity * (fallSpeedMultiply), ForceMode.Acceleration);
    }

    public void SetGravity(bool _bol)
    {
        rigd.useGravity = _bol;
    }

    public void InitRigidbodyVelocity()
    {
        rigd.velocity = Vector3.zero;
    }

    public void InitYVelocity()
    {
        rigd.velocity = new Vector3(rigd.velocity.x, 0f, rigd.velocity.z);
    }
    #endregion


    public void SetInputDir(Vector3 _inputDir)
    {
        inputDir = _inputDir;
    }
    public void StepHeightMove()
    {
        stepHeightChecker.StepHeightMove(rigd, inputDir);
    }
}
