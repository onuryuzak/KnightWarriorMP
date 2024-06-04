using Application.Managers;
using Unity.Netcode;
using UnityEngine;

namespace Application.Repositories
{
    public class PlayerFactory
    {
        private GameObject playerPrefab;
        private Transform[] spawnPoints;

        public PlayerFactory(GameObject playerPrefab, Transform[] spawnPoints)
        {
            this.playerPrefab = playerPrefab;
            this.spawnPoints = spawnPoints;
        }
        public Transform[] GetSpawnPoints()
        {
            return spawnPoints;
        }

        public GameObject SpawnPlayer(ulong clientId, Transform spawnPoint)
        {
            if (spawnPoint == null)
            {
                Debug.LogError("Invalid spawn point!");
                return null;
            }

            GameObject playerObject = Object.Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
            playerObject.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
            
            return playerObject;
        }
    }
}