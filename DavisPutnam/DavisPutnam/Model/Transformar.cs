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

        public void ImplicationsOut()
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
        public void ReductionsOut()
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
        public void NegationsIn()
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

        public void Distribution()
        {

        }

        public void OperatorsOut()
        {

        }
    }
}
