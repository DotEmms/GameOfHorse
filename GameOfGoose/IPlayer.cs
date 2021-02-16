using System.ComponentModel;

namespace GameOfGoose
{
    public interface IPlayer
    {
        int Column { get; set; }
        string DisplayedImagePath { get; set; }
        int Id { get; set; }
        bool IsFirstRound { get; set; }
        string Name { get; set; }
        int PawnLocation { get; set; }
        int Row { get; set; }
        int TurnPenalty { get; set; }

        event PropertyChangedEventHandler PropertyChanged;

        void ResetId();
    }
}