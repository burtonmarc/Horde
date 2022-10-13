namespace Data
{
    public class PlayerModel : IModel

    {
    public int Health;

    public float MovementSpeed;

    public PlayerModel()
    {
        Health = 100;
        MovementSpeed = 2;
    }
    }
}