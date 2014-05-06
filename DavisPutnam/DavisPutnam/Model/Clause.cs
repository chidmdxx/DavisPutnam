using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavisPutnam.Model
{
    class Clause
    {
        public List<string> Elements { get; set; }

        public Clause() { }

        public void AddElement(string element)
        {
            if (!Elements.Contains(element))
            {
                Elements.Add(element);
            }
        }

        public static Clause Concat(Clause a, Clause b)
        {
            var toReturn = new Clause();
            toReturn.Elements = new List<string>(a.Elements);
            foreach(var s in b.Elements)
            {
                toReturn.AddElement(s);
            }
            return toReturn;
        }

        public Clause Join(Clause join)
        {
            var toReturn = new Clause();
            foreach (var element in Elements)
            {
                if (element.Contains('!'))
                {
                    if (!join.Elements.Contains(element.Replace("!", "")))
                    {
                        toReturn.AddElement(element);
                    }
                }
                else
                {
                    if (!join.Elements.Contains("!" + element))
                    {
                        toReturn.AddElement(element);
                    }
                }
            }
            return toReturn;
        }
    }
}
