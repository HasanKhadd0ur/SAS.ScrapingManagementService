using SAS.ScrapingManagementService.SharedKernel.DomainEvents;
using System;
using System.Collections.Generic;

namespace SAS.ScrapingManagementService.SharedKernel.Entities
{
    /// <summary>
    /// The BaseEntity class serves as a foundation for all domain entities.
    /// </summary>
    public class BaseEntity<TId> : IEquatable<BaseEntity<TId>>
    {
        public TId Id { get; set; }

        /// <summary>
        /// Events List 
        /// </summary>
        public List<IDomainEvent> Events { get; private set; } = new List<IDomainEvent>();

        #region Domain Events Management 

        /// <summary>
        /// Add a domain event to the events list 
        /// </summary>
        /// <param name="eventItem">The event to be added</param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            Events.Add(eventItem);
        }

        /// <summary>
        ///  Clear the events list 
        /// </summary>
        public void ClearDomainEvents()
        {
            Events.Clear();
        }

        /// <summary>
        /// Remove a domain event from the list of domain events
        /// </summary>
        /// <param name="eventItem">The event to be removed</param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            Events.Remove(eventItem);
        }

        #endregion Domain Events Management

        #region Operators Overloading

        /// <summary>
        /// Equals operator to compare entities based on their IDs
        /// </summary>
        /// <param name="first">First entity</param>
        /// <param name="second">Second entity</param>
        /// <returns>true if entities have the same ID, otherwise false</returns>
        public static bool operator ==(BaseEntity<TId> first, BaseEntity<TId> second)
        {
            if (ReferenceEquals(first, null) && ReferenceEquals(second, null))
            {
                return true;
            }

            if (ReferenceEquals(first, null) || ReferenceEquals(second, null))
            {
                return false;
            }

            return first.Equals(second);
        }

        /// <summary>
        /// Not equals operator 
        /// </summary>
        /// <param name="first">First entity</param>
        /// <param name="second">Second entity</param>
        /// <returns>true if entities don't have the same ID, otherwise false</returns>
        public static bool operator !=(BaseEntity<TId> first, BaseEntity<TId> second)
        {
            return !(first == second);
        }

        /// <summary>
        /// Compares the entity with another entity based on their IDs
        /// </summary>
        /// <param name="other">Another entity</param>
        /// <returns>true if the entities have the same ID, otherwise false</returns>
        public bool Equals(BaseEntity<TId> other)
        {
            if (other is null || other.GetType() != GetType())
            {
                return false;
            }

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            return Equals(obj as BaseEntity<TId>);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TId>.Default.GetHashCode(Id);
        }

        #endregion Operators Overloading
    }
}
