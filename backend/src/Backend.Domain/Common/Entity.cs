namespace Backend.Domain.Common;

public abstract class Entity {
    protected Entity(Guid id) {
        Id = id;
    }

    protected Entity() {
    }

    public Guid Id { get; }

    public override bool Equals(object? obj) {
        if (obj is null || obj.GetType() != GetType()) return false;

        return ((Entity)obj).Id == Id;
    }

    public override int GetHashCode() {
        return Id.GetHashCode();
    }
}
