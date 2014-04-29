using Assets.Scripts.General;
using Assets.Scripts.NPC;
using Flai;
using Flai.Scene;
using Flai.Scripts;
using Story;

namespace Assets.Scripts.Misc
{
    public class StartFinalComputerConversation : Response
    {
        public override void Execute()
        {
            GenericEvent openDoorEvent = () =>
            {
                var doorGO = Scene.Find("Door To Break Room");
                var door = doorGO.Get<Door>();
                door.Execute();
            };

            GenericEvent openDoorToRocketEvent = () =>
            {
                var doorGO = Scene.Find("Door To Rocket Response");
                var door = doorGO.Get<Door>();
                Scene.Find("Emily").Get<EmilyAI>().StartRunning();
                door.Execute();
            };

            var speakers = new Speaker[2] { new Speaker("Jack"), new Speaker("Emily") };
            var conversation = new ConversationPiece[]
            {
                new ConversationPiece(0, "Emily? Are you there?\n" + 
                                         "Open the door"), 
        
                new ConversationPiece(1, "Jack? Jack!\n" + 
                                         "You are finally here, thank god!", openDoorEvent), 
  
                new ConversationPiece(0, "Yeah.. it wasn't easy,\n" + 
                                         "but I guess I somehow made it"), 

                new ConversationPiece(0, "Are you okay?"),
                new ConversationPiece(1, "Yeah.. I'm.. I'm fine"),
                                         
                new ConversationPiece(0, "I don't think anyone can be \"fine\"\n" + 
                                         "in this situation, but glad to hear"),

                new ConversationPiece(0, "Do you know what happened\n" + 
                                         "to Catalina? Did she make it?"),
                                  
                new ConversationPiece(1, "I.. I saw her body from the window.."),
                new ConversationPiece(0, "Damn.. So it's just me and you.\n"),
                new ConversationPiece(0, "We need to get away from here.\n" + 
                                         "Is the landing rocket still working?"),
   
                new ConversationPiece(1, "I'm not sure..."),

                new ConversationPiece(1, "But it's our only way to get out, \n" + 
                                         "so we better hope that it's working"),
                
                new ConversationPiece(1, "Let's go check it.\n" + 
                                         "Follow me", openDoorToRocketEvent),
            };

            Conversation.Instance.StartConversation(speakers, new ConversationLog(conversation), null);
        }
    }
}