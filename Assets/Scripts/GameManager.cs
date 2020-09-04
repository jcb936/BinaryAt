using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region  SingletonStuff
    private GameManager _instance;
    public GameManager Instance {
        get {
            if (_instance == null)
                _instance = new GameManager();

            return _instance;
        }
    }
    #endregion

    [SerializeField]
    private RectTransform blueSquare;

    [SerializeField]
    private RectTransform redSquare;

    [SerializeField]
    private Button redChoice;

    [SerializeField]
    private Button blueChoice;

    [SerializeField]
    private Question questionObject;

    [SerializeField]
    private TextMeshProUGUI titleText;

    [SerializeField]
    private TextAsset comments;

    [SerializeField]
    private TextMeshProUGUI finalText;

    [SerializeField]
    private GameObject finalPanel;

    [SerializeField]
    private GameObject redWinText;

    [SerializeField]
    private GameObject blueWinText;
    private int currentQuestion;

    private int totalQs;

    private int blueScore;

    private int redScore;

    private void Awake() {
        totalQs = 12;
        currentQuestion = 1;
        blueChoice.onClick.AddListener(() => StartCoroutine(StartGame()));
        redChoice.onClick.AddListener(() => Application.Quit());
    }

    private IEnumerator StartGame() {
        titleText.SetText("perfect");

        blueChoice.onClick.RemoveAllListeners();
        redChoice.onClick.RemoveAllListeners();

        blueChoice.gameObject.SetActive(false);
        redChoice.gameObject.SetActive(false);

        yield return new WaitForSeconds(3f);

        titleText.SetText("we are going to start the test now");

        yield return new WaitForSeconds(2f);

        titleText.gameObject.SetActive(false);

        questionObject.StartQs();

        blueChoice.onClick.AddListener(() => Chosen(true));
        redChoice.onClick.AddListener(() => Chosen(false));

        blueChoice.gameObject.SetActive(true);
        redChoice.gameObject.SetActive(true);
    }

    private void Chosen(bool isBlue) {
        if ((isBlue && questionObject.UpIsBlue) || (!isBlue && !questionObject.UpIsBlue))
            MoveBlue();
        else
            MoveRed();

    }

    private void Next() {
        currentQuestion++;
        questionObject.NextQuestion(currentQuestion);
    }

    private void End() {
        if (redScore == blueScore) {
            No();
            return;
        }

        questionObject.gameObject.SetActive(false);
        redChoice.gameObject.SetActive(false);
        blueChoice.gameObject.SetActive(false);
        RectTransform toResize;
        if (redScore > blueScore) {
            redWinText.SetActive(true);
            toResize = redSquare;
            redSquare.SetSiblingIndex(redSquare.GetSiblingIndex() + 1);
        } else {
            blueWinText.SetActive(true);
            toResize = blueSquare;
        }
        LeanTween.size(toResize, new Vector2(5000, 5000), 3f);
        LeanTween.alpha(toResize, 1f, 1f);
    }

    private void No() {
        finalText.SetText(GetHate(comments.text));
        finalPanel.SetActive(true);
    }

    private void MoveBlue() {
        blueScore++;
        blueChoice.interactable = false;
        redChoice.interactable = false;
        LeanTween.moveX(blueSquare, blueSquare.anchoredPosition.x - 40f, 1.1f).setOnComplete(() => {
            blueChoice.interactable = true;
            redChoice.interactable = true;
        });
        if (currentQuestion < totalQs)
            Next();
        else
            End();
    }

    private void MoveRed() {
        redScore++;
        blueChoice.interactable = false;
        redChoice.interactable = false;
        LeanTween.moveX(redSquare, redSquare.anchoredPosition.x + 40, 1.1f).setOnComplete(() => {
            blueChoice.interactable = true;
            redChoice.interactable = true;
        });
        if (currentQuestion < totalQs)
            Next();
        else
            End();
    }

    private string GetHate(string text) {
        string[] lines = text.Replace("\r","").Split("\n---\n".ToCharArray());
        return lines[Random.Range(0, lines.Length)];
    }
}
