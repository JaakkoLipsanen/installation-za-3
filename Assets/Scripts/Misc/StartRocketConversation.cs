using System.ComponentModel;
using Assets.Scripts.Enemy;
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
            var rocket = Scene.Find("Rocket");
            var emily = Scene.Find("Emily");
            GenericEvent startWaves = () =>
            {
                emily.Get<EmilyAI>().FixRocket();

                var waveGO = Scene.Find("Final Wave");
                var wave = waveGO.Get<FinalWave>();
                wave.StartWave();

                wave.Midtime += this.StartMidtimeConversation;

                wave.Ended += () =>
                {
                    this.StartEnterRocketConversation();
                    rocket.Get<Rocket>().SetEnterable();
                };
            };

            emily.Get<EmilyAI>().LookAtJack();
            Scene.Find("Door To Rocket Response").Get<Door>().ExecuteOff();
            var speakers = new Speaker[2] { new Speaker("Jack"), new Speaker("Emily") };
            var conversation = new ConversationPiece[]
            {
                new ConversationPiece(1, "Damnit! It's broken\n"), 
                new ConversationPiece(1, "It will take a while to repair it.\n" + 
                                         "Keep me safe while I'll fix it", startWaves), 
            };

            Conversation.Instance.StartConversation(speakers, new ConversationLog(conversation), null);
        }

	    private void StartMidtimeConversation()
        {
            var speakers = new Speaker[2] { new Speaker("Jack"), new Speaker("Emily") };
            var conversation = new ConversationPiece[]
            {
                new ConversationPiece(1, "Just a little while longer Jack!"),
            };

            Conversation.Instance.StartConversation(speakers, new ConversationLog(conversation), null);
	    }

	    private void StartEnterRocketConversation()
	    {
            var rocket = Scene.Find("Rocket");
            var speakers = new Speaker[2] { new Speaker("Jack"), new Speaker("Emily") };
            var conversation = new ConversationPiece[]
            {
                new ConversationPiece(1, "Jack! Jack! I did it!\n" + 
                                         "The rocket is working now!"), 

                new ConversationPiece(1, "Come here!\n" +
                                         "Let's leave this place!"), 
            };

            Conversation.Instance.StartConversation(speakers, new ConversationLog(conversation),  () => rocket.Get<Rocket>().SetEnterable());
	    }
	}
}