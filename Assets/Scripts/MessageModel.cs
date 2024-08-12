using System;

namespace TestChat
{
    public class MessageModel
    {
        public string AvatarPath { get; }
        public string Message { get; }
        public string Nickname { get; }
        public DateTime Time { get; }
        public string MessageID { get; }
        public string UserID { get; }

        public MessageModel(string avatarPath, string message, string nickname, DateTime time, string messageID, string userID)
        {
            AvatarPath = avatarPath;
            Message = message;
            Nickname = nickname;
            Time = time;
            MessageID = messageID;
            UserID = userID;
        }
    }
}