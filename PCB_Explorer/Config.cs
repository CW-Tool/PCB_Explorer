using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;


namespace PCB_Explorer
{
    public class Item
    {
        public string name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
    }
    /*
    public class Items
    {
        public ArrayList items { get; set; }
    }*/

    class Config
    {
        public float scale { get; set; }
        public float scaleX { get; set; }
        public float scaleY { get; set; }
        public int offsetX { get; set; }
        public int offsetY { get; set; }
        public int[] pos { get; set; }
        public IList<Item> items { get; set; }

        public void Save(string filename)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.File.WriteAllText(filename, json);
        }
        public static Config Load(string filename)
        {
            string json = @"{ 
			    scale: 0.2, 
				scaleX: 1.0, 
				scaleY: 1.0, 
                offsetX: 0,
                offsetY: 0
				}";
            ;
            
            try
            {
                json = System.IO.File.ReadAllText(filename);
            } catch(Exception e) { }
            //// var json = File.ReadAllText(path); 
            /*var json = @"{ 
			    ServerAddress: null, 
				ServerPort: null, 
				ServerTimeout: null }";*/
            return JsonConvert.DeserializeObject<Config>(json);
        }
    }
}
