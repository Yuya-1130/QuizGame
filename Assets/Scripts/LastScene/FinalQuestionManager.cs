using UnityEngine;

public class FinalQuestionManager : MonoBehaviour
{
    public FinalQuestion question;
    public int revealCount; // イージー2 / ノーマル4 / ハード7

    public int playerPoints = 0; // プレイヤーのポイント

    void Start()
    {
        ApplyRandomMask();
    }

    void ApplyRandomMask()
    {
        // 全文字を非表示にする
        foreach (var c in question.chars)
            c.isRevealed = false;

        // ランダムで revealCount 個だけ開く
        for (int i = 0; i < revealCount; i++)
        {
            int r = Random.Range(0, question.chars.Length);
            question.chars[r].isRevealed = true;
        }
    }

    public bool RevealChar(int index)
    {
        // ポイントが足りない
        if (playerPoints < 20)
            return false;

        // すでに開いている
        if (question.chars[index].isRevealed)
            return false;

        // ポイント消費
        playerPoints -= 20;

        // 開く
        question.chars[index].isRevealed = true;

        return true;
    }
}