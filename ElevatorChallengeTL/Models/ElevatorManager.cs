using ElevatorChallengeTL.Interfaces;
using ElevatorChallengeTL.Services;

namespace ElevatorChallengeTL.Models
{
    public class ElevatorManager : IElevatorManager
    {
        private readonly List<IElevator> _elevators;
        private readonly Dictionary<int, int> _peopleWaiting;
        private readonly IMovementService _movementService;
        private readonly IPersonManagementService _personManagementService;

        public ElevatorManager(int numberOfElevators, int floors, int maxPeoplePerElevator, int weightLimit)
        {
            _elevators = new List<IElevator>();
            _peopleWaiting = new Dictionary<int, int>();
            _movementService = new MovementService();
            _personManagementService = new PersonManagementService();

            for (int i = 1; i <= numberOfElevators; i++)
                _elevators.Add(new Elevator(i, floors, maxPeoplePerElevator, weightLimit, _personManagementService));
            
            // Initialize people waiting dictionary
            for (int i = 0; i < floors; i++)
                _peopleWaiting[i] = 0; // Initialize with 0 people waiting
        }

        public List<IElevator> GetElevators()
        {
            return _elevators;
        }

        public void DisplayStatus()
        {
            Console.WriteLine("Elevator Status:");
            foreach (var elevator in _elevators)
                elevator.DisplayStatus();
            
            Console.WriteLine("\nPeople Waiting on Floors:");
            foreach (var floor in _peopleWaiting.Keys)
                Console.WriteLine($"Floor {floor}: {_peopleWaiting[floor]} people");
        }

        public IElevator CallElevator(int floor)
        {
            var nearestElevator = _elevators.OrderBy(e => Math.Abs(e.CurrentFloor - floor)).First();
            nearestElevator.MoveToFloor(floor, _peopleWaiting[floor]);
            _peopleWaiting[floor] = 0;
            return nearestElevator;
        }

        public void SetPeopleWaiting(int floor, int people)
        {
            if (!_peopleWaiting.ContainsKey(floor))
                _peopleWaiting[floor] = 0; // Initialize with 0 if key does not exist

            _peopleWaiting[floor] = people;
        }

        public void MoveElevatorToDestination(IElevator elevator, int floor)
        {
            _movementService.MoveToFloor(elevator, floor);
        }

        public Dictionary<int, int> GetPeopleWaiting()
        {
            return _peopleWaiting;
        }
    }
}
