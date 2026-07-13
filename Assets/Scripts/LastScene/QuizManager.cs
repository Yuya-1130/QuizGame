using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public QuestionData[] questions;   // 道中の問題
    public int currentIndex = 0;       // 今の問題番号
    public int life = 3;               // 道中ライフ
    public int playerPoints = 0;       // プレイヤーのポイント
    public int revealCount = 4;        // 難易度で決める（例：ノーマル4）

    void Start()
    {
        ShowQuestion();
    }

    void ShowQuestion()
    {
        var q = questions[currentIndex];
        Debug.Log("問題：" + q.questionText);
        // UI側で q.choices[] を表示する
    }

    public void OnChoiceSelected(int index)
    {
        var q = questions[currentIndex];

        if (index == q.correctIndex)
        {
            playerPoints += 100; // 正解ポイント
        }
        else
        {
            life--;
            playerPoints = Mathf.Max(0, playerPoints - 50); // ミスでポイント減少
        }

        NextQuestion();
    }

    void NextQuestion()
    {
        currentIndex++;

        if (currentIndex >= 5)
        {
            GoToFinalScene();
        }
        else
        {
            ShowQuestion();
        }
    }

    void GoToFinalScene()
    {
        PlayerPrefs.SetInt("PlayerPoints", playerPoints);
        PlayerPrefs.SetInt("RevealCount", revealCount);

        SceneManager.LoadScene("FinalScene");
    }
}