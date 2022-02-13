using System.ComponentModel.DataAnnotations;

namespace WebApi.HttpRequests
{
    public class AddUserHttpRequest
    {
        [MaxLength(64)]
        public string Name { get; set; }
    }
}