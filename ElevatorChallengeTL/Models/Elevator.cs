using ElevatorChallengeTL.Interfaces;
using ElevatorChallengeTL.Services;

namespace ElevatorChallengeTL.Models
{
    // Single Responsibility Principle (SRP): Elevator class
    public class Elevator : IElevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public int MaxPeople { get; set; }
        public int PeopleOnboard { get; set; }
        public int WeightLimit { get; set; }
        public string Direction { get; set; }
        public int TotalFloors { get; set; }

        private readonly IPersonManagementService _personManagementService;

        public Elevator(int id, int totalFloors, int maxPeople, int weightLimit, IPersonManagementService personManagementService)
        {
            Id = id;
            CurrentFloor = 0;
            MaxPeople = maxPeople;
            PeopleOnboard = 0;
            WeightLimit = weightLimit;
            Direction = "Idle";
            TotalFloors = totalFloors;
            _personManagementService = personManagementService;
        }

        public void MoveToFloor(int floor, int peopleWaiting)
        {
            _personManagementService.ManagePeople(this, peopleWaiting);
            var movementService = new MovementService();
            movementService.MoveToFloor(this, floor);
        }

        public void DisplayStatus()
        {
            Console.WriteLine($"Elevator {Id}: Floor {CurrentFloor}, {PeopleOnboard}/{WeightLimit} people, Direction: {Direction}");
        }
    }
}
