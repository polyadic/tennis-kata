using Funcky;

namespace TennisKata
{
    [DiscriminatedUnion]
    public abstract partial record Player
    {
        public static Player Other(Player player)
            => player.Match(
                playerOne: _ => new PlayerTwo() as Player,
                playerTwo: _ => new PlayerOne());

        public sealed partial record PlayerOne() : Player;

        public sealed partial record PlayerTwo() : Player;
    }
}
