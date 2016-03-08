namespace HeroesOfFate.GameEngine
{
    public class ExceptionConstants
    {
        public const string InvalidNumberEnteredException = "Number  you entered is not Valid!.";

        public const string OutsideMapBoundariesException = "Outside map bounderies. Enter diffrent path.";

        public const string WallReachException = "There is no path at this direction. Enter Diffrent path.";

        public const string WrongDirectionException = "Please Enter valid direction.";

        public const string InvalidCommandException = "Invalid command. Enter new command.";

        public const string CharCreationException = "{0} is not Valid. Enter data again.";

        public const string InvalidItemException = "{0} does not exist at current time.";

        public const string NothingLootedException = "No luck with loot. Try harder next time.";

        public const string NullOrNegativeException = "{0} can`t be null or negative.";

        public const string LessThanException = "{0} can`t be less than {1}";

        public const string NullOrEmptyException = "{0} can`t be null or empty";

        public const string MissingException = "There is no such {0} in the game";

        public const string MovingMessage = "You moved {0} moves to the {1}";

        internal const string SomethingHappen = "You found {0} in your path and you {1}";
    }
}