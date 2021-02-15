namespace GameOfGoose
{
    public interface ISquare
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        int Row { get; set; }
        int Column { get; set; }
    }
}