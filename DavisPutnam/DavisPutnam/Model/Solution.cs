﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavisPutnam.Model
{
    public class Solution
    {
        public double Time { get; set; }
        public int Steps { get; set; }
        public Solution()
        {

        }
        
        public bool lsm(List<Clause> delta)
        {
            var result = new List<Clause>(delta);
            var finish = false;
            var count = 0;
            var limit = 10000;
            while (!finish && count < limit)
            {
                result = lss(delta, result);
                delta.AddRange(result);
                foreach (var s in delta)
                {
                    if (s.Elements.Count == 0)
                    {
                        finish = true;
                        break;
                    }
                }
                count++;
            }
            return finish;
        }

        public List<Clause> lss(List<Clause> delta, List<Clause> gama)
        {
            var result = new List<Clause>();
            foreach (var d in delta)
            {
                foreach (var g in gama)
                {
                    var temp = Clause.Concat(d, g);
                    result.Add(temp);
                }
            }
            return result;
        }

        public bool dp(List<Clause> delta)
        {
            var vocabulary = new HashSet<string>();
            Clause deltaPrima;
            foreach (var clause in delta)
            {
                vocabulary.Union(clause.Vocabulary);
            }
            foreach (var phi in vocabulary)
            {
                deltaPrima = new Clause();
                var gama1 = from element in delta
                            where element.Elements.Contains(phi)
                            select element;  //delta.All(x=>x.Elements.Contains(phi));
                var gama2 = from element in delta
                            where element.Elements.Contains("!" + phi)
                            select element;
                foreach (var g1 in gama1)
                {
                    foreach (var g2 in gama2)
                    {
                        var gamaPrima = g1.Join(g2);
                        if (!gamaPrima.Tautologia())
                        {
                            deltaPrima = deltaPrima.Join(gamaPrima);
                        }
                    }
                }
                delta.RemoveAll(x => gama1.Contains(x) || gama2.Contains(x));
                delta.Add(deltaPrima);
            }
            foreach (var x in delta)
            {
                if (x.Elements.Count == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
