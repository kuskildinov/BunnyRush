using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private Transform _root;
    [SerializeField] private AnimationCurve _yAnimationCurve;
    [Header("Jump Settings")]
    [SerializeField] private float _duration;
    [SerializeField] private float _heigth;

    private Player _player;
    private float _expiredTime;
    private IInput _input;
    private bool _isJumping;

    public void Initialize(Player player, IInput input)
    {
        _player = player;
        _input = input;
    }

    private void Update()
    {
        TryDesktopJump();
    }

    private void TryDesktopJump()
    {       
        if (_input.Jump() && _isJumping == false)
        {
            _isJumping = true;
            _player.ChangeState(PlayerState.Jump);
        }       
        if (_isJumping)
        {
            JumpRoutine();
        }
    }

    public void TryScreenJump()
    {
        _isJumping = true;
        _player.ChangeState(PlayerState.Jump);
        JumpRoutine();
    }

    private void JumpRoutine()
    {
        _expiredTime += Time.deltaTime;

        if (_expiredTime > _duration)
        {
            _expiredTime = 0;
            _isJumping = false;
            _player.ChangeState(PlayerState.Run);
        }

        float progress = _expiredTime / _duration;
        _root.transform.position = new Vector3(0, _yAnimationCurve.Evaluate(progress) * _heigth, 0);
    }
}
