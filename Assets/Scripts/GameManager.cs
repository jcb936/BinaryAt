using System.Collections;
using System.Collections.Generic;
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

    private void Awake() {
        blueChoice.onClick.AddListener(MoveBlue);
        redChoice.onClick.AddListener(MoveRed);
    }

    private void MoveBlue() {
        LeanTween.moveX(blueSquare, blueSquare.anchoredPosition.x - 20f, 0.5f);
        questionObject.NextQuestion();

    }

    private void MoveRed() {
        LeanTween.moveX(redSquare, redSquare.anchoredPosition.x + 20, 0.5f);
        questionObject.NextQuestion();
    }
}
