using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TMP_Text _statusText;
    [SerializeField] private Button _backButton;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        HideAll();
        _backButton.onClick.AddListener(BackToHome);
    }

    private void BackToHome()
    {
        using (new Load("Backing to home..."))
        {
            SceneManager.LoadScene("Auth");
        }
    }

    public void ShowMessage(string message)
    {
        _statusText.text = message;
        _statusText.gameObject.SetActive(true);
        _backButton.gameObject.SetActive(true);
    }

    private void HideAll()
    {
        _backButton.gameObject.SetActive(false);
        _statusText.gameObject.SetActive(false);
    }
}