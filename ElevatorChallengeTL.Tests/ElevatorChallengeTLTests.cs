using ElevatorChallengeTL.Interfaces;
using ElevatorChallengeTL.Models;
using ElevatorChallengeTL.Services;
using Moq;

namespace ElevatorChallengeTL.Tests
{
    public class ElevatorSimulatorTests
    {
        [Fact]
        public void MoveToFloor_ShouldUpdateCurrentFloorAndDirection()
        {
            // Arrange
            var personManagementService = new PersonManagementService();
            var elevator = new Elevator(1, 10, 8, 8, personManagementService);
            var movementService = new MovementService();
            int destinationFloor = 5;

            // Act
            movementService.MoveToFloor(elevator, destinationFloor);

            // Assert
            Assert.Equal(destinationFloor, elevator.CurrentFloor);
            Assert.Equal("Idle", elevator.Direction); // Should be Idle once the movement is completed
        }

        [Fact]
        public void MoveToFloor_ShouldUpdatePeopleOnboard()
        {
            // Arrange
            var personManagementService = new PersonManagementService();
            var elevator = new Elevator(1, 10, 8, 8, personManagementService);
            int initialPeople = 2;
            int destinationFloor = 5;

            // Act
            elevator.MoveToFloor(destinationFloor, initialPeople);

            // Assert
            Assert.Equal(initialPeople, elevator.PeopleOnboard);
        }

        [Fact]
        public void MoveToFloor_ShouldNotExceedWeightLimit()
        {
            // Arrange
            var personManagementService = new PersonManagementService();
            var elevator = new Elevator(1, 10, 8, 8, personManagementService);
            int peopleWaiting = 10; // More than the weight limit
            int destinationFloor = 5;

            // Act
            elevator.MoveToFloor(destinationFloor, peopleWaiting);

            // Assert
            Assert.Equal(elevator.WeightLimit, elevator.PeopleOnboard);
        }
    }

    public class ElevatorManagerTests
    {
        [Fact]
        public void CallElevator_ShouldReturnNearestElevator()
        {
            // Arrange
            var movementService = new MovementService();
            var personManagementService = new PersonManagementService();
            var elevatorManager = new ElevatorManager(3, 10, 8, 8);

            // Manually set the floors of the elevators for testing
            var elevator1 = elevatorManager.GetElevators().ElementAt(0) as Elevator;
            var elevator2 = elevatorManager.GetElevators().ElementAt(1) as Elevator;
            var elevator3 = elevatorManager.GetElevators().ElementAt(2) as Elevator;

            elevator1.CurrentFloor = 0;
            elevator2.CurrentFloor = 5;
            elevator3.CurrentFloor = 10;

            // Act
            var result = elevatorManager.CallElevator(7);

            // Assert
            Assert.Equal(elevator2, result); // Expecting the elevator at floor 5 (nearest to floor 7)
        }

        [Fact]
        public void MoveElevatorToDestination_ShouldMoveToCorrectFloor()
        {
            // Arrange
            var movementService = new MovementService();
            var personManagementService = new PersonManagementService();
            var elevatorManager = new ElevatorManager(1, 10, 8, 8);
            var mockElevator = elevatorManager.GetElevators().First() as Elevator;
            int destinationFloor = 5;

            // Act
            elevatorManager.MoveElevatorToDestination(mockElevator, destinationFloor);

            // Assert
            Assert.Equal(destinationFloor, mockElevator.CurrentFloor);
        }

        [Fact]
        public void SetPeopleWaiting_ShouldUpdatePeopleWaitingForFloor()
        {
            // Arrange
            var movementService = new MovementService();
            var personManagementService = new PersonManagementService();
            var elevatorManager = new ElevatorManager(3, 10, 8, 8);
            int floor = 3;
            int people = 10;

            // Act
            elevatorManager.SetPeopleWaiting(floor, people);

            // Assert
            var waitingPeople = elevatorManager.GetPeopleWaiting()[floor];
            Assert.Equal(people, waitingPeople);
        }

        [Fact]
        public void CallElevator_ShouldNotBoardMorePeopleThanWeightLimit()
        {
            // Arrange
            var movementService = new MovementService();
            var personManagementService = new PersonManagementService();
            var elevatorManager = new ElevatorManager(1, 10, 8, 8);
            var elevator = elevatorManager.GetElevators().First() as Elevator;
            elevator.CurrentFloor = 0;
            int peopleWaiting = 9; // More than the weight limit
            int callFloor = 0;
            elevatorManager.SetPeopleWaiting(callFloor, peopleWaiting);

            // Act
            var selectedElevator = elevatorManager.CallElevator(callFloor);

            // Assert
            Assert.Equal(elevator.WeightLimit, selectedElevator.PeopleOnboard);
        }
    }
}