using Funcky;
using Funcky.Monads;

namespace TennisKata
{
    [DiscriminatedUnion]
    public abstract partial record Point
    {
        public static Option<Point> Increment(Point point)
            => point.Match(
                love: _ => Option.Some<Point>(new Fifteen()),
                fifteen: _ => Option.Some<Point>(new Thirty()),
                thirty: _ => Option<Point>.None());

        public sealed partial record Love : Point;

        public sealed partial record Fifteen : Point;

        public sealed partial record Thirty : Point;
    }
}
