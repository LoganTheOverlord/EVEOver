using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hanssens.Net;
using System.Net.Http;
using System.Windows.Forms;

namespace EVEOver
{

    internal class Utils
    {
        public static Dictionary<string, SystemData> system_data = new Dictionary<string, SystemData>();
        public static LocalStorage req_manager = new LocalStorage(new LocalStorageConfiguration() { AutoSave = true, AutoLoad = true, Filename = "regions" });
        public static LocalStorage con_manager = new LocalStorage(new LocalStorageConfiguration() { AutoSave = true, AutoLoad = true, Filename = "constellations" });
        public static LocalStorage sys_manager = new LocalStorage(new LocalStorageConfiguration() { AutoSave = true, AutoLoad = true, Filename = "systems" });
        public static List<string> getSystemsFromConstellation(string constellation)
        {
            Constellation constel = JsonConvert.DeserializeObject<Constellation>(constellation);
            return constel.systems;
        }

        public static float getSystemTruesec(float min, string system)
        {

            return float.Parse(system_data[system].security_status);

        }

        public static string getSystemName(string system)
        {
            SystemInfo info = JsonConvert.DeserializeObject<SystemInfo>(system);


            return info.name;
        }

        public static Cache c = new Cache();

        public static int running = 0;
        public static int offThread = 0;

        public static async Task<bool> handleRegion(string region, bool exists)
        {
            RegionInfo _region;
            if (!exists)
            {
                _region = JsonConvert.DeserializeObject<RegionInfo>(await requestStringFromESI("universe/regions/" + region));
            }
            else
            {
                _region = req_manager.Get<RegionInfo>(region);
            }
            if (!exists) { running++; }
            Constellation _c;
            foreach (string _con in _region.constellations)
            {
                do
                {
                    if (!exists)
                    {
                        await Task.Delay(200);
                    }
                    _c = await processConstellation(_con);
                }
                while (_c == null);
                await Utils.constellationAwaiter(_c, exists);
            }
            req_manager.Store(region, _region);
            req_manager.Dispose();
            req_manager = new LocalStorage(new LocalStorageConfiguration() { AutoSave = true, AutoLoad = true, Filename = "regions" });
            progress++;
            running--;
            return true;
        }

        public static async Task<bool> constellationAwaiter(Constellation _c, bool exists)
        {
            if (_c != null)
            {
                SystemInfo _s;
                foreach (string _sys in _c.systems)
                {
                    do
                    {

                        await Task.Delay(100);
                        _s = await processSystem(_sys, _c.name);
                        
                    } while (_s == null && !exists);
                    if (exists)
                    {
                        processSystem(_sys, _c.name);
                    }
                }
            }
            return true;
        }

        public static int progress = 0;
        public static async Task<bool> initDatabase()
        {

            var regions = await requestStringFromESI("universe/regions/");
            Regions _rgs = new Regions(regions);
            List<string> defferred = new List<string>();
            foreach (string region in _rgs.regions)
            {
                Form1.instance.setProgress("Current Progress (Regions): " + progress + "/" + defferred.Count);
                bool found = true;
                if (!Utils.req_manager.Exists(region))
                {
                    found = false;
                    defferred.Add(region);
                }
                if (found)
                {
                    handleRegion(region, true);
                }
            }
            //getBestSystems();
            foreach (string region in defferred)
            {
                Form1.instance.setProgress("Current Progress (Regions): " + progress + "/" + defferred.Count);

                    await Task.Delay(2350);
                
                if (running < 5)
                {
                    handleRegion(region, false);
                }
            }
            return true;
        }

        

        public static void getBestSystems()
        {
            List<float> floats = new List<float>();
            List<SystemData> values = new List<SystemData>(system_data.Values);
            values.ForEach(f => { floats.Add(float.Parse(f.security_status)); });
            floats.Sort();
            SystemData s = values.Find(s => float.Parse(s.security_status) == floats[0]);
            Form1.instance.setLowestTruesec($"{s.name} [{s.security_status}]");
        }

        public static async Task<Constellation> processConstellation(string constel)
        {
            if (!con_manager.Exists(constel))
            {
                try
                {
                    ConstellationInfo _cin = Structs.getConstelData(await requestStringFromESI("universe/constellations/" + constel));
                    Constellation _c = new Constellation();
                    _c.systems = _cin.systems;
                    _c.name = _cin.name;
                    con_manager.Store(constel, _c);
                    con_manager.Dispose();
                    con_manager = new LocalStorage(new LocalStorageConfiguration() { AutoSave = true, AutoLoad = true, Filename = "constellations" });
                    Form1.instance.addTextToSystemsLoaded("Loaded constellation: " + _c.name);
                    return _c;
                } catch (Exception)
                {
                    throw;
                }
            } else
            {
                try
                {
                    Constellation _c = con_manager.Get<Constellation>(constel);
                    Form1.instance.addTextToSystemsLoaded("DB-Loaded constellation: " + _c.name);
                    return _c;
                } catch (Exception)
                {
                    throw;
                }
            }
        }

        public static async Task<RegionInfo> processRegion(string region)
        {
            if (!req_manager.Exists(region))
            {
                RegionInfo _region = JsonConvert.DeserializeObject<RegionInfo>(await requestStringFromESI("universe/regions/" + region));
                req_manager.Store(region, _region);
                req_manager.Dispose();
                req_manager = new LocalStorage(new LocalStorageConfiguration() { AutoSave = true, AutoLoad = true, Filename = "regions" });
                return _region;
            }
            else
            {
                RegionInfo _c = req_manager.Get<RegionInfo>(region);
                return _c;
            }
        }

        public static async Task<SystemInfo> processSystem(string system, string conName)
        {
            if (!sys_manager.Exists(system) && system.Length > 1)
            {
                try
                {
                    SystemData _data = Structs.getSystemData(await requestStringFromESI("universe/systems/" + system));
                    SystemInfo _si = new SystemInfo();
                    _si.data = _data;
                    _si.name = _data.name;
                    sys_manager.Store(system, _si);
                    system_data.Add(_si.name, _data);
                    sys_manager.Dispose();
                    sys_manager = new LocalStorage(new LocalStorageConfiguration() { AutoSave = true, AutoLoad = true, Filename = "systems" });
                    Form1.instance.addTextToSystemsLoaded("Loaded System: " + _si.name);
                    return _si;
                } catch (Exception)
                {
                    throw;
                }
            } else
            {
                try
                {
                    SystemInfo _s = sys_manager.Get<SystemInfo>(system);
                    system_data.Add(_s.data.name, _s.data);
                    Form1.instance.addTextToSystemsLoaded("Loaded System: " + _s.name);
                    //sec = sec.Remove(3);

                    return _s;
                } catch (Exception)
                {
                    throw;
                }
            }
        }

        public static async Task<string> requestStringFromESI(string endpoint)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://esi.evetech.net/latest/" + endpoint))
                {
                    var response = await httpClient.SendAsync(request);

                    if (!ReferenceEquals(response, null))
                    {

                        var json = await response.Content.ReadAsStringAsync();
                        return json;
                    }
                }
            }
            return null;
        }
    }
}
