using Assets.Scripts.General;
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
            var speakers = new Speaker[2] { new Speaker("Jack"), new Speaker("Emily") };
            var conversation = new ConversationPiece[]
            {
                new ConversationPiece(0, "Emily? Are you there?\n" + 
                                         "Open the door"), 
        
                new ConversationPiece(1, "Jack? Jack!\n" + 
                                         "You are finally here, thank god!", openDoorEvent), 

                
                new ConversationPiece(0, "Yeah.. it wasn't easy,\n" + 
                                         "but I guess I somehow made it"), 

                                         
                new ConversationPiece(0, "Okay let's get to the rocket\n" + "" +
                                         "and live our lives happily ever after"), 
            };

            Conversation.Instance.StartConversation(speakers, new ConversationLog(conversation), null);
	    }
	}
}