using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EVEOver
{
    public class Structs
    {
        public static SystemData getSystemData(string system)
        {
            return JsonConvert.DeserializeObject<SystemData>(system);
        }

        public static ConstellationInfo getConstelData(string system)
        {
            return JsonConvert.DeserializeObject<ConstellationInfo>(system);
        }
    }
    public class SystemData
    {
        public string? constellation_id;
        public string? name;
        public string? security_class, security_status, star_id, system_id;
    }
    public class SystemInfo
    {
        public SystemData? data;
        public string? name;
    }

    public class Constellation
    {
        public List<string>? systems;
        public string? name;
    }

    public class ConstellationInfo
    {
        public List<string>? systems;
        public string? name;
    }

    public class Regions
    {
        public List<string>? regions;
        public Regions(string _regs)
        {
             regions = JsonConvert.DeserializeObject<List<string>>(_regs);
        }
    }

    public class RegionInfo
    {
        public List<string>? constellations;
        public string name;

        public RegionInfo(string _name) 
        {
            name = _name;
        }
    }

    public class Cache
    {
        public List<string> systems = new List<string>();
        public List<string> constellations = new List<string>();
    }

}
