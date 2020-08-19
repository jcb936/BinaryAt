using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Question : MonoBehaviour
{
    [SerializeField]
    private TextAsset questionsTexts;

    [SerializeField]
    private TextMeshProUGUI textComponent;

    private string questionsString;

    private int currentQuestion;

    private void Awake() {
        currentQuestion = 1;
        questionsString = questionsTexts.text;
        textComponent.SetText(GetLine(questionsString, currentQuestion));
    }

    public void NextQuestion() {
        currentQuestion++;
        textComponent.SetText(GetLine(questionsString, currentQuestion));
    }

    /// <summary>
    /// Taken from https://stackoverflow.com/questions/2606368/how-to-get-specific-line-from-a-string-in-c
    /// </summary>
    private string GetLine(string text, int lineNo)
    {
        string[] lines = text.Replace("\r","").Split('\n');
        return lines.Length >= lineNo ? lines[lineNo-1] : null;
    }
}
