namespace HeroesOfFate.GameEngine
{
    class ExceptionConstants
    {
        internal const string InvalidNumberEnteredException = "Number  you entered is not Valid!.";
        internal const string OutsideMapBoundariesException = "Outside map bounderies. Enter diffrent path.";
        internal const string WallReachException = "There is no path at this direction. Enter Diffrent path.";
        internal const string WrongDirectionException = "Please Enter valid direction.";
        internal const string InvalidCommandException = "Invalid command. Enter new command.";
        internal const string CharCreationException = "{0} is not Valid. Enter data again.";
        internal const string InvalidItemException = "{0} does not exist at current time.";
        internal const string NothingLootedException = "No luck with loot. Try harder next time.";
        internal const string NullOrNegativeException = "{0} can`t be null or negative.";
        internal const string LessThanException = "{0} can`t be less than {1}";
        internal const string NullOrEmptyException = "{0} can`t be null or empty";
        internal const string MissingException = "There is no such {0} in the game";
        internal const string MovingMessage = "You moved {0} moves to the {1}";
        internal const string SomethingHappen = "You found {0} in your path and you {1}";
    }
}
