
namespace ElevatorChallengeTL.Interfaces
{
    // Open/Closed Principle (OCP) and Dependency Inversion Principle (DIP): IElevatorManager interface
    public interface IElevatorManager
    {
        void DisplayStatus();
        IElevator CallElevator(int floor);
        void SetPeopleWaiting(int floor, int people);
        void MoveElevatorToDestination(IElevator elevator, int floor);
    }
}
