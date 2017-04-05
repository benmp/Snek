using Snek.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snek
{
    class Entity
    {
        public readonly Guid Identity;
        private List<Component> _components;

        public Entity(Guid identity)
        {
            Identity = identity;
            _components = new List<Component>();
        }

        public bool AddComponent(Component component)
        {
            if (_components.Count <= component.ID)
            {
                for (int i = _components.Count; i <= component.ID; i++)
                {
                    _components[i] = null;
                }
            }

            if (_components[component.ID] != null)
            { //don't allow adding of duplicate component for now
                return false;
            }

            _components[component.ID] = component;
            return true;
        }

        public bool HasComponent(int id)
        {
            return _components.Count > id && _components[id] != null;
        }

        public Component GetComponent(int id)
        {
            return _components[id];
        }

        public void RemoveComponent(int id)
        {
            _components[id] = null;
        }
    }
}
