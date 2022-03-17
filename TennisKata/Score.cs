using Funcky;

namespace TennisKata
{
    [DiscriminatedUnion]
    public abstract partial record Score
    {
        public sealed partial record Points(Point PlayerOnePoint, Point PlayerTwoPoint) : Score;

        public sealed partial record Forty(Player Player, Point OtherPlayerPoint) : Score;

        public sealed partial record Deuce : Score;

        public sealed partial record Advantage(Player Player) : Score;

        public sealed partial record Game(Player Player) : Score;
    }
}
