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

        public MessageModel(string avatarPath, string message, string nickname, DateTime time, string messageID)
        {
            AvatarPath = avatarPath;
            Message = message;
            Nickname = nickname;
            Time = time;
            MessageID = messageID;
        }
    }
}