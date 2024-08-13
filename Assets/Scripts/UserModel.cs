namespace TestChat
{
    public class UserModel
    {
        private string _avatarPath;
        private string _nikname;
        private string _userID;

        public UserModel(string avatarPath, string nikname, string userID)
        {
            _avatarPath = avatarPath;
            _nikname = nikname;
            _userID = userID;
        }
    }
}