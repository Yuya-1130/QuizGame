using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ResultSceneController : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        if (SimpleGameManager.instance != null)
        {
            finalScoreText.text = $"{SimpleGameManager.instance.totalPoints} pt";
        }
    }

    // 「タイトルへ戻る」ボタンから呼ぶ
    public void OnBackToTitleButtonClick()
    {
        SceneManager.LoadScene("TitleScene");
    }
}