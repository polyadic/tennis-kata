using FsCheck;
using Funcky;

namespace TennisKata.Test
{
    internal class TennisArbitraries
    {
        public static void Register() => Arb.Register(typeof(TennisArbitraries));

        public static Arbitrary<Score> ArbitraryScore()
            => Arb.From(Gen.OneOf(
                Arb.Generate<Score.Points>().Select(points => points as Score),
                Arb.Generate<Score.Forty>().Select(forty => forty as Score),
                Arb.Generate<Score.Deuce>().Select(deuce => deuce as Score),
                Arb.Generate<Score.Advantage>().Select(advantage => advantage as Score),
                Arb.Generate<Score.Game>().Select(game => game as Score)));

        public static Arbitrary<Score.Forty> ArbitraryScoreForty()
            => (from player in Arb.Generate<Player>()
                from point in Arb.Generate<Point>()
                select new Score.Forty(player, point)).ToArbitrary();

        public static Arbitrary<Score.Points> ArbitraryScorePoints()
            => (from playerOnePoint in Arb.Generate<Point>()
                from playerTwoPoint in Arb.Generate<Point>()
                select new Score.Points(playerOnePoint, playerTwoPoint)).ToArbitrary();

        public static Arbitrary<Score.Deuce> ArbitraryDeuce()
            => Gen
                .Elements(new Score.Deuce[] { new Score.Deuce() })
                .ToArbitrary();

        public static Arbitrary<Score.Advantage> ArbitraryAdvantage()
            => (from player in Arb.Generate<Player>()
                select new Score.Advantage(player)).ToArbitrary();

        public static Arbitrary<Score.Game> ArbitraryGame()
            => (from player in Arb.Generate<Player>()
                select new Score.Game(player)).ToArbitrary();

        public static Arbitrary<Player> ArbitraryPlayer()
            => Gen
                .Elements(new Player[] { new Player.PlayerOne(), new Player.PlayerTwo() })
                .ToArbitrary();

        public static Arbitrary<Point> ArbitraryPoint()
            => Gen
                .Elements(new Point[] { new Point.Love(), new Point.Fifteen(), new Point.Thirty() })
                .ToArbitrary();
    }
}
