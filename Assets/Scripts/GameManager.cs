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

    private int currentQuestion;

    private int totalQs;

    private int blueScore;

    private int redScore;

    private void Awake() {
        totalQs = 3;
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

        yield return new WaitForSeconds(5f);

        titleText.SetText("we are going to start the test now");

        yield return new WaitForSeconds(3f);

        titleText.gameObject.SetActive(false);

        questionObject.StartQs();

        blueChoice.onClick.AddListener(MoveBlue);
        redChoice.onClick.AddListener(MoveRed);

        blueChoice.gameObject.SetActive(true);
        redChoice.gameObject.SetActive(true);



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

        var toResize = redScore > blueScore ? redSquare : blueSquare;
        LeanTween.size(toResize, new Vector2(5000, 5000), 3f);
        LeanTween.alpha(toResize, 1f, 1f);
    }

    private void No() {

    }

    private void MoveBlue() {
        blueScore++;
        LeanTween.moveX(blueSquare, blueSquare.anchoredPosition.x - 20f, 0.5f);
        if (currentQuestion <= totalQs)
            Next();
        else
            End();
    }

    private void MoveRed() {
        redScore++;
        LeanTween.moveX(redSquare, redSquare.anchoredPosition.x + 20, 0.5f);
        if (currentQuestion <= totalQs)
            Next();
        else
            End();
    }
}
