namespace Gram.Application.SharedModels
{
    public class ListItemWithRowVersionModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
