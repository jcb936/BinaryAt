using UnityEngine;

public class InfiniteScroll : MonoBehaviour {

    private const float OUT_TO = -1075f;

    private const float OUT_FROM = 1075;

    [SerializeField]
    private RectTransform[] elements;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private float scrollSpeed;

    private void Awake() {
        Move(0);
    }

    private void Move(int toMove) {
        var toMoveOut = elements[toMove];
        var toMoveIn = elements[(toMove + 1) % elements.Length];
        LeanTween.moveX(toMoveOut, invert ? -OUT_TO : OUT_TO, scrollSpeed).setOnComplete(() =>
        {
            toMoveOut.anchoredPosition = new Vector2(invert ? -OUT_FROM : OUT_FROM, toMoveOut.anchoredPosition.y);
            Move((toMove + 1) % elements.Length);
            return;
        });
        LeanTween.moveX(toMoveIn, 0f, scrollSpeed);
    }
}