using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallengeTL.Interfaces
{
    public interface IMovementService
    {
        void MoveToFloor(IElevator elevator, int floor);
    }
}
