using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace Roleplay.Map
{
    public enum PropertyType
    {
        House,
        Apartment
    }

    public enum PropertyState
    {
        Vacant,        // La propriété n'a pas encore de propriétaire
        OwnedOccupied, // La propriété est possédée et occupée par le propriétaire
        OwnedRented,   // La propriété est possédée et louée à un autre joueur
    }

    public class Property
    {
        public int Id { get; set; }
        public Guid RegionId { get; set; }
        public PropertyType Type { get; set; }

        public long OwnerId { get; set; }

        public PropertyState State { get; set; } = PropertyState.Vacant;

        public Property(int id, Guid regionId, PropertyType type)
        {
            Id = id;
            RegionId = regionId;
            Type = type;
        }

        public IList<IEntity> GetAllPropertyPartEntities()
        {

        }
    }
}
