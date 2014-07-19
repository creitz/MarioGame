
namespace Sprint0Game
{
    public sealed class AnimatedMarioFactory
    {
        //prevent instantiation
        private AnimatedMarioFactory() {}

        public static CrouchingMario CrouchingMario(IMario mario, IMarioState state) {
            return new CrouchingMario(mario, state.IsRightFacing(), state.PowerLevel());
        }

        public static DyingMario DyingMario(IMario mario)
        {
            return new DyingMario(mario);
        }

        public static FallingMario FallingMario(IMario mario, IMarioState state)
        {
            return new FallingMario(mario, state.IsRightFacing(), state.PowerLevel());
        }

        public static IdleMario IdleMario(IMario mario, IMarioState state)
        {
            return new IdleMario(mario, state.IsRightFacing(), state.PowerLevel());
        }

        public static JumpingMario JumpingMario(IMario mario, IMarioState state)
        {
            return new JumpingMario(mario, state.IsRightFacing(), state.PowerLevel());
        }

        public static RunningMario RunningMario(IMario mario, IMarioState state)
        {
            return new RunningMario(mario, state.IsRightFacing(), state.PowerLevel());
        }
    }
}
