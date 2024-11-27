using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private PlayerVisual _playerVisual;

    private PlayerState _playerState;
    private bool _isAlive;
    public PlayerState PlayerState => _playerState;

    public event Action OnStateChanged;
   
    public void Initialize(IInput input)
    {
        _playerState = PlayerState.Idle;

        _playerJumper.Initialize(this, input);
        _playerVisual.Initialize(this);

        _isAlive = true;
    }

    public void StartGame()
    {
        ChangeState(PlayerState.Run);
    }

    public void ChangeState(PlayerState newState)
    {
        _playerState = newState;
        OnStateChanged?.Invoke();
    }

    public void ForceJump()
    {
        _playerJumper.TryScreenJump();
    }

    private void CoindTaked()
    {
        GameRoot.Instance.OnCoinTaked();
    }

    private void Dead()
    {
        ChangeState(PlayerState.Dead);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAlive == false)
            return;

        if(other.TryGetComponent<DeadZone>(out DeadZone zone))
        {
            _isAlive = false;
            Dead();
            GameRoot.Instance.PlayerLose();
        }
        if (other.TryGetComponent<Coin>(out Coin coin))
        {
            CoindTaked();
            Destroy(coin.gameObject);          
        }
    }
}


public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Dead
}

