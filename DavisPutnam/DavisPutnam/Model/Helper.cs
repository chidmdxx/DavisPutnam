using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavisPutnam.Model
{
    public static class Helper
    {
        public static List<Clause> lsm(List<Clause> delta)
        {
            var result = new List<Clause>(delta);
            foreach(var d in delta)
            {
                result = lss(delta, result);
                delta.AddRange(result);
            }
            return result;
        }

        public static List<Clause> lss(List<Clause> delta, List<Clause> gama)
        {
            var result = new List<Clause>();
            foreach(var d in delta)
            {
                foreach(var g in gama)
                {
                    var temp = Clause.Concat(d, g);
                    result.Add(temp);
                }
            }
            return result;
        }

        public static dp(List<Clause> delta)
        {
            var vocabulary=new HashSet<string>();
            foreach(var clause in delta)
            {
                vocabulary.Concat(clause.Vocabulary);
            }
            foreach(var phi in vocabulary)
            {  
                var gama1=from element in delta 
                          where element.Elements.Contains(phi)
                          select element;  //delta.All(x=>x.Elements.Contains(phi));
                var gama2=from element in delta
                          where element.Elements.Contains("!"+phi)
                          select element;
                foreach(var g1 in gama1)
                {
                    foreach(var g2 in gama2)
                    {

                    }
                }
            }
        }

    }
}
