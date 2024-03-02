using Codebase.Tanks.Model;

namespace Codebase.Tanks.CQRS
{
    public class SetTankPositionCommand
    {
        private readonly TankModel _tank;

        public SetTankPositionCommand(TankModel tank)
        {
            _tank = tank;
        }
        
        public void Handle(float position)
        {
            _tank.Position = position;
        }
    }
}
