
namespace ElevatorChallengeTL.Interfaces
{
    // Single Responsibility Principle (SRP): IElevator interface
    public interface IElevator
    {
        int Id { get; }
        int CurrentFloor { get; }
        int MaxPeople { get; }
        int PeopleOnboard { get; }
        int WeightLimit { get; }
        string Direction { get; set; }
        int TotalFloors { get; }

        void MoveToFloor(int floor, int peopleWaiting);
        void DisplayStatus();
    }
}
