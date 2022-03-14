namespace Omnivus.Models
{
    public class ServiceBoxModel
    {
        public ServiceBoxModel()
        {
        }

        public ServiceBoxModel(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; set; }
        public string Description { get; set; }
    }
}
