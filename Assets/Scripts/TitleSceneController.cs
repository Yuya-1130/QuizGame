using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneController : MonoBehaviour
{
    // スタートボタンから呼ぶ
    public void OnStartButtonClick()
    {
        if (SimpleGameManager.instance != null)
        {
            SimpleGameManager.instance.ResetGame();
        }
        // 「道中クイズシーン」へ遷移（※ビルド設定のシーン名と合わせてください）
        SceneManager.LoadScene("QuizScene");
    }
}
