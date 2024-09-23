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
    [Header("���� �ӷ�"), SerializeField]
    private float airSpeed = 2f;
    [Header("���� ��"), SerializeField]
    private float jumpForce = 10f;
    [Header("�߶� �ӵ�"), SerializeField]
    private float fallSpeedMultiply = 1.2f;

    [Header("������ üĿ"), SerializeField]
    private SlopChecker slopChecker;
    [Header("��������Ʈ üĿ"), SerializeField]
    private StepHeightChecker stepHeightChecker;
    [Header("�׶��� üĿ"), SerializeField]
    private GroundChecker groundChecker;



    private Rigidbody rigd;
    private Vector3 inputDir;
    private CameraManager cameraMgr;
    private InputManager inputMgr;

    #region ����Ƽ �����
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

        PlayerSlopeState();

        stepHeightChecker.StepHeightMove(rigd, inputDir);

        if (inputMgr.InputDic[EUserAction.Jump])
            Jump();

    }

    private void FixedUpdate()
    {
        if (groundChecker.IsGround)
            Move(inputDir, walkSpeed);
        else
            rigd.AddForce(Physics.gravity * (1.5f), ForceMode.Acceleration);


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
        Rotate();

    }

    public void JumpMove(Vector3 _inputDir, float _speed)
    {

        Vector3 horizontalForce = _inputDir.normalized * _speed;

        rigd.AddForce(horizontalForce, ForceMode.Force);

        
        Rotate();
    }

    public void Rotate() 
    {

        Vector3 cameraForward = cameraMgr.CinemachineBrain.transform.forward;

        // ī�޶��� �� ���� ������ Y�� ������ 0���� �����Ͽ� ��������� ����
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(cameraForward);
        }


    }
    private void Jump()
    {
        if (groundChecker.IsGround)
        {
            rigd.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    #endregion
    private void PlayerSlopeState()
    {
        float groundAngle = slopChecker.CalculateGroundAngle(inputDir);


        if (groundAngle > 0 && groundChecker.IsGround)
        {
            rigd.useGravity = false;
            rigd.velocity = new Vector3(rigd.velocity.x, 0f, rigd.velocity.z);
        }
        else
            rigd.useGravity = true;

    }
}
