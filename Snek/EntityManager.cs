using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snek
{
    class EntityManager
    {
        Dictionary<Guid, Entity> Entities;

        public void AddEntity(Entity entity)
        {
            Entities[entity.Identity] = entity;
        }

        public bool RemoveEntity(Guid identity)
        {
            return Entities.Remove(identity);
        }

        public IEnumerable<Entity> GetEntitiesWithComponentIDs(params int[] componentIDs)
        {
            foreach (Entity entity in Entities.Values)
            {
                bool hasAllComponents = true;
                foreach (int componentID in componentIDs)
                {
                    if (!entity.HasComponent(componentID))
                    {
                        hasAllComponents = false;
                        break;
                    }
                }

                if (hasAllComponents)
                {
                    yield return entity;
                }
            }
        }
    }
}
