using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace calculadora
{
    public class clsCalculadora2 : MonoBehaviour
    {
        #region members

        public List<Operacion> PilaDeEjecucion;
        private List<Operacion> subList;
        private Operacion[] sub;
        private Operacion[] sub2;
        private bool opRepetida;
        /// <summary>
        /// numero que que lleva el valor 
        /// </summary>
        private double valor;

        /// <summary>
        ///  valor que representa la cantidad de erroes encontrados
        /// </summary>
        public int numeroDeErrores;
        public int infinito;

        /// <summary>
        /// este valor es falso si se ingreso una operacion sin haver introdusido los valores numericos necesarios para esta por jemplo:( +2*4 en este caso no hay oto valo que acompañe al mas)
        /// </summary>
        private bool ValorNumericoIngresado;
        /// <summary>
        ///  cuando es true cabia  las funciones por la inversa 
        /// </summary>
        public bool lnv;

        /// <summary>
        /// cuando es
        /// </summary>
        public bool Rad;

        private tipo OperacionAnterior;

        #endregion

        # region monoBehaviour

        void Start()
        {

            infinito = 0;
            lnv = false;
            Rad = true;

            OperacionAnterior = tipo.ninguno;
            ValorNumericoIngresado = false;
            numeroDeErrores = 0;
            PilaDeEjecucion = new List<Operacion>();
            subList= new List<Operacion>();
            // subList= new List<Operacion>();
            //MtdProbarCalc();

        }
        void Update()
        {

        }

        #endregion

        #region enum
        private enum tipo
        {
            ninguno,
            operacion,
            elevado,
            parentesisDer,
            numero
        }

        #endregion

        #region private methods
        //para probar la calculadora2
        private void MtdProbarCalc()
        {

            Operacion auxvz = new Operacion();
            auxvz.valorNumerico = 1;
            auxvz.jerarquía = Operacion.JerarquíaOpreacion.numero;
            auxvz.tipOperacion = Operacion.TiposDeOperacion.parentesisDerecho;
            auxvz.DelOperacion = mtdNumero;
            PilaDeEjecucion.Add(auxvz);



            Operacion auxer = new Operacion();
            auxer.valorNumerico = 0;
            auxer.jerarquía = Operacion.JerarquíaOpreacion.resta;
            auxer.tipOperacion = Operacion.TiposDeOperacion.menos;
            auxer.DelOperacion = mtdRestar;
            PilaDeEjecucion.Add(auxer);



            Operacion auxz = new Operacion();
            auxz.valorNumerico = 2;
            auxz.jerarquía = Operacion.JerarquíaOpreacion.numero;
            auxz.tipOperacion = Operacion.TiposDeOperacion.numero;
            auxz.DelOperacion = mtdNumero;
            PilaDeEjecucion.Add(auxz);

            Operacion auxerp = new Operacion();
            auxerp.valorNumerico = 0;
            auxerp.jerarquía = Operacion.JerarquíaOpreacion.multiplicacion;
            auxerp.tipOperacion = Operacion.TiposDeOperacion.operacion;
            auxerp.DelOperacion = mtdMultiplicacion;
            PilaDeEjecucion.Add(auxerp);


            Operacion aux = new Operacion();
            aux.valorNumerico = 3;
            aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
            aux.tipOperacion = Operacion.TiposDeOperacion.numero;
            aux.DelOperacion = mtdNumero;
            PilaDeEjecucion.Add(aux);



            /*

                Operacion auxfr = new Operacion();
                auxfr.valorNumerico = 0;
                auxfr.tipOperacion = Operacion.TiposDeOperacion.alCuadrado;
                auxfr.DelOperacion = mtdRaiz;
                PilaDeEjecucion.Add(auxfr);


                        Operacion auxer = new Operacion();
                        auxer.valorNumerico = 0;
                        auxer.tipOperacion = Operacion.TiposDeOperacion.menos;
                        auxer.DelOperacion = mtdRestar;
                        PilaDeEjecucion.Add(auxer);


                        Operacion aux = new Operacion();
                        aux.valorNumerico = 3;
                        aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                        aux.DelOperacion = mtdNumero;
                        PilaDeEjecucion.Add(aux);


                        Operacion auxerw = new Operacion();
                        auxerw.valorNumerico = 0;
                        auxerw.tipOperacion = Operacion.TiposDeOperacion.parentesisDerecho;
                        auxerw.DelOperacion = mtdParentesisDerecho;
                        PilaDeEjecucion.Add(auxerw);

                        Operacion auxv = new Operacion();
                        auxv.valorNumerico = 0;
                        auxv.tipOperacion = Operacion.TiposDeOperacion.operacion;
                        auxv.DelOperacion = mtdMultiplicacion;
                        PilaDeEjecucion.Add(auxv);



                        Operacion auxz = new Operacion();
                        auxz.valorNumerico = 2;
                        auxz.tipOperacion = Operacion.TiposDeOperacion.numero;
                        auxz.DelOperacion = mtdNumero;
                        PilaDeEjecucion.Add(auxz);

                        Operacion auxvz = new Operacion();
                        auxvz.valorNumerico = 0;
                        auxvz.tipOperacion = Operacion.TiposDeOperacion.parentesisDerecho;
                        auxvz.DelOperacion = mtdParentesisDerecho;
                        PilaDeEjecucion.Add(auxvz);


                        /*
                        Operacion auxvz = new Operacion();
                        auxvz.valorNumerico = 1;
                        auxvz.tipOperacion = Operacion.TiposDeOperacion.parentesisDerecho;
                        auxvz.DelOperacion = mtdParentesisDerecho;
                        PilaDeEjecucion.Add(auxvz);

                        Operacion auxd = new Operacion();
                        auxd.valorNumerico = 0;
                        auxd.tipOperacion = Operacion.TiposDeOperacion.operacion;
                        auxd.DelOperacion = mtdMultiplicacion;
                        PilaDeEjecucion.Add(auxd);


                        Operacion auxc = new Operacion();
                        auxc.valorNumerico = 0;
                        auxc.tipOperacion = Operacion.TiposDeOperacion.menos;
                        auxc.DelOperacion = mtdRestar;
                        PilaDeEjecucion.Add(auxc);


                        Operacion aux = new Operacion();
                        aux.valorNumerico = 3;
                        aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                        aux.DelOperacion = mtdNumero;
                        PilaDeEjecucion.Add(aux);

                        Operacion auxa = new Operacion();
                        auxa.valorNumerico = 0;
                        auxa.tipOperacion = Operacion.TiposDeOperacion.menos;
                        auxa.DelOperacion = mtdRestar;
                        PilaDeEjecucion.Add(auxa);

                        Operacion auxb = new Operacion();
                        auxb.valorNumerico = 3;
                        auxb.tipOperacion = Operacion.TiposDeOperacion.numero;
                        auxb.DelOperacion = mtdNumero;
                        PilaDeEjecucion.Add(auxb);
            */

            MtdIgual();

        }

        /// <summary>
        ///revisa si hay nuemros ingresados para previamente
        /// </summary>
        /// <returns></returns>
        private bool MtdHayNumerosIngresados()
        {
            if (valor == 0)
            {
                if (ValorNumericoIngresado)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// metodo que clacula la potencia de un numero en la pila de operaciones
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        private double mtdPow(double num)
        {

            if (PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.parentesisIzquierdo )//|| PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.menos)
            {
                var pow = PilaDeEjecucion[0].DelOperacion();
                var resultado = System.Math.Pow(num, pow);
                return resultado;
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        /// <summary>
        /// calcula el factorial de un numero
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        private double mtdFactorial(double valor)
        {
            double result = valor;
            if (valor != 0)
            {
                for (int i = 1; i < valor; i++)
                {
                    result = result * i;
                }
                return result;
            }
            else
            {
                result = 1;
                return result;
            }

        }

        private double mtdCalEXP(double num)
        {

            Debug.Log(PilaDeEjecucion[0].jerarquía);
            if (PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.parentesisIzquierdo)
            {
                var pow = PilaDeEjecucion[0].DelOperacion();
                Debug.Log("entre: " + pow);
                if (pow >= 0)
                {
                    for (int i = 0; i < pow; i++)
                    {
                        num = num * 10;
                    }
                }
                else
                {
                    for (int i = 0; i > pow; i = i - 1)
                    {
                        num = num * 0.1;
                    }
                }
                return num;
            }
            else
            {
                Debug.Log("estoy entrando aqui carajo carajo jajjaajaajjajajajajajajjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjjj");
                numeroDeErrores++;
                return 0;
            }
        }

        /// <summary>
        /// ejecutaoperaciones para resolver
        /// </summary>
        /// <returns></returns>
        private double mtdresolverOpUnitarias(Operacion.TiposDeOperacion op, double numero)
        {
            Debug.Log("adentro de exp: " + op);
            switch (op)
            {
                case Operacion.TiposDeOperacion.porsentaje:
                    // PilaDeEjecucion[0].DelOperacion();
                    numero = numero / 100;
                    break;
                case Operacion.TiposDeOperacion.factorial:
                    // PilaDeEjecucion[0].DelOperacion();
                    numero = mtdFactorial(numero);
                    break;
                case Operacion.TiposDeOperacion.elevado:
                    // PilaDeEjecucion[0].DelOperacion();
                    numero = mtdPow(numero);
                    break;
                case Operacion.TiposDeOperacion.alCuadrado:
                    //PilaDeEjecucion[0].DelOperacion();
                    numero = numero * numero;
                    break;
                case Operacion.TiposDeOperacion.Exp:
                    //PilaDeEjecucion[0].DelOperacion();
                    numero = mtdCalEXP(numero);
                    break;
            }

            return numero;

        }
       
        // (2+(5x4+3)x(-3x8-9 )x5 )+1

        ///(2+(20+3)x(-24-9 )x5 ) +1
       

        // ( 2+ 23x33x5)+1

            
        #endregion

        #region public methods

        /// <summary>
        /// borra y deja la pila lista para ingresar nuevos valores   
        /// </summary>
        public void MtdReIniciarPila()
        {

            OperacionAnterior = tipo.ninguno;
            ValorNumericoIngresado = false;
            numeroDeErrores = 0;
            PilaDeEjecucion = new List<Operacion>();

        }

        /// <summary>
        /// metodo que ejecuta la pila de operaciones
        /// </summary>
        public double MtdIgual()
        {
            Operacion final = new Operacion();
            final.tipOperacion = Operacion.TiposDeOperacion.fin;
            final.valorNumerico =0;
            final.DelOperacion =null;
            final.jerarquía = Operacion.JerarquíaOpreacion.fin;

            PilaDeEjecucion.Add(final);
            sub = PilaDeEjecucion.ToArray();

            mtdSolucionJerarquiaParenteses();

            mtdResolverJerarquiPor(Operacion.JerarquíaOpreacion.division);
            mtdResolverJerarquiPor(Operacion.JerarquíaOpreacion.multiplicacion);

            mtdllenarPilaDejEjecucion(sub);

            valor = solucionLineal();


            Debug.Log("se ejecutaron las operaciones numero de errores: " + numeroDeErrores);
            Debug.Log("el resuldado es: " + valor);
            return valor;

        }

        /// <summary>
        /// metodo que resulve primero la operacion que sele entrega
        /// </summary>
        /// <param name="jerarquía"></param>
        private void mtdResolverJerarquiPor(Operacion.JerarquíaOpreacion jerarquía)
        {
            Operacion anterior=sub[0];
            int posterior = 0;

            for (int i=0 ; i < sub.Length; i++)
            {
                if (sub[i].jerarquía == jerarquía && sub[i-1].jerarquía!=Operacion.JerarquíaOpreacion.parentesisDe&& sub[i + 1].jerarquía != Operacion.JerarquíaOpreacion.parentesisIz)
                {
                   // opRepetida = true;

                    if (anterior.jerarquía == Operacion.JerarquíaOpreacion.numero && sub[i + 1].jerarquía == Operacion.JerarquíaOpreacion.numero)
                    {

                        if (i + 2 < sub.Length)
                        {
                            if (sub[i + 2].jerarquía == Operacion.JerarquíaOpreacion.OpMod)
                            {

                                PilaDeEjecucion.Clear();
                                if (!opRepetida)
                                    PilaDeEjecucion.Add(sub[i - 1]);
                                PilaDeEjecucion.Add(sub[i]);
                                PilaDeEjecucion.Add(sub[i + 1]);
                                PilaDeEjecucion.Add(sub[i + 2]);

                                var num = solucionLineal();

                                Debug.Log("este es el resultado de la funcion solucion lineal 1x2%= " + num);
                                Operacion aux = new Operacion();
                                aux.valorNumerico = num;
                                aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                aux.DelOperacion = mtdNumero;
                                if (!opRepetida)
                                    subList.Add(aux);
                                else
                                {
                                    var index = subList.Count;
                                    subList[index] = aux;
                                }

                                posterior = posterior + 2;

                            }
                            else
                            {

                                PilaDeEjecucion.Clear();
                                if (!opRepetida)
                                    PilaDeEjecucion.Add(sub[i - 1]);
                                PilaDeEjecucion.Add(sub[i]);
                                PilaDeEjecucion.Add(sub[i + 1]);

                                var num = solucionLineal();

                                Debug.Log("este es el resultado de la funcion solucion lineal 1x2= " + num);
                                Operacion aux = new Operacion();
                                aux.valorNumerico = num;
                                aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                aux.DelOperacion = mtdNumero;
                                if (!opRepetida)
                                    subList.Add(aux);
                                else
                                {
                                    var index = subList.Count;
                                    subList[index-1] = aux;
                                }

                                posterior = posterior + 1;

                            }

                        }
                        else
                        {
                            PilaDeEjecucion.Clear();
                            if (!opRepetida)
                                PilaDeEjecucion.Add(sub[i - 1]);
                            PilaDeEjecucion.Add(sub[i]);
                            PilaDeEjecucion.Add(sub[i + 1]);

                            var num = solucionLineal();

                            Debug.Log("este es el resultado de la funacion solucion lineal 1x2fin= " + num);
                            Operacion aux = new Operacion();
                            aux.valorNumerico = num;
                            aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                            aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                            aux.DelOperacion = mtdNumero;
                            if (!opRepetida)
                                subList.Add(aux);
                            else
                            {
                                var index = subList.Count;
                                subList[index - 1] = aux;
                            }
                            posterior = posterior + 1;
                        }


                    }
                    else
                    {

                        if (anterior.jerarquía == Operacion.JerarquíaOpreacion.numero && sub[i + 1].jerarquía == Operacion.JerarquíaOpreacion.resta)
                        {
                            if (i + 2 < sub.Length)
                            {
                                if (anterior.jerarquía == Operacion.JerarquíaOpreacion.numero && sub[i + 1].jerarquía == Operacion.JerarquíaOpreacion.resta && sub[i + 2].jerarquía == Operacion.JerarquíaOpreacion.numero)
                                {
                                    if (i + 3 < sub.Length)
                                    {
                                        if (sub[i + 3].jerarquía == Operacion.JerarquíaOpreacion.OpMod)
                                        {
                                            PilaDeEjecucion.Clear();
                                            if (!opRepetida)
                                                PilaDeEjecucion.Add(sub[i - 1]);
                                            PilaDeEjecucion.Add(sub[i]);
                                            PilaDeEjecucion.Add(sub[i + 1]);
                                            PilaDeEjecucion.Add(sub[i + 2]);
                                            PilaDeEjecucion.Add(sub[i + 3]);

                                            var num = solucionLineal();

                                            Debug.Log("este es el resultado de la funacion solucion lineal   4x-1% = " + num);
                                            Operacion aux = new Operacion();
                                            aux.valorNumerico = num;
                                            aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                            aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                            aux.DelOperacion = mtdNumero;
                                            if (!opRepetida)
                                                subList.Add(aux);
                                            else
                                            {
                                                var index = subList.Count;
                                                subList[index - 1] = aux;
                                            }

                                            posterior = posterior + 3;
                                        }
                                        else
                                        {
                                            PilaDeEjecucion.Clear();
                                            if (!opRepetida)
                                                PilaDeEjecucion.Add(sub[i - 1]);
                                            PilaDeEjecucion.Add(sub[i]);
                                            PilaDeEjecucion.Add(sub[i + 1]);
                                            PilaDeEjecucion.Add(sub[i + 2]);

                                            var num = solucionLineal();

                                            Debug.Log("este es el resultado de la funacion solucion lineal   4x-1 = " + num);
                                            Operacion aux = new Operacion();
                                            aux.valorNumerico = num;
                                            aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                            aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                            aux.DelOperacion = mtdNumero;
                                            if (!opRepetida)
                                                subList.Add(aux);
                                            else
                                            {
                                                var index = subList.Count;
                                                subList[index - 1] = aux;
                                            }

                                            posterior = posterior + 2;

                                        }

                                    }
                                    else
                                    {
                                        PilaDeEjecucion.Clear();
                                        if (!opRepetida)
                                            PilaDeEjecucion.Add(sub[i - 1]);
                                        PilaDeEjecucion.Add(sub[i]);
                                        PilaDeEjecucion.Add(sub[i + 1]);
                                        PilaDeEjecucion.Add(sub[i + 2]);

                                        var num = solucionLineal();

                                        Debug.Log("este es el resultado de la funacion solucion lineal   4x-1fin = " + num);
                                        Operacion aux = new Operacion();
                                        aux.valorNumerico = num;
                                        aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                        aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                        aux.DelOperacion = mtdNumero;
                                        if (!opRepetida)
                                            subList.Add(aux);
                                        else
                                        {
                                            var index = subList.Count;
                                            subList[index - 1] = aux;
                                        }

                                        posterior = posterior + 2;
                                    }

                                }



                            }
                        }
                        
                        if (anterior.jerarquía == Operacion.JerarquíaOpreacion.OpMod && sub[i + 1].jerarquía == Operacion.JerarquíaOpreacion.numero)
                        {
                            if (i + 2 < sub.Length)
                            {
                                if (sub[i + 2].jerarquía == Operacion.JerarquíaOpreacion.OpMod)
                                {
                                    PilaDeEjecucion.Clear();
                                    if (!opRepetida)
                                    {
                                        PilaDeEjecucion.Add(sub[i - 2]);
                                        PilaDeEjecucion.Add(sub[i - 1]);
                                    }
                                    PilaDeEjecucion.Add(sub[i]);
                                    PilaDeEjecucion.Add(sub[i + 1]);
                                    PilaDeEjecucion.Add(sub[i + 2]);

                                    var num = solucionLineal();

                                    Debug.Log("este es el resultado de la funacion solucion lineal   s%x1% = " + num);
                                    Operacion aux = new Operacion();
                                    aux.valorNumerico = num;
                                    aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                    aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                    aux.DelOperacion = mtdNumero;

                                    if (!opRepetida)
                                        subList.Add(aux);
                                    else
                                    {
                                        var index = subList.Count;
                                        subList[index - 1] = aux;
                                    }

                                    posterior = posterior + 2;


                                }
                                else
                                {

                                    PilaDeEjecucion.Clear();
                                    if (!opRepetida)
                                    {
                                        PilaDeEjecucion.Add(sub[i - 2]);
                                        PilaDeEjecucion.Add(sub[i - 1]);
                                    }
                                    PilaDeEjecucion.Add(sub[i]);
                                    PilaDeEjecucion.Add(sub[i + 1]);

                                    var num = solucionLineal();

                                    Debug.Log("este es el resultado de la funacion solucion lineal   4%x1 = " + num);
                                    Operacion aux = new Operacion();
                                    aux.valorNumerico = num;
                                    aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                    aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                    aux.DelOperacion = mtdNumero;
                                    if (!opRepetida)
                                        subList.Add(aux);
                                    else
                                    {
                                        var index = subList.Count;
                                        subList[index - 1] = aux;
                                    }

                                    posterior = posterior + 1;
                                }

                            }
                            else
                            {
                                PilaDeEjecucion.Clear();
                                if (!opRepetida)
                                {
                                    PilaDeEjecucion.Add(sub[i - 2]);
                                    PilaDeEjecucion.Add(sub[i - 1]);
                                }

                                PilaDeEjecucion.Add(sub[i]);
                                PilaDeEjecucion.Add(sub[i + 1]);

                                var num = solucionLineal();

                                Debug.Log("este es el resultado de la funacion solucion lineal   4%x1fin = " + num);
                                Operacion aux = new Operacion();
                                aux.valorNumerico = num;
                                aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                aux.DelOperacion = mtdNumero;
                                if (!opRepetida)
                                    subList.Add(aux);
                                else
                                {
                                    var index = subList.Count;
                                    subList[index - 1] = aux;
                                }

                                posterior = posterior + 1;
                            }


                        }
                        else
                        {
                            
                            if (i + 2 < sub.Length)
                            {

                                if (anterior.jerarquía == Operacion.JerarquíaOpreacion.OpMod && sub[i + 1].jerarquía == Operacion.JerarquíaOpreacion.resta && sub[i + 2].jerarquía == Operacion.JerarquíaOpreacion.numero)
                                {
                                    if (i + 3 < sub.Length)
                                    {
                                        if (sub[i + 3].jerarquía == Operacion.JerarquíaOpreacion.OpMod)
                                        {
                                            PilaDeEjecucion.Clear();
                                            if (!opRepetida)
                                            {
                                                PilaDeEjecucion.Add(sub[i - 2]);
                                                PilaDeEjecucion.Add(sub[i - 1]);
                                            }

                                            PilaDeEjecucion.Add(sub[i]);
                                            PilaDeEjecucion.Add(sub[i + 1]);
                                            PilaDeEjecucion.Add(sub[i + 2]);
                                            PilaDeEjecucion.Add(sub[i + 3]);

                                            var num = solucionLineal();

                                            Debug.Log("este es el resultado de la funacion solucion lineal   4%x-1% = " + num);
                                            Operacion aux = new Operacion();
                                            aux.valorNumerico = num;
                                            aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                            aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                            aux.DelOperacion = mtdNumero;
                                            if (!opRepetida)
                                                subList.Add(aux);
                                            else
                                            {
                                                var index = subList.Count;
                                                subList[index - 1] = aux;
                                            }

                                            posterior = posterior + 3;
                                        }
                                        else
                                        {

                                            PilaDeEjecucion.Clear();
                                            if (!opRepetida)
                                            {
                                                PilaDeEjecucion.Add(sub[i - 2]);
                                                PilaDeEjecucion.Add(sub[i - 1]);
                                            }
                                            PilaDeEjecucion.Add(sub[i]);
                                            PilaDeEjecucion.Add(sub[i + 1]);
                                            PilaDeEjecucion.Add(sub[i + 2]);

                                            var num = solucionLineal();

                                            Debug.Log("este es el resultado de la funacion solucion lineal   4%x-1 = " + num);
                                            Operacion aux = new Operacion();
                                            aux.valorNumerico = num;
                                            aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                            aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                            aux.DelOperacion = mtdNumero;
                                            if (!opRepetida)
                                                subList.Add(aux);
                                            else
                                            {
                                                var index = subList.Count;
                                                subList[index - 1] = aux;
                                          }

                                            posterior = posterior + 2;

                                        }

                                    }
                                    else
                                    {
                                        PilaDeEjecucion.Clear();
                                        if (!opRepetida)
                                        {
                                            PilaDeEjecucion.Add(sub[i - 2]);
                                            PilaDeEjecucion.Add(sub[i - 1]);
                                        }
                                        PilaDeEjecucion.Add(sub[i]);
                                        PilaDeEjecucion.Add(sub[i + 1]);
                                        PilaDeEjecucion.Add(sub[i + 2]);

                                        var num = solucionLineal();

                                        Debug.Log("este es el resultado de la funacion solucion lineal   4%x-1fin = " + num);
                                        Operacion aux = new Operacion();
                                        aux.valorNumerico = num;
                                        aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                                        aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                                        aux.DelOperacion = mtdNumero;
                                        if (!opRepetida)
                                            subList.Add(aux);
                                        else
                                        {
                                            var index = subList.Count;
                                            subList[index - 1] = aux;
                                        }
                                        posterior = posterior + 2;
                                    }

                                    //tiene que entrar obligatoria mente a los dos anteriores

                                }
                                else
                                {
                                  //  numeroDeErrores++;
                                }

                            }
                            else
                            {
                                //numeroDeErrores++;
                            }


                        }

                    }
                    opRepetida = true;
                }
                else
                {
                    if (opRepetida)
                    {
                        if (sub[i].jerarquía == jerarquía || sub[i].jerarquía == Operacion.JerarquíaOpreacion.numero)
                        {
                            Debug.Log("no hay operacion diferente");
                        }
                        else
                        {

                            if (sub[i].jerarquía == Operacion.JerarquíaOpreacion.resta)
                            {
                                if (i + 1 < sub.Length)
                                {
                                    if (anterior.jerarquía == Operacion.JerarquíaOpreacion.numero && sub[i + 1].jerarquía == Operacion.JerarquíaOpreacion.numero)
                                    {
                                        Debug.Log("Ahora si  hay operacion diferente");
                                        opRepetida = false;
                                    }

                                }

                            }
                            else {

                                if (sub[i].jerarquía == Operacion.JerarquíaOpreacion.OpMod)
                                {
                                    Debug.Log("no hay operacion diferente");

                                }
                                else
                                {
                                    if (sub[i].jerarquía == Operacion.JerarquíaOpreacion.multiplicacion || sub[i].jerarquía == Operacion.JerarquíaOpreacion.division || sub[i].jerarquía == Operacion.JerarquíaOpreacion.suma)
                                    {
                                        Debug.Log("Ahora si  hay operacion diferente(esta es la sobrante)");
                                        opRepetida = false;
                                    }
                                    else
                                    {
                                        Debug.Log("no hay operacion diferente");
                                    }

                                }
                            }
                            
                        }
                    }

                    if (i + 1 < sub.Length)
                    {

                        if (i + 2 < sub.Length)
                        {
                            if (sub[i].jerarquía == Operacion.JerarquíaOpreacion.numero && sub[i + 1].jerarquía == Operacion.JerarquíaOpreacion.OpMod && sub[i + 2].jerarquía == jerarquía)
                            {

                                Debug.Log("entre al caso de 1%x");

                                if (posterior == 0)
                                {
                                   // subList.Add(sub[i]);
                                }
                                else
                                {
                                    posterior = posterior - 1;
                                }
                            }
                            else
                            {
                                if (sub[i].jerarquía == Operacion.JerarquíaOpreacion.OpMod && sub[i + 1].jerarquía == jerarquía)
                                {
                                    Debug.Log("entre al caso de %x");
                                    if (posterior == 0)
                                    {
                                       // subList.Add(sub[i]);
                                    }
                                    else
                                    {
                                        posterior = posterior - 1;
                                    }
                                }
                                else
                                {

                                    if (sub[i].jerarquía == Operacion.JerarquíaOpreacion.numero && sub[i + 1].jerarquía == jerarquía)
                                    {
                                        Debug.Log("entre al caso de 1x");
                                        if (posterior == 0)
                                        {
                                           // subList.Add(sub[i]);
                                        }
                                        else
                                        {
                                            posterior = posterior - 1;
                                        }
                                    }
                                    else
                                    {
                                        if (posterior == 0)
                                        {
                                            subList.Add(sub[i]);
                                        }
                                        else
                                        {
                                            posterior = posterior - 1;
                                        }
                                    }

                                }

                            }



                        }
                        else
                        {
                        
                            if ( sub[i + 1].jerarquía != jerarquía)
                            {
                                if (posterior==0)
                                {
                                    subList.Add(sub[i]);
                                }
                                else
                                {
                                    posterior = posterior-1;
                                }
                            }
                        }

                    }
                    else
                    {

                       
                            subList.Add(sub[i]);
                    }

                }

                anterior = sub[i];
            }
           
            sub = subList.ToArray();
            subList.Clear();
            opRepetida = false;

        }
       
        private void mtdSolucionJerarquiaParenteses(  )
        {
            sub2=  PilaDeEjecucion.ToArray(); 

            bool termine=false;
            bool resolverSubparentesis = false;
            int IndexParentesisIquierdo=-1;
            int IndexParentesisDerecho = 0;

            while(!termine)
            {
  
                for (int i = 0; i < sub2.Length && !resolverSubparentesis; i++)
                {
                    //Debug.Log("for secundario = "+ sub2[i].jerarquía);
                    if (sub2[i].jerarquía == Operacion.JerarquíaOpreacion.parentesisIz)
                    {  
                        if (i > 0)
                        {
                            if (sub2[i - 1].jerarquía == Operacion.JerarquíaOpreacion.OpUnitaria)
                            {
                                Debug.Log("hola1");
                                IndexParentesisIquierdo = i - 1;
                            }
                            else
                            {
                                if (sub2[i - 1].jerarquía == Operacion.JerarquíaOpreacion.opdoble)
                                {
                                   
                                    Debug.Log("hola2");
                                     IndexParentesisIquierdo = i - 2;

                                }
                                else
                                {
                                    Debug.Log("hola3");
                                    IndexParentesisIquierdo = i;
                                }

                            }
                        }
                        else {
                            IndexParentesisIquierdo = i;
                        }

                       
                    }

                    if (sub2[i].jerarquía == Operacion.JerarquíaOpreacion.parentesisDe)
                    {
                        IndexParentesisDerecho = i;
                        if (IndexParentesisIquierdo != -1)
                        {
                            resolverSubparentesis = true;
                        }
                        else
                        {
                            numeroDeErrores++;
                            resolverSubparentesis = true;
                            termine = true;
                        }
                        
                    }
                    if (i + 1 == sub2.Length)
                    {
                        termine = true;
                    }

                }
                if (!termine)
                {
                    Debug.Log("parentesis izquierdo en " + IndexParentesisIquierdo + " parentesis derecho en " + IndexParentesisDerecho);
                    mtdLlenarPilaDeEjecuCONsubParentesis(IndexParentesisIquierdo,IndexParentesisDerecho);
                    sub = PilaDeEjecucion.ToArray();
                    //imprimir();
                    var tamLIsta = PilaDeEjecucion.Count;
                    OperacionAnterior = tipo.ninguno;
                    var num= solucionLineal();
                    Debug.Log("resultado del subparentesis= "+num);
                    Operacion aux = new Operacion();
                    aux.valorNumerico = num ;
                    aux.jerarquía = Operacion.JerarquíaOpreacion.numero;
                    aux.tipOperacion = Operacion.TiposDeOperacion.numero;
                    aux.DelOperacion = mtdNumero;
                    Debug.Log("esto es lo que hay en  indexparenteisis= " + IndexParentesisIquierdo);
                    insertarEleentoENsub(aux, IndexParentesisIquierdo, tamLIsta+IndexParentesisIquierdo);
                    //imprimir();
                    Debug.Log("esto es lo que hay en  indexparenteisis izquierdo nueva lista "+ sub2[IndexParentesisIquierdo].valorNumerico );
                    resolverSubparentesis = false;
                    IndexParentesisIquierdo = -1;
                    IndexParentesisDerecho = 0;

                }


            }
            OperacionAnterior = tipo.ninguno;
            sub = sub2;

        }


        private void imprimir()
        {
            int tamS2 = sub2.Length;
            int tamS = sub.Length;
            int tamL = PilaDeEjecucion.Count;
            for (int i = 0; i < tamL; i++)
            {
                  Debug.Log("loquetien la lista prinsipal= "+PilaDeEjecucion[i].jerarquía);
            }
            for (int i = 0; i < tamS; i++)
            {
                Debug.Log("loque tiene sub= "+sub[i].jerarquía);
            }
            for (int i = 0; i < tamS2; i++)
            {
                Debug.Log("loque tiene sub2= " + sub2[i].jerarquía);
            }

        }


        /// <summary>
        /// crea la neuva lista con el resultado que dio el sub parentesis
        /// </summary>
        /// <param name="NewElemento">recultado del parentesis</param>
        /// <param name="Ubicacion">ubicaciondonde se va  a guardar el nuevo elemento</param>
        /// <param name="rango">rango del cual se va a dejar de copiar</param>
        private void insertarEleentoENsub(Operacion NewElemento,int Ubicacion,int rango)
        {
         

            for(int i=0;i<sub2.Length; i++)
            {
                if (i== Ubicacion)
                {
                    Debug.Log("insertarEleentoENsub = "+NewElemento.valorNumerico+" ubicacion "+Ubicacion+" rengo "+rango);
                    subList.Add(NewElemento);
                }
                else
                {
                    if (i < Ubicacion || i >= rango)
                    {
                        Debug.Log("esto es lo que  va en la neuva pila = " + sub2[i].jerarquía);
                        subList.Add(sub2[i]);
                    }
                }

            }

            sub2 = subList.ToArray();
            subList.Clear();

        }

        /// <summary>
        /// llena la pila prinsipal con el sub parentesis encontrado 
        /// </summary>
        /// <param name="InicioDEcopia">ubicacion en sub del parentesis izquierdo</param>
        /// <param name="FinDEcopia">ubicacion en sub del parentesis derecho</param>
        private void mtdLlenarPilaDeEjecuCONsubParentesis(int InicioDEcopia,int FinDEcopia )
        {
  
            PilaDeEjecucion.Clear();
            while (InicioDEcopia<=FinDEcopia)
            {
                Debug.Log("llenando la pila con el subparentesis para resolverlo con ="+ sub2[InicioDEcopia].jerarquía);
                PilaDeEjecucion.Add(sub2[InicioDEcopia]);
                InicioDEcopia++;
            }

        }

        /// <summary>
        /// llena la pila de ejecucion con el array proporcionado del tipo operacion
        /// </summary>
        /// <param name="list">array con el que vamos a llenar la lista </param>
        private void mtdllenarPilaDejEjecucion (Operacion[] list) {
            PilaDeEjecucion.Clear();
            int tamL = list.Length;
            for (int i=0; i<tamL;i++)
            {
               // Debug.Log("estoy dentro del llenar pila ejecucion "+list[i].tipOperacion);

                PilaDeEjecucion.Add(list[i]);
            }

        }

        /// <summary>
        /// soluciona la pila de derecha a izquierda
        /// </summary>
        /// <returns></returns>
        private double solucionLineal()
        {
            if (!opRepetida)
            {
                Debug.Log("entre a opRepetida");
                valor = 0;
            }
            Operacion final = new Operacion();
            final.tipOperacion = Operacion.TiposDeOperacion.fin;
            final.valorNumerico = 0;
            final.DelOperacion = null;
            final.jerarquía = Operacion.JerarquíaOpreacion.fin;

            PilaDeEjecucion.Add(final);

           // Debug.Log("lo que hay en la subLIst " + PilaDeEjecucion[1].tipOperacion);
            while (PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.fin)
            {
                Debug.Log("lo que hay en la subLIst " + PilaDeEjecucion[0].jerarquía);
                valor = PilaDeEjecucion[0].DelOperacion();

            }

            return valor;
        }



        /// <summary>
        /// metodo que se coloca al ingresar un numero a la pila
        /// </summary>
        /// <returns></returns>
        public double mtdNumero()
        {
            Debug.Log("nuemro");

            ValorNumericoIngresado = true;
            var numero = PilaDeEjecucion[0].valorNumerico;
            PilaDeEjecucion.RemoveAt(0);

            switch (PilaDeEjecucion[0].tipOperacion)
            {
                case Operacion.TiposDeOperacion.porsentaje:
                    PilaDeEjecucion[0].DelOperacion();
                    numero = numero / 100;
                    break;
                case Operacion.TiposDeOperacion.factorial:
                    PilaDeEjecucion[0].DelOperacion();
                    numero = mtdFactorial(numero);
                    break;
                case Operacion.TiposDeOperacion.elevado:
                    PilaDeEjecucion[0].DelOperacion();
                    numero = mtdPow(numero);
                    break;
                case Operacion.TiposDeOperacion.alCuadrado:
                    PilaDeEjecucion[0].DelOperacion();
                    numero = numero * numero;
                    break;
                case Operacion.TiposDeOperacion.Exp:
                    PilaDeEjecucion[0].DelOperacion();
                    numero = mtdCalEXP(numero);
                    break;
            }

            OperacionAnterior = tipo.numero;
            return numero;

        }

        /// <summary>
        /// metodo de sumar que es llamado cuando se encuentra en la pila 
        /// </summary>
        /// <returns></returns>
        public double mtdSuma()
        {
            Debug.Log("suma");
            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            if (MtdHayNumerosIngresados())
            {

                if (PilaDeEjecucion.Count != 1)
                {
                    var valorSiguiente = PilaDeEjecucion[0];

                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.numero || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.menos || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.parentesisIzquierdo || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.Unitarias)
                    {
                        OperacionAnterior = tipo.operacion;
                        return valor + valorSiguiente.DelOperacion();
                    }
                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.parentesisIzquierdo)
                    {
                        valorSiguiente.DelOperacion();
                        // para poner lo que se ejecutaria al encontrar parentesis derecho
                    }

                    numeroDeErrores++;
                    return 0;
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }
        }

        /// <summary>
        /// metodo para restar los valores que seria ingresado y llamado desde la pila 
        /// </summary>
        /// <returns></returns>
        public double mtdRestar()
        {
            Debug.Log("resta");

            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            if (MtdHayNumerosIngresados())
            {

                if (PilaDeEjecucion.Count != 1)
                {

                    var valorSiguiente = PilaDeEjecucion[0];
                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.numero && OperacionAnterior == tipo.numero || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.Unitarias)
                    {
                        OperacionAnterior = tipo.operacion;
                        return valor - valorSiguiente.DelOperacion();
                    }
                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.parentesisIzquierdo)
                    {
                        return valor - PilaDeEjecucion[0].DelOperacion();
                        // para poner lo que se ejecutaria al encontrar parentesis derecho

                    }
                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.numero && OperacionAnterior == tipo.operacion || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.numero && OperacionAnterior == tipo.ninguno)
                    {
                        return valorSiguiente.DelOperacion() * -1;
                        // para poner lo que se ejecutaria al encontrar parentesis derecho

                    }

                    numeroDeErrores++;
                    return 0;
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }
            }
            else
            {
                if (tipo.ninguno == OperacionAnterior && PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.numero || PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.numero && OperacionAnterior == tipo.operacion)
                {
                    OperacionAnterior = tipo.numero;
                    return PilaDeEjecucion[0].DelOperacion() * -1;
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }
            }
        }

        /// <summary>
        /// Metodo que sedebe encola al precionar el boton multiplicacion
        /// </summary>
        /// <returns></returns>
        public double mtdMultiplicacion()
        {

            Debug.Log("Multiplicacion");
            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            if (MtdHayNumerosIngresados())
            {

                if (PilaDeEjecucion.Count != 1)
                {
                    var valorSiguiente = PilaDeEjecucion[0];
                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.numero || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.menos || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.parentesisIzquierdo || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.Unitarias)
                    {
                        OperacionAnterior = tipo.operacion;
                        return valor * valorSiguiente.DelOperacion();
                    }
                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.parentesisDerecho)
                    {
                        valorSiguiente.DelOperacion();
                        // para poner lo que se ejecutaria al encontrar parentesis derecho

                    }
                    numeroDeErrores++;
                    return 0;
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        /// <summary>
        /// metodoque se debe encolar al precionar el boton de division en la calculadora
        /// </summary>
        /// <returns></returns>
        public double mtdDivision()
        {

            Debug.Log("Division");

            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            if (MtdHayNumerosIngresados())
            {

                if (PilaDeEjecucion.Count != 0)
                {

                    var valorSiguiente = PilaDeEjecucion[0];
                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.numero || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.menos || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.parentesisIzquierdo || valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.Unitarias)
                    {
                        OperacionAnterior = tipo.operacion;
                        return valor / valorSiguiente.DelOperacion();
                    }
                    if (valorSiguiente.tipOperacion == Operacion.TiposDeOperacion.parentesisDerecho)
                    {
                        valorSiguiente.DelOperacion();
                        // para poner lo que se ejecutaria al encontrar parentesis derecho

                    }
                    numeroDeErrores++;
                    return 0;
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        /// <summary>
        /// metodo que se debe encola al precionar el boton de sen( en la calculadora
        /// </summary>
        /// <returns></returns>
        public double mtdSen()
        {

            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            OperacionAnterior = tipo.operacion;

            if (PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.operacion && PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.parentesisDerecho)
            {
                if (Rad)
                {
                    if (lnv)
                    {
                        var resultado = PilaDeEjecucion[0].DelOperacion();
                        if (resultado <= 1)
                        {
                            return System.Math.Sinh(resultado);
                        }
                        else
                        {
                            numeroDeErrores++;
                            return 0;
                        }

                    }
                    else
                    {
                        return System.Math.Sin(PilaDeEjecucion[0].DelOperacion());
                    }
                }
                else
                {
                    if (lnv)
                    {
                        double num = PilaDeEjecucion[0].DelOperacion();
                        if (num <= 1 && num >= -1)
                        {
                            num = System.Math.Asin(num);
                            return num * (180 / System.Math.PI);
                        }
                        else
                        {
                            numeroDeErrores++;
                            return 0;
                        }

                    }
                    else
                    {
                        double resulp = System.Math.PI * (PilaDeEjecucion[0].DelOperacion() / 180);
                        return System.Math.Sin(resulp);
                    }
                }

            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        public double mtdcos()
        {
            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            OperacionAnterior = tipo.operacion;

            if (PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.operacion && PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.parentesisDerecho)
            {
                if (Rad)
                {
                    if (lnv)
                    {
                        var resultado = PilaDeEjecucion[0].DelOperacion();
                        if (resultado <= 1)
                        {
                            return System.Math.Acos(resultado);
                        }
                        else
                        {
                            numeroDeErrores++;
                            return 0;
                        }

                    }
                    else
                    {
                        return System.Math.Cos(PilaDeEjecucion[0].DelOperacion());
                    }
                }
                else
                {

                    if (lnv)
                    {
                        double num = PilaDeEjecucion[0].DelOperacion();
                        if (num <= 1 && num >= -1)
                        {
                            num = System.Math.Acos(num);
                            return num * (180 / System.Math.PI);
                        }
                        else
                        {
                            numeroDeErrores++;
                            return 0;
                        }

                    }
                    else
                    {
                        double resulp = System.Math.PI * (PilaDeEjecucion[0].DelOperacion() / 180);
                        return System.Math.Cos(resulp);
                    }
                }

            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        public double mtdtan()
        {
            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            OperacionAnterior = tipo.operacion;

            if (PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.operacion && PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.parentesisDerecho)
            {
                if (Rad)
                {
                    if (lnv)
                    {
                        double resultado = PilaDeEjecucion[0].DelOperacion();
                        return System.Math.Atan(resultado);
                    }
                    else
                    {
                        double num = PilaDeEjecucion[0].DelOperacion();

                        if (num != (System.Math.PI / 2) && num != ((3 * System.Math.PI) / 2))
                        {
                            return System.Math.Tan(num);
                        }
                        else
                        {
                            numeroDeErrores++;
                            return 0;
                        }
                    }
                }
                else
                {

                    if (lnv)
                    {
                        double resulp = System.Math.Atan(PilaDeEjecucion[0].DelOperacion());
                        return resulp * (180 / System.Math.PI);
                    }
                    else
                    {
                        double num = PilaDeEjecucion[0].DelOperacion();
                        double resulp = System.Math.PI * num / 180;

                        if (num != 90 && num != 270 && num != 245 && num != 630 && num != -90 && num != -270 && num != -245 && num != -630)
                        {
                            return System.Math.Tan(resulp);
                        }
                        else
                        {
                            numeroDeErrores++;
                            return 0;
                        }

                    }

                }

            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }


        /// <summary>
        /// metodo que inicia las operaciones puestas entre parentesis este se debe encolar al presionar
        /// </summary>
        /// <returns></returns>
        public double mtdParentesisIzquierdo()
        {
            Debug.Log("parenIZ");
            var yo = PilaDeEjecucion[0];

            PilaDeEjecucion.RemoveAt(0);
            OperacionAnterior = tipo.operacion;

            if (PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.numero || PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.Unitarias || PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.menos)
            {
                ///OperacionAnterior = tipo.operacion;
                return mtdResolverParentesis();
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        /// <summary>
        /// metodo que termina 
        /// </summary>
        /// <returns></returns>
        public double mtdParentesisDerecho()
        {
            Debug.Log("parenDer");
            PilaDeEjecucion.RemoveAt(0);
            OperacionAnterior = tipo.parentesisDer;
            return 0;

        }
        
        /// <summary>
        /// metodo que ejecuta las operaciones dentro de un parentesis
        /// </summary>
        /// <returns></returns>
        public double mtdResolverParentesis()   ///--------------------------------------------------------------------------------------------------------------------
        {
            Operacion[] backSubList = subList.ToArray();//la lista que lleva hasta el momento
            Operacion[] backsub= sub; // guardo el vector sub que va hasta el momento 
            sub = PilaDeEjecucion.ToArray();



            bool continuar = true;
            var valorPasado = valor;
            valor = 0;

            mtdResolverJerarquiPor(Operacion.JerarquíaOpreacion.division);
            mtdResolverJerarquiPor(Operacion.JerarquíaOpreacion.multiplicacion);

            mtdllenarPilaDejEjecucion(sub);

            
            while (PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.parentesisDerecho && continuar)
            {
                if (PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.fin)
                {
                    continuar = false;
                    numeroDeErrores++;
                }
                else
                {
                    valor = PilaDeEjecucion[0].DelOperacion();

                }

            }
            if (PilaDeEjecucion[0].tipOperacion == Operacion.TiposDeOperacion.parentesisDerecho)
            {
                PilaDeEjecucion[0].DelOperacion();      
            }
            var resul = valor;
            valor = valorPasado;

            if (continuar)
                return resul;
            else
                return 0;

        }

        /// <summary>
        /// elimina los elementos de la pila 
        /// </summary>
        public void MtdBorrarOP()
        {

            Debug.Log("removi op");
            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);

        }

        public double mtdRaiz()
        {
            var yo = PilaDeEjecucion[0];
            var op = PilaDeEjecucion[0].tipOperacion;
            PilaDeEjecucion.RemoveAt(0);

            if (!lnv)
            {
                if (PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.operacion && PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.parentesisDerecho)
                {
                    var num = PilaDeEjecucion[0].DelOperacion();
                    if (num >= 0)
                    {
                        OperacionAnterior = tipo.operacion;
                        return System.Math.Sqrt(num);
                    }
                    else
                    {
                        numeroDeErrores++;
                        return 0;
                    }
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }

            }
            else
            {
                if (MtdHayNumerosIngresados())
                {
                    if (tipo.parentesisDer == OperacionAnterior)
                    {
                        double num = mtdresolverOpUnitarias(op, valor);
                        // Debug.Log("EL resultado del exp:"+ num );
                        return num;
                    }
                    return 0;
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }

            }
        }

        public double mtdlog()
        {
            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            if (!lnv)
            {
                if (PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.operacion && PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.parentesisDerecho)
                {
                    var num = PilaDeEjecucion[0].DelOperacion();
                    if (num == 0)
                    {
                        OperacionAnterior = tipo.numero;
                        infinito = -1;
                        return 0;
                    }

                    if (num > 0)
                    {
                        OperacionAnterior = tipo.numero;
                        return System.Math.Log10(num);
                    }
                    else
                    {

                        numeroDeErrores++;
                        return 0;
                    }
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }

            }
            else
            {
                OperacionAnterior = tipo.numero;
                var num = PilaDeEjecucion[0].DelOperacion();
                return System.Math.Pow(10, num);

            }
        }

        public double mtdln()
        {
            var yo = PilaDeEjecucion[0];
            PilaDeEjecucion.RemoveAt(0);
            OperacionAnterior = tipo.numero;//------------------------------------------------------
            if (!lnv)
            {
                if (PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.operacion && PilaDeEjecucion[0].tipOperacion != Operacion.TiposDeOperacion.parentesisDerecho)
                {
                    var num = PilaDeEjecucion[0].DelOperacion();
                    if (num == 0)
                    {
                        infinito = -1;
                        return 0;
                    }

                    if (num > 0)
                    {
                        return System.Math.Log(num, System.Math.E);
                    }
                    else
                    {
                        numeroDeErrores++;
                        return 0;
                    }
                }
                else
                {
                    numeroDeErrores++;
                    return 0;
                }

            }
            else
            {
                var num = PilaDeEjecucion[0].DelOperacion();
                return System.Math.Exp(num);

            }
        }

        public double mtdPorSentaje()
        {
            Debug.Log("%");
            var op = PilaDeEjecucion[0].tipOperacion;

            if (MtdHayNumerosIngresados())
            {
                PilaDeEjecucion.RemoveAt(0);
                if (tipo.parentesisDer == OperacionAnterior)
                {
                    double num = mtdresolverOpUnitarias(op, valor);
                    // Debug.Log("EL resultado del exp:"+ num );
                    return num;
                }
                return 0;
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        public double mtdFactorial()
        {
            Debug.Log("!");
            var op = PilaDeEjecucion[0].tipOperacion;

            if (MtdHayNumerosIngresados())
            {
                PilaDeEjecucion.RemoveAt(0);
                if (tipo.parentesisDer == OperacionAnterior)
                {
                    double num = mtdresolverOpUnitarias(op, valor);
                    // Debug.Log("EL resultado del exp:"+ num );
                    return num;
                }
                return 0;
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        public double mtdElevar()
        {
            Debug.Log("^");
            var op = PilaDeEjecucion[0].tipOperacion;
            if (MtdHayNumerosIngresados())
            {
                PilaDeEjecucion.RemoveAt(0);
                if (tipo.parentesisDer == OperacionAnterior)
                {
                    double num = mtdresolverOpUnitarias(op, valor);
                    // Debug.Log("EL resultado del exp:"+ num );
                    return num;
                }
                return 0;
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        public double mtdEXP()
        {
            Debug.Log("E");
            var op = PilaDeEjecucion[0].tipOperacion;
            if (MtdHayNumerosIngresados())
            {
                PilaDeEjecucion.RemoveAt(0);
                if (tipo.parentesisDer == OperacionAnterior)
                {
                    double num = mtdresolverOpUnitarias(op, valor);
                    return num;
                }
                return 0;
            }
            else
            {
                numeroDeErrores++;
                return 0;
            }

        }

        #endregion


    }


}
