using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // シーン切り替えに必須


public class SimpleGameManager : MonoBehaviour
{
    public List<int> openedLetterIndices = new List<int>();
    

    // どこからでもデータを参照できるようにするための仕組み（シングルトン）
    public static SimpleGameManager instance;

    [Header("--- クイズデータ（インスペクターで5問入れる） ---")]
    public List<SimpleQuestion> quizList = new List<SimpleQuestion>();

    [Header("--- プレイヤーの状態（自動で保持されます） ---")]
    public int currentRound = 1;
    public int totalPoints = 0;
    public int totalOpenedLetters = 0;
    public int currentMissCount = 0;

    // イージー最終問題のデータ（固定）
    public readonly string finalQuestionText = "イカに1を足すとイカイになります。タコに1を足すと何という言葉になるでしょう。";
    public readonly string[] finalOptions = { "タコイ", "タコワン", "イカコ", "タコタ", "タコチ" };
    public readonly int finalAnswerIndex = 0;
    public readonly int finalTotalLetters = 34;

    void Awake()
    {
        // シーンが変わってもこのオブジェクトを消さない設定
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetGame()
    {
        currentRound = 1;         // 1問目に戻す
        totalPoints = 0;          // ポイントを0にする
        totalOpenedLetters = 0;   // 解放文字数を0にする
        currentMissCount = 0;     // ミスカウントを0にする

        // 新しく作ったランダム虫食いのリストも綺麗に空っぽにする！
        if (openedLetterIndices != null)
        {
            openedLetterIndices.Clear();
        }
    }

    // ■ 最初からやり直す処理
    public void ResetGameData()
    {
        currentRound = 1;
        totalPoints = 0;
        totalOpenedLetters = 0;
        currentMissCount = 0;
    }

    // ■ 道中クイズの判定処理
    public bool CheckAnswer(int chosenIndex)
    {
        SimpleQuestion q = quizList[currentRound - 1];

        if (chosenIndex == q.correctAnswerIndex)
        {
            int[] pointsTable = { 30, 20, 10 };
            totalPoints += pointsTable[currentMissCount];
            totalOpenedLetters += 5; // 正解で2文字解放

            currentRound++;
            currentMissCount = 0;
            return true; // 正解
        }
        else
        {
            currentMissCount++;
            if (currentMissCount >= 3)
            {
                // 3回ミスで失敗、次へ
                currentRound++;
                currentMissCount = 0;
                return true; // 終了という意味でtrueを返す
            }
            return false; // 不正解（まだチャンスあり）
        }
    }
}
