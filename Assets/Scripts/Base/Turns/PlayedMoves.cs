using ModestTree;
using UnityEngine;

public class PlayedMoves : IPlayedTurn
{
    public SpriteRenderer spriteRenderer;
    public Model model;
    public Collider2D collider2D;
    public Model[] models;
    public int playerIndex { get; set; }

    public PlayedMoves(Model model, RaycastHit2D hit, SpriteRenderer spriteRenderer, Model[] models)
    {
        this.models = models;
        this.model = model;
        collider2D = hit.collider;
        this.spriteRenderer = spriteRenderer;
    }
    public bool CheckPosition(int[] intList)
    {
        return collider2D.gameObject.GetComponent<Data>().pos == intList;
    }
    public int[] GetPosition()
    {
        return collider2D.gameObject.GetComponent<Data>().pos;
    }
    public void Undo()
    {
        playerIndex = -1;
        spriteRenderer.sprite = null;
        spriteRenderer.color = Color.white;
        collider2D.enabled = true;
    }
    public void Execute()
    {
        playerIndex = models.IndexOf(model);
        spriteRenderer.sprite = model.sprite;
        spriteRenderer.color = model.color;
        collider2D.enabled = false;
    }
}