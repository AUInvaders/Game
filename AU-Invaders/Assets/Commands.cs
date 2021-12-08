//using Backend.Models;

namespace Backend.Types
{
    public class Commands
    {
        public const string LOGIN = "Login";
        public const string GET_USER = "Get user";
        public const string REGISTER_USER = "Register";
        public const string FORGOT_PASSWORD = "Forgot password";
        public const string RESET_PASSWORD = "Reset password";
        public const string CHANGE_PASSWORD = "Change password";
        public const string CHANGE_EMAIL = "Change email";
        public const string ADD_GAME = "Add game";
        public const string GET_BEST_GAMES = "Get best games";
        public const string GET_ALL_GAMES = "Get all games from user";
        public const string GET_BEST_GAME = "Get best game";
        public const string GET_STORE_ITEMS = "Get store items";
        public const string GET_PURCHASED_ITEMS = "Get purchased items";
        public const string PURCHASE_ITEM = "Purchase Item";
        public const string GET_USER_COINS = "Get user coins";
        public const string GET_USER_ACCOUNT_EVENTS = "Get Useraccountevents";
    }
}