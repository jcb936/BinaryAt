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

    public bool UpIsBlue { get; private set; }

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
        LeanTween.moveX(textRect, 710f, 0.5f).setEase(easeType).setOnComplete(() =>
        {
            textComponent.SetText(GetLine(questionsString, qNumb));
            textRect.anchoredPosition *= new Vector3(-1, 1, 1);
            LeanTween.moveX(textRect, 0f, 0.5f).setEase(easeType);
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
        int randomAnswer = Random.Range(0, 2);
        UpIsBlue = randomAnswer == 0;
        answerComps[0].SetText(answers[randomAnswer]);
        answerComps[1].SetText(answers[(randomAnswer + 1) % answers.Length]);
    }
}
