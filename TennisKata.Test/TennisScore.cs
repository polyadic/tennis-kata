using FsCheck;
using FsCheck.Xunit;

namespace TennisKata.Test
{
    public class TennisScore
    {
        public TennisScore()
        {
            TennisArbitraries.Register();
        }

        [Property]
        public Property GivenDeuceWhenPlayerWinsThenScoreIsCorrect(Player winner)
        {
            var actual = Scoring.ScoreWhenDeuce(winner);

            var expected = new Score.Advantage(winner);
            return (expected == actual).ToProperty();
        }

        [Property]
        public Property GivenAdvantageWhenAdvantagedPlayerWinsThenScoreIsCorrect(Player advantagedPlayer)
        {
            var scoreWhenAdvantage = Scoring.ScoreWhenAdvantage(advantagedPlayer);
            var actual = scoreWhenAdvantage(new Score.Advantage(advantagedPlayer));

            var expected = new Score.Game(advantagedPlayer);
            return (expected == actual).ToProperty();
        }

        [Property]
        public Property GivenAdvantageWhenOtherPlayerWinsThenScoreIsCorrect(Player advantagedPlayer)
        {
            var scoreWhenAdvantage = Scoring.ScoreWhenAdvantage(Player.Other(advantagedPlayer));
            var actual = scoreWhenAdvantage(new Score.Advantage(advantagedPlayer));

            var expected = new Score.Deuce();
            return (expected == actual).ToProperty();
        }

        [Property]
        public Property GivenPlayer40WhenPlayerWinsThenScoreIsCorrect(Score.Forty current)
        {
            var scoreWhenForty = Scoring.ScoreWhenForty(current.Player);
            var actual = scoreWhenForty(current);

            var expected = new Score.Game(current.Player);
            return (expected == actual).ToProperty();
        }

        [Property]
        public Property GivenPlayer40Other30WhenOtherWinsThenScoreIsCorrect(Score.Forty forty)
        {
            var current = forty with { OtherPlayerPoint = new Point.Thirty() };
            var scoreWhenForty = Scoring.ScoreWhenForty(Player.Other(current.Player));
            var actual = scoreWhenForty(current);

            var expected = new Score.Deuce();
            return (expected == actual).ToProperty();
        }

        [Property]
        public Property GivenPlayer40OtherSmaller30WhenOtherWinsThenScoreIsCorrect(Score.Forty forty)
        {
            var opp = Gen.Elements(new Point[] { new Point.Love(), new Point.Fifteen() }).ToArbitrary();
            return Prop.ForAll(opp, otherPlayerPoint =>
            {
                var current = forty with { OtherPlayerPoint = otherPlayerPoint };
                var scoreWhenForty = Scoring.ScoreWhenForty(Player.Other(current.Player));
                var actual = scoreWhenForty(current);
                var expected = Point.Increment(current.OtherPlayerPoint).AndThen(point => current with { OtherPlayerPoint = point } as Score);

                return (expected == actual).ToProperty();
            });
        }

        [Property]
        public Property ScoreReturnsAValue(Score current, Player winner)
        {
            var result = Scoring.Score(current, winner);

            return true.ToProperty();
        }
    }
}
