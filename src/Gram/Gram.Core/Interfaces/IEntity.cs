namespace Gram.Core.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        byte[] RowVersion { get; set; }
    }
}
