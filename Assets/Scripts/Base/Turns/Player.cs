using UnityEngine;
using Zenject;
public class Player : ITurn
{
    [Inject] public Model[] models;
    public int currentTurn { get; set; }

    public int turnIndex { get; set; }

    CommandHandler CommandHandler;

    [Inject]
    public Player(CommandHandler CommandHandler)
    {
        this.CommandHandler = CommandHandler;
    }
    public void PlayTurn(Vector3 pos, int playerCount, Model model, GameManager GameManager, UI_Manager UI_Manager)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10)), Vector2.zero);
        if (hit.collider != null)
        {
            SpriteRenderer spriteRenderer = hit.collider.gameObject.GetComponent<SpriteRenderer>();
            PlayedMove(model, hit, spriteRenderer, playerCount, GameManager, UI_Manager);
            currentTurn = turnIndex % playerCount;
        }
    }
    private void PlayedMove(Model model, RaycastHit2D hit, SpriteRenderer spriteRenderer, int count, GameManager GameManager, UI_Manager UI_Manager)
    {
        IPlayedTurn turn = new PlayedMoves(model, hit, spriteRenderer, models);
        CommandHandler.AddCommand(turn, GameManager, UI_Manager, this);
    }
}
