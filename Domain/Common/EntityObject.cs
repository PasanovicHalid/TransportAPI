using System.ComponentModel.DataAnnotations;

namespace Domain.Common
{
    public abstract class EntityObject
    {
        [Key]
        public ulong Id { get; protected set; }

        public bool Deleted { get; set; }

        protected EntityObject()
        {
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            var entity = (EntityObject)obj;

            return Id == entity.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(EntityObject? a, EntityObject? b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityObject? a, EntityObject? b)
        {
            return !(a == b);
        }
    }
}
