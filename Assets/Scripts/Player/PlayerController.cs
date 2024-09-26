using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("플레이어"), SerializeField]
    private Transform player;
    [Header("걷는 속력"), SerializeField]
    private float walkSpeed = 5f;
    [Header("달리는 속력"), SerializeField]
    private float runSpeed = 10f;
    [Header("공중 속력"), SerializeField]
    private float airSpeed = 2f;
    [Header("점프 힘"), SerializeField]
    private float jumpForce = 10f;
    [Header("추락 속도"), SerializeField]
    private float fallSpeedMultiply = 1.2f;

    [Header("슬로프 체커"), SerializeField]
    private SlopChecker slopChecker;
    [Header("스텝헤이트 체커"), SerializeField]
    private StepHeightChecker stepHeightChecker;
    [Header("그라운드 체커"), SerializeField]
    private GroundChecker groundChecker;



    private Rigidbody rigd;
    private Vector3 inputDir;
    public Vector3 InputDir => inputDir;
    private CameraManager cameraMgr;
    private InputManager inputMgr;

    public bool isSlope = false;

    #region 유니티 실행부
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

        isSlope = slopChecker.CheckSlope();



        if (groundChecker.IsGround)
        {
            if (inputMgr.InputDic[EUserAction.Jump])
                Jump();



            if (isSlope)
            {
                rigd.velocity = new Vector3(rigd.velocity.x, 0f, rigd.velocity.z);
                inputDir= slopChecker.AdjustDirectionToSlope(inputDir);
                rigd.useGravity = false;
            }
            else
            {
                stepHeightChecker.StepHeightMove(rigd, inputDir);
                if (stepHeightChecker.IsTask)
                {
                    rigd.useGravity = false;
                    rigd.velocity = new Vector3(rigd.velocity.x, 0f, rigd.velocity.z);
                }
                else
                {
                    rigd.useGravity = true;
                }
            }
            if (inputMgr.InputDic[EUserAction.Run])
                Move(inputDir, runSpeed);
            else
                Move(inputDir, walkSpeed);
        }
        else
            rigd.AddForce(Physics.gravity * (0.2f), ForceMode.Acceleration);





    }

    private void FixedUpdate()
    {
        //if (groundChecker.IsGround)
        //    Move(inputDir, walkSpeed);
        //else
        //    rigd.AddForce(Physics.gravity * (1.5f), ForceMode.Acceleration);
       


    }
    #endregion


    #region 초기화
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


    #region 움직임
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

        
        cameraForward.y = 0;
        cameraForward.Normalize();

        if (cameraForward != Vector3.zero)
        {
            player.transform.rotation = Quaternion.LookRotation(cameraForward);
        }


    }
    private void Jump()
    {
        rigd.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    #endregion
    private void PlayerSlopeState()
    {

        if (isSlope)
        {
            rigd.velocity = new Vector3(rigd.velocity.x, 0f, rigd.velocity.z);
            rigd.useGravity = false;

        }
        else
            rigd.useGravity = true;

    }
}
