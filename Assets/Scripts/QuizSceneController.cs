using UnityEngine;
using UnityEngine.UI; // ボタン（Button）を制御するために必須
using UnityEngine.SceneManagement;
using TMPro;

public class QuizSceneController : MonoBehaviour
{
    [Header("--- UIテキスト要素 (TMP) ---")]
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI chanceText;

    [Header("--- 選択肢のボタン本体（5個） ---")]
    public Button[] optionButtons = new Button[5];

    [Header("--- ボタンの中のテキスト（5個） ---")]
    public TextMeshProUGUI[] optionTexts = new TextMeshProUGUI[5];

    void Start()
    {
        DisplayQuestion();
    }

    void DisplayQuestion()
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        // 5問終わったら清算　最終問題シーンへ
        if (manager.currentRound > 5)
        {
            SceneManager.LoadScene("SettlementScene");
            return;
        }

        SimpleQuestion q = manager.quizList[manager.currentRound - 1];
        roundText.text = $"第 {manager.currentRound} 問";
        questionText.text = q.text;
        chanceText.text = $"残りチャンス: {3 - manager.currentMissCount}回";

        // 新しい問題になったので、5つのボタンをすべて「押せる状態」にリセット
        for (int i = 0; i < 5; i++)
        {
            optionButtons[i].interactable = true;
            optionTexts[i].text = q.options[i];
        }
    }

    // 5つの選択肢ボタンから呼ばれる（引数: 0～4）
    public void OnOptionButtonClick(int index)
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        bool isRoundEnded = manager.CheckAnswer(index);

        if (isRoundEnded)
        {
            // 正解、または3回ミスでラウンド終了 ➔ 次の問題（または次シーン）へ
            DisplayQuestion();
        }
        else
        {
            // 不正解だった場合：間違えたボタンだけを「半透明にして押せなくする」
            optionButtons[index].interactable = false;

            // 残り回数の表示を更新
            chanceText.text = $"残りチャンス: {3 - manager.currentMissCount}回";
        }
    }
}