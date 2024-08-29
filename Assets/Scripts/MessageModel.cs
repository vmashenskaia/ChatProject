using System;

namespace TestChat
{
    public class MessageModel
    { 
        public string Message { get; }
        public string Time { get; }
        public string MessageID { get; }

        public UserModel UserModel { get; }

        public MessageModel(string avatarPath, string message, string nickname, string time, string messageID, string userID)
        {
            Message = message;
            Time = time;
            MessageID = messageID;
            UserModel = new UserModel(avatarPath, nickname, userID);
        }
    }
}