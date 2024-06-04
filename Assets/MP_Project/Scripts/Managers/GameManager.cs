using UnityEngine;
using Unity.Netcode;
using Application.Repositories;
using Domain.Entities;

namespace Application.Managers
{
    public class GameManager : NetworkBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform[] spawnPoints;

        private PlayerFactory _playerFactory;
        private PlayerManager _playerManager;

        private NetworkVariable<int> _networkPosIndex =
            new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

        private void Awake()
        {
            _playerFactory = new PlayerFactory(playerPrefab, spawnPoints);
            _playerManager = new PlayerManager(_playerFactory);
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
            {
                NetworkManager.OnClientConnectedCallback += SpawnPlayer;
            }

            
        }


        private void SpawnPlayer(ulong playerId)
        {
            _playerManager.HandleClientConnected(playerId, _networkPosIndex.Value);
            _networkPosIndex.Value += 1;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            MatchmakingService.LeaveLobby();
            _networkPosIndex.Value = 0;
            if (NetworkManager.Singleton != null)NetworkManager.Singleton.OnClientConnectedCallback -= SpawnPlayer;

            if (NetworkManager.Singleton != null) NetworkManager.Singleton.Shutdown();
        }
    }
}