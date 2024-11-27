using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string Jump = "Jump";
    private const string StartGame = "StartGame";
    private const string Dead = "Dead";

    [SerializeField] private Animator _animator;

    private Player _player;
    public void Initialize(Player player)
    {
        _player = player;
        _player.OnStateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        switch (_player.PlayerState)
        {
            case PlayerState.Idle:
                {
                    break;
                }
            case PlayerState.Run:
                {
                    PlayRunAnimation();
                    break;
                }
            case PlayerState.Jump:
                {
                    PlayJumpAnimation();
                    break;
                }
            case PlayerState.Dead:
                {
                    PlayDeadAnimation();
                    break;
                }

        }

    }

    private void PlayRunAnimation()
    {
        _animator.SetBool(StartGame,true);
        _animator.SetBool(Jump, false);
    }

    private void PlayJumpAnimation()
    {
        _animator.SetBool(Jump, true);
    }

    private void PlayDeadAnimation()
    {
        _animator.SetBool(Dead, true);
    }

    private void OnEnable()
    {
        if (_player == null)
            return;

        _player.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _player.OnStateChanged -= OnStateChanged;
    }


}
