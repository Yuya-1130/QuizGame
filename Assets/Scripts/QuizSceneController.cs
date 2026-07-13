using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QuizSceneController : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] optionTexts = new TextMeshProUGUI[5];
    public TextMeshProUGUI chanceText;

    void Start()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        // 5問終わったら清算＆最終問題シーンへ
        if (manager.currentRound > 5)
        {
            SceneManager.LoadScene("SettlementScene");
            return;
        }

        SimpleQuestion q = manager.quizList[manager.currentRound - 1];
        roundText.text = $"第 {manager.currentRound} 問";
        questionText.text = q.text;
        chanceText.text = $"残りチャンス: {3 - manager.currentMissCount}回";

        for (int i = 0; i < 5; i++)
        {
            optionTexts[i].text = q.options[i];
        }
    }

    // 5つの選択肢ボタンから呼ぶ（引数: 0～4）
    public void OnOptionButtonClick(int index)
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        bool isRoundEnded = manager.CheckAnswer(index);

        if (isRoundEnded)
        {
            // 次の問題に進むか、次のシーンへ行くか判定するために再読み込み
            DisplayQuestion();
        }
        else
        {
            // ミスした時の残り回数表示更新
            chanceText.text = $"残りチャンス: {3 - manager.currentMissCount}回";
        }
    }
}