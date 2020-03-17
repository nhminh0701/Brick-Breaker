using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        levelText.text = $"Level {LevelDataBase.Instance.currentLevelId + 1}";
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLobbyScene(int lobbySceneIndex)
    {
        SceneManager.LoadScene(lobbySceneIndex);
    }
}
