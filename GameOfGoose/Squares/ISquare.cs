namespace GameOfGoose
{
    interface ISquare
    {
        int ID { get; set; }
        string Name { get; set; }

        void AssignPawnImage();
    }
}