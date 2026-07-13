using UnityEngine;
using UnityEngine.UI;

public class FinalQuestionUI : MonoBehaviour
{
    public FinalQuestionManager manager;
    public Text questionText;

    void Update()
    {
        questionText.text = BuildMaskedText();
    }

    string BuildMaskedText()
    {
        string result = "";

        foreach (var c in manager.question.chars)
        {
            result += c.isRevealed ? c.character.ToString() : "Å°";
        }

        return result;
    }
}