using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavisPutnam.Model
{
    public class Solution
    {
        public long Time { get; set; }
        public int Steps { get; set; }
        public List<Clause> Delta { get; set; }
        public Solution()
        {

        }

        public bool lsm(List<Clause> deltaI)
        {
            var delta = new List<Clause>(deltaI);
            var stopWatch = new Stopwatch();
            var result = new List<Clause>(delta);
            var finish = false;
            Steps = 0;
            var limit = 10000;
            stopWatch.Start();
            while (!finish && limit != 0)
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
                limit--;
            }
            stopWatch.Stop();
            Delta = delta;
            Time = stopWatch.ElapsedMilliseconds;
            return !finish;
        }

        public List<Clause> lss(List<Clause> delta, List<Clause> gama)
        {
            var result = new List<Clause>();
            foreach (var d in delta)
            {
                foreach (var g in gama)
                {
                    var temp = d.Join(g);
                    result.Add(temp);
                    Steps++;
                }
            }
            return result;
        }

        public bool dp(List<Clause> deltaI)
        {
            var delta = new List<Clause>(deltaI);
            Delta = new List<Clause>(deltaI);
            var stopWatch = new Stopwatch();
            Steps = 0;
            stopWatch.Start();
            var vocabulary = new HashSet<string>();
            Clause deltaPrima;
            foreach (var clause in delta)
            {
                vocabulary.UnionWith(clause.Vocabulary);
            }
            foreach (var phi in vocabulary)
            {
                deltaPrima = new Clause();
                bool trabajo = false;
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
                        trabajo = true;
                        var gamaPrima = Clause.Concat(g1,g2);
                        gamaPrima.Elements.Remove(phi);
                        gamaPrima.Elements.Remove("!" + phi);
                        if (!gamaPrima.Tautologia())
                        {
                            Delta.Add(gamaPrima);
                            deltaPrima = Clause.Concat(deltaPrima,gamaPrima);                            
                        }
                        Steps++;
                    }
                }
                delta.RemoveAll(x => gama1.Contains(x) || gama2.Contains(x));
                if (trabajo)
                {
                    delta.Add(deltaPrima);
                    Delta.Add(deltaPrima);
                }
            }
            foreach (var x in delta)
            {
                if (x.Elements.Count == 0)
                {
                    stopWatch.Stop();
                    Time = stopWatch.ElapsedMilliseconds;
                    return false;
                }
            }
            stopWatch.Stop();
            Time = stopWatch.ElapsedMilliseconds;
            return true;
        }
    }
}
