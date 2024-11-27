using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private List<Player> _playerPrefabs;
    [SerializeField] private Transform _playerSpawnPoint;

    public Player SpawnPlayer(int index)
    {
        if (index < 0 || index >= _playerPrefabs.Count)
            return null;

        Player player = Instantiate(_playerPrefabs[index],_playerSpawnPoint.position,Quaternion.identity);

        return player;
    }
}
