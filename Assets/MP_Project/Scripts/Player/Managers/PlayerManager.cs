using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Domain.Entities;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using UnityEngine;

namespace Application.Managers
{
    public class PlayerManager
    {
        private readonly PlayerFactory _playerFactory;

        public PlayerManager(PlayerFactory playerFactory)
        {
            _playerFactory = playerFactory;
        }


        public void HandleClientConnected(ulong clientId, int playerIndex)
        {
            Transform spawnPoint = GetSpawnPoint(playerIndex);
            var player = _playerFactory.SpawnPlayer(clientId, spawnPoint);
        }

        private Transform GetSpawnPoint(int index)
        {
            if (index >= 0 && index < _playerFactory.GetSpawnPoints().Length)
            {
                return _playerFactory.GetSpawnPoints()[index];
            }

            Debug.LogError("Invalid spawn point index!");
            return null;
        }
    }
}