using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportLibrary.Shared.ModelBase
{
    public abstract class EntityObject
    {
        [Key]
        public int Id { get; protected set; }

        public bool Deleted { get; set; }

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
            unchecked
            {
                int hash = 17;
                return hash * 23 + Id.GetHashCode();
            }
        }

        public static bool operator ==(EntityObject a, EntityObject b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityObject a, EntityObject b)
        {
            return !(a == b);
        }
    }
}
