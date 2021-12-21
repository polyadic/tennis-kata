using TennisKata;

Score game = new Score.Points(new Point.Love(), new Point.Love());

var randomness = new Random();

Console.WriteLine(game);

while (game is not Score.Game)
{
    Player winner = PickWinner(randomness);
    game = Scoring.Score(game, winner);

    Console.WriteLine($"Winner: {winner}");
    Console.WriteLine(game);
}

static Player PickWinner(Random randomness)
{
    return randomness.Next() % 2 == 0 ? new Player.PlayerOne() : new Player.PlayerTwo();
}
