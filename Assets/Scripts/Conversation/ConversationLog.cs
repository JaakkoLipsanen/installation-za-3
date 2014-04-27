
using System;
using Flai;

namespace Story
{

    public class ConversationLog
    {
        private readonly ConversationPiece[] _conversationPieces;
        public int Count
        {
            get { return _conversationPieces.Length; }
        }

        public ConversationLog(ConversationPiece[] conversations)
        {
            _conversationPieces = conversations;
        }

        public ConversationPiece this[int index]
        {
            get { return _conversationPieces[index]; }
        }
    }

    public class ConversationPiece
    {
        public readonly int SpeakerIndex; // in theory 0 or 1 in Jaya
        public readonly string Text;
        public readonly GenericEvent OnFinished;

        public ConversationPiece(int speaker, string text, GenericEvent onFinished = null)
        {
            this.SpeakerIndex = speaker;
            this.Text = text;
            this.OnFinished = onFinished;
        }
    }
}
