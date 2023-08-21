using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;
using Roleplay.Entities.Components;

namespace Roleplay.Models
{
    public class Region
    {
        public Guid Id = Guid.NewGuid();

        public string Name;

        public Vector3 StartPosition;

        public Vector3 EndPosition;

        public IList<IEntity> GetAllPropertyPartEntitiesInBound()
        {
            IEnumerable<Entity> entities = Entity.FindInBox(new BBox(StartPosition, EndPosition));
            IList<IEntity> entitiesWithComponent = new List<IEntity>();
            foreach (var entity in entities)
            {
                if (entity.Components.Get<PropertyPartComponent>() != null)
                {
                    entitiesWithComponent.Add(entity);
                }
            }
            return entitiesWithComponent;
        }
    }
}
