namespace TennisKata
{
    public static class Scoring
    {
        public static Score Score(Score current, Player winner)
            => current.Match(
                points: p => ScoreWhenPoints(p, winner),
                forty: f => ScoreWhenForty(f, winner),
                deuce: _ => ScoreWhenDeuce(winner),
                advantage: a => ScoreWhenAdvantage(a, winner),
                game: g => ScoreWhenGame(g));

        public static Score ScoreWhenAdvantage(Score.Advantage current, Player winner)
            => current.Player == winner
                ? new Score.Game(winner)
                : new Score.Deuce();

        public static Score ScoreWhenDeuce(Player winner)
            => new Score.Advantage(winner);

        public static Score ScoreWhenForty(Score.Forty current, Player winner)
            => current.Player == winner
                ? new Score.Game(winner)
                : Point
                    .Increment(current.OtherPlayerPoint)
                    .Match(
                        none: () => new Score.Deuce() as Score,
                        some: point => current with { OtherPlayerPoint = point });

        public static Score ScoreWhenPoints(Score.Points current, Player winner)
            => Point
                .Increment(PointFor(winner, current))
                .Match(
                    none: () => new Score.Forty(winner, PointFor(Player.Other(winner), current)),
                    some: point => PointTo(winner, point, current));

        public static Score ScoreWhenGame(Score.Game game)
            => game;

        private static Score PointTo(Player player, Point point, Score.Points current)
            => player.Match(
                playerOne: _ => current with { PlayerOnePoint = point },
                playerTwo: _ => current with { PlayerTwoPoint = point });

        private static Point PointFor(Player player, Score.Points current)
            => player.Match(
                playerOne: _ => current.PlayerOnePoint,
                playerTwo: _ => current.PlayerTwoPoint);
    }
}
