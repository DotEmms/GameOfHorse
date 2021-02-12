namespace GameOfGoose
{
    public interface ISquare
    {
        int ID { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string Image { get; set; }

        void AssignPawnImage();
    }
}