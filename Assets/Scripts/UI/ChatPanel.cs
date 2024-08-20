using System.Collections.Generic;
using TMPro;

namespace UI
{
    public class ChatPanel
    {
        private readonly TextMeshProUGUI chatText;
        private readonly List<string> chatLines = new List<string>();
        private readonly int maxLines;
    
        public ChatPanel(TextMeshProUGUI chatText, int maxLines = 13)
        {
            this.chatText = chatText;
            this.maxLines = maxLines;
        }

        public void AddMessage(string message)
        {
            chatLines.Insert(0, message);
        
            if (chatLines.Count > maxLines)
            {
                chatLines.RemoveAt(chatLines.Count - 1);
            }
        
            UpdateChatText();
        }

        private void UpdateChatText()
        {
            chatText.text = string.Join("\n", chatLines);
        }
    }
}