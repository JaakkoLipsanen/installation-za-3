using Assets.Scripts.General;
using Flai.Scene;
using Flai.Scripts;
using Story;

namespace Assets.Scripts.Misc
{
    public class StartFirstComputerConversation : Response
    {
        public override void Execute()
        {
            Scene.Find("PirisijaAlku").DestroyIfNotNull();
            Scene.Find("Help & Instructions").DestroyIfNotNull();
            var speakers = new Speaker[2] { new Speaker("Jack"), new Speaker("Emily") };
            var conversation = new ConversationPiece[]
            {
                new ConversationPiece(0, "Hey, it's Jack"), 
                new ConversationPiece(1, "Jack? Jack?!?\n" +
                                         "Can you hear me?!"), 
                new ConversationPiece(0, "Yeah I can hear you, Emily!\nWhat's wrong?!"), 

                new ConversationPiece(1, "Something.. something is out there!\n" +
                                         "They attacked Catalina and Ben!"), 
            
                new ConversationPiece(0, "What do you mean?\nSomething is out there?"),                   
                new ConversationPiece(0, "We're the only ones on this planet!"), 
              
                new ConversationPiece(1, "I don't know...\nSomething came under the ground.."), 
                new ConversationPiece(1, "Oh my god Jack!\nThey just killed Ben!"), 
                new ConversationPiece(1, "They are eating him!!\nThey're going to kill me!!"),          
                new ConversationPiece(1, "What do I do Jack??\nWhat do I do?!?!"),
 
                new ConversationPiece(0, "Okay... Holy..\nUmm.. umm... ummm.."), 
                new ConversationPiece(0, "We should be together.\nIt's safer that way"), 
                new ConversationPiece(0, "Where are you?\nAre you still at the break room?"),
 
                new ConversationPiece(1, "Yeah I'm at the break room!\nI managed to lock the door."),
                new ConversationPiece(1, "I'm not sure how long\nit will last though.."),
                new ConversationPiece(1, "Those things look strong..."),  
            
                new ConversationPiece(1, "Come here as fast as you can\n"), 
                new ConversationPiece(1, "You must go outside to reach\n" +
                                         "this building, so be careful"),
         
                new ConversationPiece(1, "You should also lock the all\n" +
                                         "the doors on the way"),
                                         
                new ConversationPiece(1, "There are already some of \n" +
                                         "those things inside the base"),
                                     
                new ConversationPiece(1, "Locking the doors should\n" +
                                         "keep rest of them out."),

                new ConversationPiece(1, "You still have your pistol, right?\n"),
                new ConversationPiece(1, "It's enough for now but try to\n" +
                                         "find something stronger if you can"),
            };

            Conversation.Instance.StartConversation(speakers, new ConversationLog(conversation),
                () => Scene.Find("FirstDoor").GetComponentInChildren<Door>().Execute());
        }
    }
}