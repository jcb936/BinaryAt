using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Question : MonoBehaviour
{
    [SerializeField]
    private TextAsset questionsTexts;

    [SerializeField]
    private TextAsset answersTexts;

    [SerializeField]
    private TextMeshProUGUI textComponent;
    
    [SerializeField]
    private LeanTweenType easeType;

    [SerializeField]
    private TextMeshProUGUI[] answerComps;

    private string questionsString;

    private string answersString;

    private RectTransform textRect;

    private void Awake() {
        questionsString = questionsTexts.text;
        answersString = answersTexts.text;
        textComponent.gameObject.SetActive(false);
        textRect = textComponent.rectTransform;

    }

    public void StartQs() {
        textComponent.SetText(GetLine(questionsString, 1));
        SetAnswers(1);
        textComponent.gameObject.SetActive(true);
    }

    public void NextQuestion(int qNumb) {
        LeanTween.moveX(textRect, 710f, 1f).setEase(easeType).setOnComplete(() =>
        {
            textComponent.SetText(GetLine(questionsString, qNumb));
            textRect.anchoredPosition *= new Vector3(-1, 1, 1);
            LeanTween.moveX(textRect, 0f, 1f).setEase(easeType);
            SetAnswers(qNumb);
        });
    }

    private string GetLine(string text, int lineNo) {
        string[] lines = text.Replace("\r","").Split('\n');
        return lines.Length >= lineNo ? lines[lineNo-1] : null;
    }

    private string[] GetAnswers(string text, int lineNo) {
        string[] lines = text.Replace("\r","").Split('\n');
        return (lines.Length >= lineNo ? lines[lineNo-1] : "").Split(' ');
    }

    private void SetAnswers(int lineNo) {
        string[] answers = GetAnswers(answersString, lineNo);
        for (int i = 0; i < answerComps.Length; i++) {
            answerComps[i].SetText(answers[i]);
        }
    }
}
