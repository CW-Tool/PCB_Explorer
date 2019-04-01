using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Newtonsoft.Json;

//Open the console. " View" > "Other Windows" > "Package Manager Console"
//Then type the following: Install-Package Newtonsoft.Json.

namespace PCB_Explorer
{
    public class Contact
    {
        public float x { get; set; }
        public float y { get; set; }
        public string b { get; set; }
        public Contact(float _x, float _y, string _b)
        {
            x = _x;
            y = _y;
            b = _b;
        }
    }

    public class Item
    {
        public string name { get; set; }
        public IList<Contact> contacts { get; set; }
    }


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

            return JsonConvert.DeserializeObject<Config>(json);
        }
    }
}
