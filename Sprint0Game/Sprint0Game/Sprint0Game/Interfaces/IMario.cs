using Microsoft.Xna.Framework;

namespace Sprint0Game
{
    public interface IMario : IObject
    {
        IMarioState CurrentState { get; set; }
        ILevel Level { get; }
        bool IsStar { get; set; }
        bool IsTransitioningFromDamage { get; set; }
        bool IsTransitioningFromItem { get; set; }
        bool IsTravelingPipe { get; set; }
        bool IsUnderground { get; set; }
        bool OnTransPipe { get; set; }
        bool TouchingTransPipe { get; set; }
        int TransitionFromDamageTimer { get; set; }
        int MetalTimer { get; set; }
        
        bool IsBig();
        bool IsFire();
        bool IsDead();
        bool IsDying();
        bool IsRightFacing();
        bool IsCrouching();
        bool IsFalling();
        bool IsIdle();
        bool IsJumping();
        bool IsRunning();
        MarioPowerLevel PowerLevel();

        void RespondToRequest(MarioActionRequest change);
        void TakeLeftInput();
        void TakeRightInput();
        void TakeUpInput();
        void TakeDownInput();
        void TakeNoInput();
        void TakeUpReleased();
        void EnableAllInput();
        void DisableAllInput();
        void Flash(int timer);
    }
}
