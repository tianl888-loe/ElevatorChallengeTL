using ElevatorChallengeTL.Interfaces;
using ElevatorChallengeTL.Models;

namespace ElevatorChallengeTL.Services
{
    public class PersonManagementService : IPersonManagementService
    {
        public void ManagePeople(IElevator elevator, int peopleWaiting)
        {
            var elevatorImpl = elevator as Elevator;
            if (elevatorImpl == null) throw new InvalidCastException();

            if (elevatorImpl.PeopleOnboard + peopleWaiting > elevatorImpl.WeightLimit)
            {
                Console.WriteLine($"Elevator {elevatorImpl.Id}: Weight limit exceeded! Only {elevatorImpl.WeightLimit - elevatorImpl.PeopleOnboard} more people can board.");
                elevatorImpl.PeopleOnboard = elevatorImpl.WeightLimit;
            }
            else
                elevatorImpl.PeopleOnboard += peopleWaiting;
        }
    }
}
