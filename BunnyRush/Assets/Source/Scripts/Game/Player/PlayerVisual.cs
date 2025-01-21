using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    private const string Jump = "Jump";
    private const string StartGame = "StartGame";
    private const string Dead = "Dead";

    [SerializeField] private Animator _animator;
    [SerializeField] private List<SkinnedMeshRenderer> _meshes;
    [SerializeField] private Material _invisibleMaterial;
    private List<Material> _startMatarial; 

    private Player _player;
    public void Initialize(Player player)
    {
        _player = player;
        _startMatarial = new List<Material>();

        for (int i = 0; i < _meshes.Count; i++)
        {
            Material material = _meshes[i].material;
            _startMatarial.Add(material);
        }
        _player.OnStateChanged += OnStateChanged;
    }

    #region Animations
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
        _animator.SetBool(Dead, false);
    }

    private void PlayJumpAnimation()
    {
        _animator.SetBool(Jump, true);
    }

    private void PlayDeadAnimation()
    {
        _animator.SetBool(StartGame, false);
        _animator.SetBool(Dead, true);
    }
    #endregion
    #region Mesh Visual

    public void StartInvisableTick(int invisableTime)
    {
        StartCoroutine(InvisableRoutine(invisableTime));
    }

    private void StopInvisableTick()
    {
        _player.CanTakeDamage = true;
    }

    private void SetNormalColor()
    {
        for (int i = 0; i < _meshes.Count; i++)
        {           
           _meshes[i].material = _startMatarial[i];
        }
    }

    private void SetInvisableColor()
    {
        foreach (SkinnedMeshRenderer mesh in _meshes)
        {
            mesh.material = _invisibleMaterial;
        }
    }

    private IEnumerator InvisableRoutine(int invisableTime)
    {
        int timer = 0;
        while(timer <= invisableTime)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            timer++;
            SetInvisableColor();
            yield return new WaitForSecondsRealtime(0.5f);
            SetNormalColor();
        }

        StopInvisableTick();
        yield break;
    }
    #endregion

    private void OnEnable()
    {
        if (_player == null)
            return;

        _player.OnStateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        if (_player == null)
            return;

        _player.OnStateChanged -= OnStateChanged;
    }
}
