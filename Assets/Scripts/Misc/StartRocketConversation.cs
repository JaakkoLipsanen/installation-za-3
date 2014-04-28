using Assets.Scripts.General;
using Assets.Scripts.NPC;
using Flai;
using Flai.Scene;
using Flai.Scripts;
using Story;

namespace Assets.Scripts.Misc
{
	public class StartRocketConversation : Response
	{
        public override void Execute()
        {
            GenericEvent startFinalCutscene = () =>
            {
                Scene.Find("Player").renderer.enabled = false;
                Scene.Find("Emily").renderer.enabled = false;

                var rocket = Scene.Find("Rocket");
                rocket.Get<Rocket>().StartFlight();
            };

            Scene.Find("Emily").Get<EmilyAI>().LookAtJack();
            Scene.Find("Door To Rocket Response").Get<Door>().ExecuteOff();
            var speakers = new Speaker[2] { new Speaker("Jack"), new Speaker("Emily") };
            var conversation = new ConversationPiece[]
            {
                new ConversationPiece(1, "It's not broken!\n"), 
                new ConversationPiece(1, "Let's board it before\n" + 
                                         "more of those things come here!", startFinalCutscene), 
            };

            Conversation.Instance.StartConversation(speakers, new ConversationLog(conversation), null);
        }
	}
}