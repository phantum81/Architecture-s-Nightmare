using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    private Fsm<EPlayerState, PlayerController> playerFsm;
    private Fsm<EPlayerGroundState, PlayerController> playerGroundFsm;

    [Header("플레이어컨트롤러"),SerializeField]

    private PlayerController playerCtr;
    private InputManager inputMgr;


    public EPlayerState ePlayerState;
    public EPlayerGroundState ePlayerGroundState;


    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        StateUpdate();

        


        if (playerCtr.IsGround)
        {
            if (inputMgr.InputDic[EUserAction.Jump])
                ChangeState(EPlayerState.Jump);

            if (playerCtr.IsSlope)
                ChangeState(EPlayerGroundState.Slope);
            else
                ChangeState(EPlayerGroundState.Ground);
        }
        else
            ChangeState(EPlayerGroundState.Air);




        if (ePlayerGroundState != EPlayerGroundState.Air)
        {
            if (inputMgr.InputDir != Vector3.zero)
            {
                
                if (inputMgr.InputDic[EUserAction.Run])
                    ChangeState(EPlayerState.Run);
                else
                    ChangeState(EPlayerState.Walk);

            }
            else
                ChangeState(EPlayerState.Idle);
        }
        else
            ChangeState(EPlayerState.None);



        playerFsm.Update(playerCtr);
        playerGroundFsm.Update(playerCtr);

    }




    private void Init()
    {

        playerFsm = Fsm<EPlayerState, PlayerController>.Instance;
        playerGroundFsm = Fsm<EPlayerGroundState, PlayerController>.Instance;
        playerFsm.ChangeState(EPlayerState.None, playerCtr);
        
        playerGroundFsm.ChangeState(EPlayerGroundState.Ground, playerCtr);
        inputMgr = GameManager.Instance.InputMgr;
    }

    private void StateUpdate()
    {
        ePlayerState = playerFsm.CurState;
        ePlayerGroundState = playerGroundFsm.CurState;
    }

    private void ChangeState<T>(T _state) where T : System.Enum
    {
        if(_state is EPlayerState playerState)
        {
            playerFsm.ChangeState(playerState, playerCtr);
        }
        else if(_state is EPlayerGroundState playerGroundState)
        {
            playerGroundFsm.ChangeState(playerGroundState, playerCtr);
        }
    }
}
