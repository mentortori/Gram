namespace Gram.Application.EventGuides.Models
{
    public class EventGuideModel
    {
        public int Id { get; set; }
        public byte[] RowVersion { get; set; }
        public string Name { get; set; }
    }
}
