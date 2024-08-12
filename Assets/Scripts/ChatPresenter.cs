using System.Collections.Generic;
using UnityEngine;

namespace TestChat
{
    public class ChatPresenter
    {
        private MessageScreenView _view;
        
        public void LoadAndShowWindow(List<MessageModel> messages)
        {
            _view = GameObject.FindFirstObjectByType<MessageScreenView>();
            _view.ApplyMessages(messages);
        }
    }
}