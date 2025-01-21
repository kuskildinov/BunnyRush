using System.Collections.Generic;
using UnityEngine;

public class GameLevelRoot : MonoBehaviour
{
    [Header("Level 1")]
    [SerializeField] private RoadTile _emptyTile_1;
    [SerializeField] private List<RoadTile> _level_1_1_prefabs;
    [SerializeField] private List<RoadTile> _level_1_2_prefabs;
    [Header("Level 2")]
    [SerializeField] private RoadTile _emptyTile_2;
    [SerializeField] private List<RoadTile> _level_2_1_prefabs;
    [SerializeField] private List<RoadTile> _level_2_2_prefabs;
    [Header("Level 3")]
    [SerializeField] private RoadTile _emptyTile_3;
    [SerializeField] private List<RoadTile> _level_3_1_prefabs;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Color _level_one_color;
    [SerializeField] private Color _level_two_color;
    [SerializeField] private Color _level_three_color;

    public void Initialize()
    {
        _particleSystem.startColor = _level_one_color;
    }

    private void Update()
    {
        float score = GameRoot.Instance.GameScore.CurrentLevelScore;
        if(score > 0 && score < 1000)
        {
            _particleSystem.startColor = _level_one_color;
        }
        else if(score > 1000 && score < 2000)
        {
            _particleSystem.startColor = _level_two_color;
        }
        else if (score > 2000 )
        {
            _particleSystem.startColor = _level_three_color;
        }
    }

    public RoadTile GetEmptyTile(float level)
    {
        if(level == 1f)
            return _emptyTile_1;
        else if (level == 2f)
            return _emptyTile_2;
        else if (level == 3f)
            return _emptyTile_3;
        return null;
    }

    public RoadTile GetRandomTile(float level)
    {
        if (level == 1.1f)
        {
            var randonIndex = UnityEngine.Random.Range(0, _level_1_1_prefabs.Count);
            return _level_1_1_prefabs[randonIndex];
        }
        else if (level == 1.2f)
        {
            var randonIndex = UnityEngine.Random.Range(0, _level_1_2_prefabs.Count);
            return _level_1_2_prefabs[randonIndex];
        }
        else if (level == 2.1f)
        {
            var randonIndex = UnityEngine.Random.Range(0, _level_2_1_prefabs.Count);
            return _level_2_1_prefabs[randonIndex];
        }
        else if (level == 2.2f)
        {
            var randonIndex = UnityEngine.Random.Range(0, _level_2_2_prefabs.Count);
            return _level_2_2_prefabs[randonIndex];
        }
        else if (level == 3.1f)
        {
            var randonIndex = UnityEngine.Random.Range(0, _level_3_1_prefabs.Count);
            return _level_3_1_prefabs[randonIndex];
        }

        return null;
    }
}
