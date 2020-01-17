﻿using Develover.Core.Entities;
using System;
using System.Data.Entity.Core.Objects;

namespace Develover.Core.Extensions
{
    public static class EntityExtensions
    {/// <summary>
     /// Get unproxied entity type
     /// </summary>
     /// <remarks> If your Entity Framework context is proxy-enabled, 
     /// the runtime will create a proxy instance of your entities, 
     /// i.e. a dynamically generated class which inherits from your entity class 
     /// and overrides its virtual properties by inserting specific code useful for example 
     /// for tracking changes and lazy loading.
     /// </remarks>
     /// <param name="entity"></param>
     /// <returns></returns>
        public static Type GetUnproxiedEntityType(this BaseEntity entity)
        {
            var userType = ObjectContext.GetObjectType(entity.GetType());
            return userType;
        }
    }
}