using System;

namespace Grocery.Domain.SeedWork
{
    /// <summary>
    /// The entity generic base class that takes the type used for identification.
    /// </summary>
    public abstract class Entity<T> : IEquatable<Entity<T>>
    {
        protected Entity(T id)
        {
            Id = id;
        }

        public T Id { get; }

        /// <summary>
        /// Overrides equality method to compare ID
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Entity<T> other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Entity<T> entity) { return Equals(entity); }

            return base.Equals(obj);
        }
    }
}