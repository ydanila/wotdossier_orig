﻿using System;
using System.Runtime.Serialization;

namespace WotDossier.Domain.Entities
{
    /// <summary>
    /// 	Base class for all entities connected with tables
    /// </summary>
    [DataContract]
    public abstract class EntityBase
    {
        protected EntityBase()
        {
        }

        /// <summary>
        /// Unique	Identifier of an entity
        /// </summary>
        [DataMember]
        public virtual Guid UId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 	Identifier of an entity
        /// </summary>
        [DataMember]
        public virtual int Id { get; set; }

        /// <summary>
        /// 	Gets/Sets version of current entity.
        /// </summary>
        [DataMember]
        public virtual int Version { get; set; }

        /// <summary>
        /// Gets or sets the rev.
        /// </summary>
        [DataMember]
        public virtual int Rev { get; set; }
    }
}