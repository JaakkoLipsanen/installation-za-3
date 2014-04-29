using Flai;
using Flai.Scripts;

namespace Assets.Scripts.Enemy
{
    public class Wave : Response
    {
        public override void Execute()
        {
            foreach (Response response in this.GetComponentsInChildren<Response>().ToArray())
            {
                if (response != null && response != this)
                {
                    response.Execute();
                }
            }
        }
    }
}