using Sandbox;
using Sandbox.Engine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;

namespace Roleplay.Business
{
    public partial class BusinessRegistry : BaseNetworkable
    {
        public string BusinessRegistryName;

        [Net]
        public IList<Business> Businesses { get; set; } = new List<Business>();

        public BusinessRegistry(string businessRegistryName)
        {
            BusinessRegistryName = businessRegistryName;
        }

        public Business GetBusinessByOwnerId(long id)
        {
            foreach (Business business in Businesses)
            {
                if (business.OwnerId == id)
                {
                    return business;
                }
            }

            return null;
        }

        public static BusinessRegistry Deserialize(string businessRegistryData)
        {
            return JsonSerializer.Deserialize<BusinessRegistry>(businessRegistryData, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
        }

        public static BusinessRegistry Load(string businessRegistryName)
        {
            if (FileSystem.Data.FileExists(businessRegistryName + ".json"))
            {

                return Deserialize(FileSystem.Data.ReadAllText(businessRegistryName + ".json"));
            }
            else
            {
                BusinessRegistry businessRegistry = new(businessRegistryName);

                businessRegistry.Save();

                return businessRegistry;
            }
        }

        public string Serialize()
        {
            return JsonSerializer.Serialize<BusinessRegistry>(this, new JsonSerializerOptions { IncludeFields = true, WriteIndented = true });
        }

        public void Save()
        {
            Log.Info(this.Serialize());
            FileSystem.Data.WriteAllText(BusinessRegistryName + ".json", this.Serialize());
        }
    }
}