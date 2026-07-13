using UnityEngine;

[System.Serializable]
public class QuestionData
{
    public string questionText;   // 問題文
    public string[] choices;      // 選択肢（5つ）
    public int correctIndex;      // 正解番号（0〜4）
}