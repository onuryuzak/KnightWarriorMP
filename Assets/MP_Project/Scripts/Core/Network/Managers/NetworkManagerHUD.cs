using UnityEngine;
using Unity.Netcode;

namespace Presentation.UI
{
    public class NetworkManagerHUD : MonoBehaviour
    {
        private void OnGUI()
        {
            if (NetworkManager.Singleton == null)
            {
                GUILayout.Label("NetworkManager.Singleton is null. Please make sure NetworkManager is in the scene.");
                return;
            }

            GUILayout.BeginArea(new Rect(10, 10, 300, 300));

            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                if (GUILayout.Button("Host"))
                {
                    NetworkManager.Singleton.StartHost();
                }

                if (GUILayout.Button("Client"))
                {
                    NetworkManager.Singleton.StartClient();
                }

                if (GUILayout.Button("Server"))
                {
                    NetworkManager.Singleton.StartServer();
                }
            }
            else
            {
                if (GUILayout.Button("Disconnect"))
                {
                    NetworkManager.Singleton.Shutdown();
                }
            }

            GUILayout.EndArea();
        }
    }
}