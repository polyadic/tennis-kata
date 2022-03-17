namespace TennisKata
{
    public static class Scoring
    {
        public static Score Score(Score current, Player winner)
            => current.Match(
                points: ScoreWhenPoints(winner),
                forty: ScoreWhenForty(winner),
                deuce: _ => ScoreWhenDeuce(winner),
                advantage: ScoreWhenAdvantage(winner),
                game: ScoreWhenGame);

        public static Func<Score.Advantage, Score> ScoreWhenAdvantage(Player winner)
            => current
                => current.Player == winner
                    ? new Score.Game(winner)
                    : new Score.Deuce();

        public static Score ScoreWhenDeuce(Player winner)
            => new Score.Advantage(winner);

        public static Func<Score.Forty, Score> ScoreWhenForty(Player winner)
            => current
                => current.Player == winner
                    ? new Score.Game(winner)
                    : Point
                        .Increment(current.OtherPlayerPoint)
                        .Match(
                            none: () => new Score.Deuce() as Score,
                            some: point => current with { OtherPlayerPoint = point });

        public static Func<Score.Points, Score> ScoreWhenPoints(Player winner)
            => current
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
