using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField,Min(0f)] private int _deadInvisableTime = 6;
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private PlayerVisual _playerVisual;

    private PlayerState _playerState;
    private bool _isAlive;
    private bool _canTakeDamage;
    public PlayerState PlayerState => _playerState;
    public bool IsAlive => _isAlive;
    public bool CanTakeDamage { get => _canTakeDamage; set => _canTakeDamage = value; }

    public event Action OnStateChanged;
   
    public void Initialize(IInput input)
    {
        _playerState = PlayerState.Idle;

        _playerJumper.Initialize(this, input);
        _playerVisual.Initialize(this);

        _isAlive = true;
        _canTakeDamage = true;
    }

    private void Update()
    {
        if(_canTakeDamage == false)
        {
            StartCoroutine(CheckInvisableFrameRoutine());
        }
    }

    public void StartGame()
    {
        ChangeState(PlayerState.Run);
        _isAlive = true;
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

    private void CoinTaked()
    {       
        GameRoot.Instance.OnCoinTaked();
    }

    private void Dead()
    {
        SoundsRoot.Instance.PlayTakeHitSound();

        ChangeState(PlayerState.Dead);
        _isAlive = false;
    }

    public void ContinueGame()
    {
        ChangeState(PlayerState.Run);
        _isAlive = true;
        _canTakeDamage = false;
    }

    public void ResumeGame()
    {
        ChangeState(PlayerState.Run);
        _isAlive = true;
        _canTakeDamage = false;
        _playerVisual.StartInvisableTick(_deadInvisableTime);
    }

    public void SetNewJumpDuration(float value)
    {
        _playerJumper.Duration = value;
    }

    private IEnumerator CheckInvisableFrameRoutine()
    {
        yield return new WaitForSecondsRealtime(7f);
        _canTakeDamage = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isAlive == false)
            return;

        if(other.TryGetComponent<DeadZone>(out DeadZone zone))
        {
            if(_canTakeDamage)
            {
                _isAlive = false;
                Dead();
                GameRoot.Instance.PlayerLose(zone);
            }           
        }
        if (other.TryGetComponent<Coin>(out Coin coin))
        {           
            CoinTaked();
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

