﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavisPutnam.Model
{
    public class Clause
    {
        public HashSet<string> Elements { get; set; }
        public HashSet<string> Vocabulary
        {
            get
            {
                var toReturn = new List<string>(Elements);
                for (var i = 0; i < toReturn.Count; i++)
                {
                    toReturn[i] = toReturn[i].Replace("!", "");
                }
                return new HashSet<string>(toReturn);
            }
        }

        public Clause()
        {
            Elements = new HashSet<string>();
        }

        public void AddElement(string element)
        {
            Elements.Add(element);
        }

        public static Clause Concat(Clause a, Clause b)
        {
            var toReturn = new Clause();
            toReturn.Elements = new HashSet<string>(a.Elements);
            foreach (var s in b.Elements)
            {
                toReturn.AddElement(s);
            }
            return toReturn;
        }

        public bool Tautologia()
        {
            foreach (var s in Vocabulary)
            {
                if (Elements.Contains(s) && Elements.Contains("!" + s))
                {
                    return true;
                }
            }
            return false;
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
            foreach(var element in join.Elements)
            {
                if (element.Contains('!'))
                {
                    if (!Elements.Contains(element.Replace("!", "")))
                    {
                        toReturn.AddElement(element);
                    }
                }
                else
                {
                    if (!Elements.Contains("!" + element))
                    {
                        toReturn.AddElement(element);
                    }
                }
            }
            return toReturn;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append("{");
            foreach (var s in Elements)
            {
                builder.AppendFormat(" {0},", s);
            }
            builder.Append("}");
            builder.Replace(",}", "}"); 
            return builder.ToString();
        }
    }
}
