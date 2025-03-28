using MediatR;

namespace Ordering.Domain.Abstractions
{
    public interface IDomainEvent : INotification
    {
        Guid Id => Guid.NewGuid();
        public DateTime OcurredOn => DateTime.Now;
        public string EventType => GetType().AssemblyQualifiedName;
    }
}
