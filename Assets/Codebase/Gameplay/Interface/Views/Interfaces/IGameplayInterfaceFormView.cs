﻿namespace Codebase.Gameplay.Interface.Views.Interfaces
{
    public interface IGameplayInterfaceFormView
    {
        public void SetCoinAmount(int currentValue, int difference);
        public void SetMaxUpgradePoints(int upgradeMaxValue);
        public void SetBallsToShoot(int amount);
        public void SetUpgradePoints(int upgradeValue);
    }
}