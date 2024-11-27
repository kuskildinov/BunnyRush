using UnityEngine;

public class RoadTile : MonoBehaviour
{
    private float _speed;
    private RoadTileSpawner _spawner;

    private bool _isActive;
    public bool IsActive { get => _isActive; set => _isActive = value; }


    public void Initialize(RoadTileSpawner spawner,float speed)
    {
        _spawner = spawner;
        _speed = speed;

        _spawner.OnGameStarted += ActivateTile;
    }

    private void OnDisable()
    {
        _spawner.OnGameStarted -= ActivateTile;
    }

    private void FixedUpdate()
    {
        if(_isActive)
        {
            Move();
        }        
    }

    public void ActivateTile() => _isActive = true;
    public void DeactivateTile() => _isActive = false;

    private void Move()
    {
        transform.Translate(Vector3.left * _speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<RoadDestroyTrigger>(out RoadDestroyTrigger trigger))
        {
            _spawner.DestroyTile(this);
        }
    }
}
