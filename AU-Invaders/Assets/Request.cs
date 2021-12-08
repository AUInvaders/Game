using Newtonsoft.Json;
namespace Backend.Types
{
    public class Request
    {
        
        public string Command;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}