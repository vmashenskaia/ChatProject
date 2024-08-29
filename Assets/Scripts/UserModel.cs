namespace TestChat
{
    public class UserModel
    {
        public string AvatarPath { get; }
        public string Nikname { get; }
        public string UserID { get; }

        public UserModel(string avatarPath, string nikname, string userID)
        {
            AvatarPath = avatarPath;
            Nikname = nikname;
            UserID = userID;
        }
    }
}