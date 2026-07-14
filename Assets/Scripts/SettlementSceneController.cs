using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text; // 虫食い処理に必要
using System.Collections.Generic;
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

        // 5つの回答ボタンの文字をセット
        for (int i = 0; i < 5; i++)
        {
            finalOptionTexts[i].text = manager.finalOptions[i];
        }

        SyncInitialOpenedLetters();

        UpdateUI();
    }

    void SyncInitialOpenedLetters()
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        // すでにリストが作られていたらスキップ
        if (manager.openedLetterIndices.Count >= manager.totalOpenedLetters) return;

        // まだ開いていない文字の位置をランダムに選んで、獲得分だけ開ける
        while (manager.openedLetterIndices.Count < manager.totalOpenedLetters &&
               manager.openedLetterIndices.Count < manager.finalTotalLetters)
        {
            int randIndex = Random.Range(0, manager.finalTotalLetters);
            if (!manager.openedLetterIndices.Contains(randIndex))
            {
                manager.openedLetterIndices.Add(randIndex);
            }
        }
    }

    void UpdateUI()
    {
        var manager = SimpleGameManager.instance;
        if (manager == null) return;

        pointsText.text = $"所持ポイント: {manager.totalPoints} pt";
        openedLettersText.text = $"解放文字数: {manager.openedLetterIndices.Count} / {manager.finalTotalLetters} 文字";

        // ここをチェック！ 確実に manager.openedLetterIndices（リスト）を渡すようにします
        finalQuestionText.text = MakeMaskedSentence(manager.finalQuestionText, manager.openedLetterIndices);
    }

    //  開いている位置（リスト）を元に、それ以外を「■」にする関数
    string MakeMaskedSentence(string original, System.Collections.Generic.List<int> openedLetterIndices)
    {
        if (string.IsNullOrEmpty(original)) return "問題データが空です";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < original.Length; i++)
        {
            char c = original[i];

            // 記号や改行、スペースはそのまま表示
            if (c == '。' || c == '、' || c == '？' || c == ' ' || c == '\n')
            {
                sb.Append(c);
            }
            else
            {
                // リストの中に「現在の文字の位置（i）」が含まれているか、1つずつ泥臭くチェック
                bool isOpened = false;
                for (int j = 0; j < openedLetterIndices.Count; j++)
                {
                    if (openedLetterIndices[j] == i)
                    {
                        isOpened = true;
                        break;
                    }
                }

                if (isOpened)
                {
                    sb.Append(c); // 解放されていれば本物の文字を表示
                }
                else
                {
                    sb.Append("■"); // 閉じていれば虫食い
                }
            }
        }
        return sb.ToString();
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
            UpdateUI(); // 画面を更新（これで虫食いが1文字実体化します！）
        }
        else if (manager.totalPoints < 20)
        {
            messageText.text = "ポイントが足りません！";
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