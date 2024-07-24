using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestChat
{
    public class ApplicationController: MonoBehaviour
    {
        private void Start()
        {
            new ChatPresenter().LoadAndShowWindow(new List<MessageModel>()
            {
                new MessageModel("default", "Hello", "Viktoria", new DateTime(2024, 7, 1, 12, 0, 0), "22"),
                new MessageModel("default", "Hi", "Viktoria2", new DateTime(2024, 7, 1, 14, 0, 0), "23"),
                new MessageModel("default", "How are you? Text Text Text Text Text Text Text Text Text Text Text Text Text Textпше", "Viktoria2", new DateTime(2024, 7, 1, 14, 5, 0), "24"),
                new MessageModel("default", "Fine", "Viktoria", new DateTime(2024, 7, 2, 14, 0, 0), "25")
            });
        }
    }
}