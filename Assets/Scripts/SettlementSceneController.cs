using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettlementSceneController : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI openedLettersText;
    public TextMeshProUGUI finalQuestionText;
    public TextMeshProUGUI[] finalOptionTexts = new TextMeshProUGUI[5];
    public TextMeshProUGUI messageText;

    void Start()
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        messageText.text = "";
        for (int i = 0; i < 5; i++)
        {
            finalOptionTexts[i].text = manager.finalOptions[i];
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        pointsText.text = $"所持ポイント: {manager.totalPoints} pt";
        openedLettersText.text = $"解放文字数: {manager.totalOpenedLetters} / {manager.finalTotalLetters} 文字";

        // とりあえず今はそのまま表示（後で虫食いロジックを入れられます）
        finalQuestionText.text = manager.finalQuestionText;
    }

    // 「20ptで1文字開ける」ボタンから呼ぶ
    public void OnBuyLetterButtonClick()
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        if (manager.totalPoints >= 20 && manager.totalOpenedLetters < manager.finalTotalLetters)
        {
            manager.totalPoints -= 20;
            manager.totalOpenedLetters++;
            UpdateUI();
        }
    }

    // 最終問題の5つの回答ボタンから呼ぶ（引数: 0～4）
    public void OnFinalAnswerButtonClick(int index)
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        if (index == manager.finalAnswerIndex)
        {
            SceneManager.LoadScene("ResultScene");
        }
        else
        {
            messageText.text = "不正解！もう一度よく考えてみよう。";
        }
    }
}