using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;

        gameOver.alpha = 0f;
        gameOver.interactable = false;

    }

    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

}
