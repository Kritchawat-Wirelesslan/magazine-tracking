using System;
using System.Collections.Generic;
using System.Text;

namespace MagazineTracking.Core.DomainModels.Base
{
    public abstract class EntityBase<TKey> 
    {
        #region Fields

        protected TKey id;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor.
        /// </summary>
        protected EntityBase()
        {

        }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        /// <param name="key">An <see cref="System.Object"/> that 
        /// represents the primary identifier value for the 
        /// class.</param>
        protected EntityBase(TKey key)
        {
            this.id = key;
        }

        #endregion

        #region Properties

        /// <summary>
        /// An <see cref="System.Object"/> that represents the 
        /// primary identifier value for the class.
        /// </summary>
        public virtual TKey Id
        {
            get
            {
                return this.id;
            }
            protected set
            {
                this.id = value;
            }
        }

        public virtual DateTimeOffset CreatedDate { get; set; }

        public virtual DateTimeOffset UpdatedDate { get; set; }

        #endregion

        #region Equality Tests

        /// <summary>
        /// Determines whether the specified entity is equal to the 
        /// current instance.
        /// </summary>
        /// <param name="entity">An <see cref="System.Object"/> that 
        /// will be compared to the current instance.</param>
        /// <returns>True if the passed in entity is equal to the 
        /// current instance.</returns>
        public override bool Equals(object entity)
        {
            return entity != null
                && entity is EntityBase<TKey>
                && this == (EntityBase<TKey>)entity;
        }

        /// <summary>
        /// Operator overload for determining equality.
        /// </summary>
        /// <param name="base1">The first instance of an 
        /// <see cref="EntityBase"/>.</param>
        /// <param name="base2">The second instance of an 
        /// <see cref="EntityBase"/>.</param>
        /// <returns>True if equal.</returns>
        public static bool operator ==(EntityBase<TKey> base1,
            EntityBase<TKey> base2)
        {
            // check for both null (cast to object or recursive loop)
            if ((object)base1 == null && (object)base2 == null)
            {
                return true;
            }

            // check for either of them == to null
            if ((object)base1 == null || (object)base2 == null)
            {
                return false;
            }

            if (!base1.Id.Equals(base2.Id))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Operator overload for determining inequality.
        /// </summary>
        /// <param name="base1">The first instance of an 
        /// <see cref="EntityBase"/>.</param>
        /// <param name="base2">The second instance of an 
        /// <see cref="EntityBase"/>.</param>
        /// <returns>True if not equal.</returns>
        public static bool operator !=(EntityBase<TKey> base1,
            EntityBase<TKey> base2)
        {
            return (!(base1 == base2));
        }

        /// <summary>
        /// Serves as a hash function for this type.
        /// </summary>
        /// <returns>A hash code for the current Key 
        /// property.</returns>
        public override int GetHashCode()
        {
            if (this.id != null)
            {
                return this.id.GetHashCode();
            }
            else
            {
                return 0;
            }
        }

        #endregion
    }
}