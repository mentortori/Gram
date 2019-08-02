namespace Gram.Domain.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        byte[] RowVersion { get; set; }
    }
}
