using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SimpleQuestion
{
    [TextArea(2, 3)] public string text; // –â‘è•¶
    public string[] options = new string[5]; // 5‘ğ
    public int correctAnswerIndex; // ³‰ğ‚Ì”Ô†i0`4j
}
