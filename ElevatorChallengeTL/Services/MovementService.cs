using ElevatorChallengeTL.Interfaces;
using ElevatorChallengeTL.Models;

namespace ElevatorChallengeTL.Services
{
    public class MovementService : IMovementService
    {
        public void MoveToFloor(IElevator elevator, int floor)
        {
            var elevatorImpl = elevator as Elevator;
            if (elevatorImpl == null) throw new InvalidCastException();

            elevatorImpl.Direction = floor > elevatorImpl.CurrentFloor ? "Up" : floor < elevatorImpl.CurrentFloor ? "Down" : "Idle";

            // Simulate moving to the floor
            for (int i = elevatorImpl.CurrentFloor; i != floor; i += (elevatorImpl.Direction == "Up" ? 1 : -1))
            {
                elevatorImpl.CurrentFloor = i;
                elevatorImpl.DisplayStatus();
                Thread.Sleep(500); // Simulate time delay for moving between floors
            }

            elevatorImpl.CurrentFloor = floor;
            elevatorImpl.Direction = "Idle";
            elevatorImpl.DisplayStatus();
        }
    }
}
