using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalAnswerManager : MonoBehaviour
{
    public FinalQuestion question;
    public int life = 3; // 最終問題のライフ
    private string[] shuffledChoices;
    private int correctIndexAfterShuffle;

    void Start()
    {
        ShuffleChoices();
    }

    void ShuffleChoices()
    {
        shuffledChoices = (string[])question.choices.Clone();

        // シャッフル
        for (int i = shuffledChoices.Length - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            string temp = shuffledChoices[i];
            shuffledChoices[i] = shuffledChoices[r];
            shuffledChoices[r] = temp;
        }

        // 正解位置を探す
        for (int i = 0; i < shuffledChoices.Length; i++)
        {
            if (shuffledChoices[i] == question.choices[question.correctIndex])
            {
                correctIndexAfterShuffle = i;
                break;
            }
        }
    }

    public void OnChoiceSelected(int index)
    {
        if (index == correctIndexAfterShuffle)
        {
            Debug.Log("正解！");
            // 正解時の処理（クリア演出など）
        }
        else
        {
            life--;
            Debug.Log("不正解！ ライフ残り：" + life);

            if (life <= 0)
            {
                Debug.Log("最終問題失敗 → スコア画面へ遷移");
                SceneManager.LoadScene("ScoreScene"); // ← スコア表示シーン名
            }
        }
    }

    public string[] GetChoices()
    {
        return shuffledChoices;
    }
}