using ElevatorChallengeTL.Interfaces;
using ElevatorChallengeTL.Models;

namespace ElevatorChallengeTL
{
    class Program
    {
        static void Main(string[] args)
        {
            int numberOfElevators = 3;
            int floors = 10;
            int maxPeoplePerElevator = 8;
            int weightLimit = 8; // Weight limit in terms of number of people

            IElevatorManager elevatorManager = new ElevatorManager(numberOfElevators, floors, maxPeoplePerElevator, weightLimit);

            while (true)
            {
                Console.Clear();
                elevatorManager.DisplayStatus();

                Console.WriteLine("1. Call Elevator");
                Console.WriteLine("2. Set People Waiting on Floor");
                Console.WriteLine("3. Exit");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter your current floor to call the elevator: ");
                        int currentFloor = int.Parse(Console.ReadLine());
                        IElevator elevator = elevatorManager.CallElevator(currentFloor);

                        Console.Write("Enter the number of people waiting: ");
                        int peopleWaiting = int.Parse(Console.ReadLine());

                        if (peopleWaiting > elevator.WeightLimit - elevator.PeopleOnboard)
                        {
                            Console.WriteLine($"Warning: The number of people waiting exceeds the remaining capacity of the elevator. Only {elevator.WeightLimit - elevator.PeopleOnboard} people can board.");
                            peopleWaiting = elevator.WeightLimit - elevator.PeopleOnboard;
                        }

                        elevatorManager.SetPeopleWaiting(currentFloor, peopleWaiting);

                        Console.Write("Enter your destination floor: ");
                        int destinationFloor = int.Parse(Console.ReadLine());
                        elevatorManager.MoveElevatorToDestination(elevator, destinationFloor);
                        break;

                    case 2:
                        Console.Write("Enter floor to set people: ");
                        int setFloor = int.Parse(Console.ReadLine());

                        Console.Write("Enter number of people: ");
                        int people = int.Parse(Console.ReadLine());

                        elevatorManager.SetPeopleWaiting(setFloor, people);
                        break;

                    case 3:
                        return;
                }
            }
        }
    }
}
