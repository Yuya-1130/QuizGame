using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    // クイズ1問分のデータを定義する構造体
    [Serializable]
    public struct QuizData
    {
        public string question;    // 問題文
        public string[] choices;   // 5択の選択肢（※必ず1番目を正解に設定する）
        public int difficulty;     // 1:簡単, 2:普通, 3:難しい, 4:最終問題（イージー）, 5:最終（ノーマル）, 6:最終（ハード）
    }

    [Header("UI要素（インスペクターから紐付け）")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button[] choiceButtons;

    [Header("クイズデータ（インスペクターで18問分入力可能）")]
    [SerializeField] private List<QuizData> allQuizList = new List<QuizData>();

    // 進行管理用の変数
    private int _currentQuizIndex = 0;   // 今何問目か
    private string _correctAnswerText;   // 現在の問題の正解文字列

    void Start()
    {
        // 本番はインスペクターから18問データを入れますが、
        // もし空っぽならテスト用に3問だけコード側で生成します
        if (allQuizList.Count == 0)
        {
            CreateDebugData();
        }

        // 第1問目を開始！
        LoadQuiz(_currentQuizIndex);
    }

    // 指定された番号のクイズを画面にセットアップする関数（使い回しルーチン）
    public void LoadQuiz(int index)
    {
        if (index >= allQuizList.Count)
        {
            Debug.Log("全問題クリア！リザルト画面へ遷移する処理などをここに書く");
            return;
        }

        QuizData currentQuiz = allQuizList[index];

        // 1. シャッフル前に正解のテキスト（必ず配列の[0]番目）を記憶
        _correctAnswerText = currentQuiz.choices[0];

        // 2. 5択をランダムシャッフル
        string[] shuffledChoices = currentQuiz.choices.OrderBy(x => Guid.NewGuid()).ToArray();

        // 3. UIへの反映
        questionText.text = currentQuiz.question;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            // 一旦ボタンをすべて押せる状態に戻す
            choiceButtons[i].interactable = true;

            TextMeshProUGUI buttonText = choiceButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = shuffledChoices[i];

            // 4. ボタンのイベント登録（クロージャ対策）
            choiceButtons[i].onClick.RemoveAllListeners();
            string selectedText = shuffledChoices[i];
            choiceButtons[i].onClick.AddListener(() => OnChoiceSelected(selectedText));
        }
    }

    // ボタンが押された時の処理
    private void OnChoiceSelected(string clickedText)
    {
        if (clickedText == _correctAnswerText)
        {
            Debug.Log("正解！次の問題へ");

            // TODO: ここでスコア加算の計算を行う

            // 1秒後に次の問題をロード（すぐ切り替わると味気ないため）
            _currentQuizIndex++;
            Invoke(nameof(NextQuiz), 1.0f);
        }
        else
        {
            Debug.Log("不正解！");

            // TODO: ここでミス回数をカウント（3ミスで失敗処理）

            // 間違えたボタンはグレーアウトして押せなくする
            // （今押されたテキストを持つボタンを探して無効化）
            foreach (var button in choiceButtons)
            {
                if (button.GetComponentInChildren<TextMeshProUGUI>().text == clickedText)
                {
                    button.interactable = false;
                }
            }
        }
    }

    private void NextQuiz()
    {
        LoadQuiz(_currentQuizIndex);
    }

    // 動作確認用のデバッグデータ作成関数
    private void CreateDebugData()
    {
        allQuizList.Add(new QuizData { question = "日本一高い山は？", choices = new[] { "富士山", "北岳", "槍ヶ岳", "間ノ岳", "浅間山" }, difficulty = 1 });
        allQuizList.Add(new QuizData { question = "フランスの首都は？", choices = new[] { "パリ", "ロンドン", "ローマ", "ベルリン", "マドリード" }, difficulty = 1 });
        allQuizList.Add(new QuizData { question = "最初の地下鉄は？", choices = new[] { "ロンドン", "マンチェスター", "リヴァプール", "エディンバラ", "バーミンガム" }, difficulty = 2 });
    }
}