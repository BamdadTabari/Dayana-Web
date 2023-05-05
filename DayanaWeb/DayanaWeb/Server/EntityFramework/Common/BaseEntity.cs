namespace DayanaWeb.Server.EntityFramework.Common;

public interface IBaseEntity { }
public abstract class BaseEntity<T> : IBaseEntity
{
    public T Id { get; set; }
    public DateTime ModifiedDate { get; set; }
    public DateTime CreateDate { get; set; }
}
public abstract class BaseEntity : BaseEntity<long> { }