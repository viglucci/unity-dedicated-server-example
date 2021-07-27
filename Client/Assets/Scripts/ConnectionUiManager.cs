using UnityEngine;
using UnityEngine.UI;

public class ConnectionUiManager : MonoBehaviour
{
    public static ConnectionUiManager Instance;

    public GameObject startMenu;
    public InputField usernameField;
    public Button connectButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            connectButton.onClick.AddListener(OnConnectButtonClick);
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"Instance of {GetType().Name} already exists. Newly created instance will be destroyed.");
            Destroy(this);
        }
    }

    private void OnConnectButtonClick()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        ClientManager.Instance.Connect();
    }
}