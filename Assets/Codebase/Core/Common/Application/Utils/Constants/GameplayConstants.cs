namespace Codebase.Core.Common.Application.Utils.Constants
{
    public class GameplayConstants
    {
        public class Curtains
        {
            public const float AnimationTime = 0.3f;
        }

        public class Balls
        {
            public const float Speed = 6f;
        }

        public class Shooting
        {
            public const int DefaultAmountToShoot = 1;
            public const int TimeToShootMilliseconds = 150;
            public const int DefaultShootingUpgradeThreshold = 5;
        }

        public class Structures
        {
            
            public static readonly string[] DefaultStructure = { "Test Two Towers" };
            public const float BlockSize = 0.5f;
        }
    }
}
