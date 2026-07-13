using UnityEngine;

[CreateAssetMenu(fileName = "FinalQuestion", menuName = "Quiz/FinalQuestion")]
public class FinalQuestion : ScriptableObject
{
    public FinalCharData[] chars;   // 1文字ずつの配列
    public string[] choices;        // 選択肢(5択)
    public int correctIndex;        // 正解の番号(0〜4)
}
