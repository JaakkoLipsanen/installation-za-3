using Flai;
using Flai.General;
using Flai.Input;
using UnityEngine;

namespace Story
{
    [ExecuteInEditMode]
    public class Conversation : Singleton<Conversation>
    {
        private Timer _characterTime = new Timer(0.15f);
        private TextMesh _conversationText;
        private TextMesh _speakerNameText;
        private MeshRenderer _speakerImageRenderer;

        private Speaker[] _speakers;
        private ConversationLog _conversationLog;
        private GenericEvent _conversationEndedAction;

        private int _conversationPieceIndex = -1;
        private int _conversationPieceCharacters = 0;

        private bool IsConversationOn
        {
            get { return _conversationLog != null; }
        }

        private ConversationPiece CurrentPiece
        {
            get { return _conversationLog[_conversationPieceIndex]; }
        }

        private bool IsPieceFinished
        {
            get { return _conversationPieceCharacters == this.CurrentPiece.Text.Length; }
        }

        protected override void Awake()
        {
            _conversationText = this.GetChild("ConversationText").Get<TextMesh>();
            _speakerNameText = this.GetChildRecursively("ConversationSpeakerName").Get<TextMesh>();
            _speakerImageRenderer = this.GetChild("ConversationSpeakerImage").Get<MeshRenderer>();

            this.Reset();
        }

        public bool StartConversation(Speaker[] speakers, ConversationLog conversationLog, GenericEvent onEnded)
        {
            if (this.IsConversationOn)
            {
                return false;
            }

            Ensure.True(speakers.Length > 0);
            this.Reset();

            _speakers = speakers;
            _conversationLog = conversationLog;
            _conversationEndedAction = onEnded;
            this.MoveToNextPiece();
            this.SetIsRendererEnabledRecursively(true);

            return true;
        }

        protected override void Update()
        {
            if (!this.IsConversationOn)
            {
                return;
            }

            if (!this.IsPieceFinished)
            {
                _characterTime.Update();
                if (_characterTime.HasFinished)
                {
                    _conversationPieceCharacters++;
                }
            }

            if (FlaiInput.IsNewKeyPress(KeyCode.E))
            {
                if (this.IsPieceFinished)
                {
                    if (_conversationLog.Count == _conversationPieceIndex + 1)
                    {
                        this.CurrentPiece.OnFinished.InvokeIfNotNull();
                        _conversationEndedAction.InvokeIfNotNull();
                        this.Reset();
                        return;
                    }

                    this.MoveToNextPiece();
                }
                else
                {
                    _conversationPieceCharacters = _conversationLog[_conversationPieceIndex].Text.Length - 1;
                }
            }

            _conversationText.text = this.CurrentPiece.Text.Substring(0, _conversationPieceCharacters);
        }

        protected override void LateUpdate()
        {
            this.AdjustSize();
        }

        private void AdjustSize()
        {
            var verticalSize = Camera.main.orthographicSize * 2;
            var horizontalSize = verticalSize * Screen.width / Screen.height;

            this.Scale2D = Vector2f.One * horizontalSize / 16;
        }

        private void Reset()
        {
            _speakers = null;
            _conversationLog = null;
            _conversationEndedAction = null;
            _conversationPieceCharacters = 0;
            _conversationPieceIndex = -1;

            this.SetIsRendererEnabledRecursively(false);
        }

        private void MoveToNextPiece()
        {
            if (_conversationPieceIndex != -1)
            {
                this.CurrentPiece.OnFinished.InvokeIfNotNull();
            }

            _conversationPieceIndex++;
            _conversationPieceCharacters = 0;

            _speakerNameText.text = _speakers[this.CurrentPiece.SpeakerIndex].Name;
            _speakerImageRenderer.material.mainTexture = this.LoadTexture(_speakers[this.CurrentPiece.SpeakerIndex]);
        }

        private Texture2D LoadTexture(Speaker speaker)
        {
            return Resources.Load<Texture2D>("Headshots/" + speaker.Name);
        }
    }
}
