using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DavisPutnam.Model
{
    class Transformar
    {
        public List<Clause> Resultado { get; set; }
        public string Enunciado { get; set; }
        public Transformar() { }

        public void Work()
        {
            Enunciado = Enunciado.Replace(" ", "");
            ImplicationsOut();
            ReductionsOut();
            NegationsIn();
            Distribution();
            OperatorsOut();
        }

        private void ImplicationsOut()
        {
            int index = 0;
            int indexAuxiliar = 0;
            int siguienteParentesis;
            while (true)
            {
                index = Enunciado.IndexOf("=>");
                if (index == -1)
                {
                    break;
                }
                Enunciado = Enunciado.Remove(index, 2);
                Enunciado = Enunciado.Insert(index, "|");
                if (Enunciado[index - 1] == ')')
                {
                    siguienteParentesis = index - 1;
                    indexAuxiliar = index;
                    while (true)
                    {
                        indexAuxiliar = Enunciado.LastIndexOf("(", indexAuxiliar - 1);
                        siguienteParentesis = Enunciado.LastIndexOf(")", siguienteParentesis - 1);
                        if (siguienteParentesis == -1 || siguienteParentesis < indexAuxiliar)
                        {
                            break;
                        }
                    }
                    Enunciado = Enunciado.Insert(indexAuxiliar, "!");
                }
                else
                {
                    indexAuxiliar = index - 1;
                    while (Enunciado[indexAuxiliar] != '&' &&
                        Enunciado[indexAuxiliar] != '|' &&
                        Enunciado[indexAuxiliar] != '>' &&
                        Enunciado[indexAuxiliar] != '=' &&
                        indexAuxiliar > 0)
                    {
                        indexAuxiliar--;
                    }
                    Enunciado = Enunciado.Insert(indexAuxiliar, "!");
                }
            }
        }
        private void ReductionsOut()
        {
            int index = 0;
            while (true)
            {
                index = Enunciado.IndexOf("<=");
                if (index == -1)
                {
                    break;
                }

                Enunciado = Enunciado.Remove(index, 2);
                Enunciado = Enunciado.Insert(index, "|");
                Enunciado = Enunciado.Insert(index + 1, "!");
            }
        }
        private void NegationsIn()
        {
            int index = -1;
            int indexAuxiliar = 0;
            int siguienteParentesis;
            int numeroDeParentesis;
            while (true)
            {

                numeroDeParentesis = 0;
                index = Enunciado.IndexOf("!", index + 1);
                if (index == -1)
                {
                    break;
                }
                if (Enunciado[index + 1] == '(')
                {
                    siguienteParentesis = index + 1;
                    indexAuxiliar = index;
                    while (true)
                    {
                        indexAuxiliar = Enunciado.IndexOf(")", indexAuxiliar + 1);
                        siguienteParentesis = Enunciado.IndexOf("(", siguienteParentesis + 1);
                        if (siguienteParentesis == -1 || siguienteParentesis > indexAuxiliar)
                        {
                            break;
                        }
                        numeroDeParentesis++;
                    }
                    while (indexAuxiliar > index)
                    {
                        if (Enunciado[indexAuxiliar] == '&' || Enunciado[indexAuxiliar] == '|')
                        {
                            if (Enunciado[indexAuxiliar + 1] != '(')
                            {
                                Enunciado = Enunciado.Insert(indexAuxiliar + 1, "!");
                            }
                            if (Enunciado[indexAuxiliar] == '&')
                            {
                                Enunciado = Enunciado.ReplaceAt(indexAuxiliar, '|');
                            }
                            else if (Enunciado[indexAuxiliar] == '|')
                            {
                                Enunciado = Enunciado.ReplaceAt(indexAuxiliar, '&');
                            }
                        }
                        if (Enunciado[indexAuxiliar] == '(' && Enunciado[indexAuxiliar + 1] != '(')
                        {
                            Enunciado = Enunciado.Insert(indexAuxiliar + 1, "!");
                        }
                        indexAuxiliar--;
                    }
                    Enunciado = Enunciado.Remove(index, 1);
                }
            }
            Enunciado = Enunciado.Replace("!!", "");
        }

        private void Distribution()
        {
            while (true)
            {
                var inicioParentesis = -1;
                var finParentesis = -1;
                var siguienteParentesis = -1;
                var indiceElementoAnterior = -1;
                var indiceElementoSiguiente = -1;
                while (true)
                {
                    inicioParentesis = Enunciado.IndexOf('(', inicioParentesis + 1);
                    siguienteParentesis = Enunciado.IndexOf('(', inicioParentesis + 1);
                    finParentesis = Enunciado.IndexOf(')', finParentesis + 1);
                    if (siguienteParentesis == -1 || siguienteParentesis > finParentesis)
                    {
                        break;
                    }
                }
                if (inicioParentesis == -1)
                {
                    break;
                }
                indiceElementoAnterior = inicioParentesis - 1;
                indiceElementoSiguiente = finParentesis + 1;
                do
                {
                    indiceElementoAnterior--;
                } while (Enunciado[indiceElementoAnterior] != '&' && Enunciado[indiceElementoAnterior] != '|');
                do
                {
                    indiceElementoSiguiente++;
                } while (Enunciado[indiceElementoAnterior] != '&' && Enunciado[indiceElementoAnterior] != '|');
                var subEnunciado = Enunciado.Substring(indiceElementoAnterior, indiceElementoSiguiente - indiceElementoAnterior);
                Enunciado = Enunciado.Replace(subEnunciado, HacerDistribucion(subEnunciado));
            }
        }

        private string HacerDistribucion(string enunciado)
        {
            string toReturn = enunciado.Replace('(', '[').Replace(')', ']');
            var inicioParentesis = enunciado.IndexOf('(');
            var finParentesis = enunciado.IndexOf(')');
            var dentroParentesis = enunciado.Substring(inicioParentesis, finParentesis - inicioParentesis).Trim('(', ')');
            var operadorAntes = enunciado[inicioParentesis - 1];
            var operadorDespues = enunciado[finParentesis + 1];
            char operadorDentro;
            string elementoAntes = enunciado.Substring(0, inicioParentesis - 1).Trim(operadorAntes);
            string elementoDespues = enunciado.Substring(finParentesis + 1).Trim(operadorDespues);
            bool mismoAdentro = (dentroParentesis.Contains('&') && !dentroParentesis.Contains('|')) ||
                (dentroParentesis.Contains('|') && !dentroParentesis.Contains('&'));
            if (mismoAdentro)
            {
                var builder = new StringBuilder();
                operadorDentro = dentroParentesis.Contains('&') ? '&' : '|';
                if (operadorDentro == '&' && operadorAntes == '|')
                {
                    builder.Append('(');
                    var allElementos = dentroParentesis.Split(operadorDentro);
                    foreach (var s in allElementos)
                    {
                        builder.AppendFormat("{0}|{1}&", elementoAntes, s);
                    }
                    builder.Append(')');
                    builder.Replace("&)", ")");
                    builder.AppendFormat("{0}{1}", operadorDespues, elementoDespues);
                }
                else if (operadorDentro == '&' && operadorDespues == '|')
                {
                    builder.AppendFormat("{0}{1}", elementoAntes, operadorAntes);
                    builder.Append('(');
                    var allElementos = dentroParentesis.Split(operadorDentro);
                    foreach (var s in allElementos)
                    {
                        builder.AppendFormat("{0}|{1}&", elementoDespues, s);
                    }
                    builder.Append(')');
                    builder.Replace("&)", ")");
                }
                else if (operadorAntes == operadorDentro && operadorDentro == operadorDespues)
                {
                    toReturn.Replace("[", "").Replace("]", "");
                    builder.Append(toReturn);
                }
                else if (operadorAntes == operadorDentro)
                {
                    builder.Append('(');
                    builder.AppendFormat("{0}{1}", elementoAntes, operadorAntes);
                    var allElementos = dentroParentesis.Split(operadorDentro);
                    foreach (var s in allElementos)
                    {
                        builder.AppendFormat("{0}{1}", s, operadorDentro);
                    }
                    builder.Append(')');
                    builder.Replace(operadorDentro + ")", ")");
                    builder.AppendFormat("{0}{1}", operadorDespues, elementoDespues);
                }
                else if(operadorDespues==operadorDentro)
                {
                    builder.AppendFormat("{0}{1}", elementoAntes, operadorAntes);
                    builder.Append('(');
                    var allElementos = dentroParentesis.Split(operadorDentro);
                    foreach (var s in allElementos)
                    {
                        builder.AppendFormat("{0}{1}", s, operadorDentro);
                    }
                    builder.Append(elementoDespues);
                    builder.Append(')');
                }
                else
                {
                    builder.Append(toReturn);
                }
                toReturn = builder.ToString();
            }

            return toReturn;
        }

        private void OperatorsOut()
        {
            Resultado = new List<Clause>();
            var enunciadoSplit = Enunciado.Split('&', '[', ']');
            foreach (var clause in enunciadoSplit)
            {
                var elementos = clause.Split('|');
                Clause temp=new Clause();
                foreach(var el in elementos)
                {
                    temp.AddElement(el);
                }
                Resultado.Add(temp);
            }
        }
    }
}
