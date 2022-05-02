using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using calculadora;
using System.Globalization;

public class ClsClaculadoraFrente : MonoBehaviour {

    #region members
    /// <summary>
    /// texto que muestra el resulta de la calculadora 
    /// </summary>
    public Text textoCalculadora;
    /// <summary>
    /// 
    /// </summary>
    public Text PantallaPrinsipal;

    /// <summary>
    /// numero pequeño del valor
    /// </summary>
    public Text textoValorGuardado;

    public Text Rad;

    public Text Inv;


    public clsCalculadora2 backCalculadora;

    /// <summary>
    /// Valor actual de la calculadora
    /// </summary>
    public double valor;

    /// <summary>
    /// Valor actual de la calculadora en texto
    /// </summary>
    public string valorTexto;

    /// <summary>
    /// cantidad maxima de digitos permidos en la calculadora
    /// </summary>
    const int maxDigitos = 12;

    /// <summary>
    /// para saber si el valor de la calculadora tiene punto
    /// </summary>
    bool tienePunto = false;

    /// <summary>
    /// valor de la calculadora guardado que se usa para operar el valor actual con el guardado
    /// </summary>
    double valorGuardado;


    /// <summary>
    /// operacion actual de la calculadora
    /// </summary>
    private OpAnterior opPasada = OpAnterior.Ninguna;

    private opIngresada opINGResada = opIngresada.ninguna;

    //numero de parentesis izquierdos puestos
    private int NUmparentesis;
    #endregion

    #region enum

    /// <summary>
    /// enum con las posibles operaciones de la calculadora
    /// </summary>
   
    enum OpAnterior { ANS, Ninguna, igual, Operacion, nuemor, error, parentesis, parenDer, pi, Euler }

    enum opIngresada {suma, resta, multiplicacion,division,igual,numero, parenIzquierdo,parenDerecho,ninguna  }

    #endregion


    # region monoBehaviour

    void Start() {
        Reiniciar();

    }


    void Update() {

        if (backCalculadora.Rad)
            Rad.text = "Rad";
        else
            Rad.text = "Deg";

        if (backCalculadora.lnv)
            Inv.text = "Inv";
        else
            Inv.text = "";



    }

    #endregion

    #region private Methods

    /// <summary>
    /// metodo que reinicia el valor ingresado si despuesde de un igual se oprime un numero
    /// </summary>
    private void mtdReIniciarNumero()
    {
        if (opPasada == OpAnterior.igual || opPasada == OpAnterior.error)
        {
            opPasada = OpAnterior.nuemor;
            valorTexto = "";
            PantallaPrinsipal.text = "";
            textoValorGuardado.text = valorGuardado.ToString();
        }
    }

    /// <summary>
    /// guarda el numero previamente digitado en la pila
    /// </summary>
    private void mtdGuardarNumero()
    {
        if (opPasada == OpAnterior.igual)
        {

            Operacion opt = new Operacion();
            opt.valorNumerico = valorGuardado;
            opt.tipOperacion = Operacion.TiposDeOperacion.numero;
            opt.jerarquía = Operacion.JerarquíaOpreacion.numero;
            opt.DelOperacion = backCalculadora.mtdNumero;
            backCalculadora.PilaDeEjecucion.Add(opt);

        }
        else
        {
            if (opPasada != OpAnterior.ANS && opPasada != OpAnterior.Euler && opPasada != OpAnterior.pi && opPasada != OpAnterior.parenDer)
            {
                Operacion num = new Operacion();
                num.valorNumerico = System.Convert.ToDouble(textoCalculadora.text);
                num.tipOperacion = Operacion.TiposDeOperacion.numero;
                num.jerarquía = Operacion.JerarquíaOpreacion.numero;
                num.DelOperacion = backCalculadora.mtdNumero;
                backCalculadora.PilaDeEjecucion.Add(num);
            }
        }
        tienePunto = false;
    }

    /// <summary>
    /// Reinicia los valores de la calculadora
    /// </summary>
    private void Reiniciar()
    {
        PantallaPrinsipal.text = "";
        valorGuardado = 0;
        valor = 0;
        valorTexto = "0";
        textoCalculadora.text = valorTexto;
        textoValorGuardado.text = "";
        tienePunto = false;
        opPasada = OpAnterior.Ninguna;

    }

    /// <summary>
    /// Actualiza el valor en la pantalla de la calculadora
    /// </summary>
    private void ActualizarValor()
    {
        if (valorTexto.Length > maxDigitos)
        {
            valorTexto = valorTexto.Substring(0, maxDigitos);
            textoCalculadora.text = valorTexto;
        }
        else {
            
            bool puntoAlFinal = false;

            if (valorTexto.Length > 0 && tienePunto && valorTexto[valorTexto.Length - 1] == '.')
            {
                puntoAlFinal = true;
            }
/*
            if (valorTexto == "0.")
                valorTexto = "0.";
            else if (valorTexto == "0.0")
                valorTexto = "0.0";
            else if (valorTexto == "0.00")
                valorTexto = "0.00";
            else if (valorTexto == "0.000")
                valorTexto = "0.000";
            else if (valorTexto == "0.0000")
                valorTexto = "0.0000";
            else if (valorTexto == "0.00000")
                valorTexto = "0.00000";
            else if (valorTexto == "0.000000")
                valorTexto = "0.000000";
            else if (valorTexto == "0.0000000")
                valorTexto = "0.0000000";
            else if (valorTexto == "0.00000000")
                valorTexto = "0.00000000";
            else if (valorTexto == "0.000000000")
                valorTexto = "0.000000000";
            else if (valorTexto == "0.0000000000")
                valorTexto = "0.0000000000";
            else if (valorTexto == "0.00000000000")
                valorTexto = "0.000000000000";
            else
            {
                valor = double.Parse(valorTexto);
                if (valor < 1)
                    valorTexto = valor.ToString("0.################");
                else
                {
                    string valorStr = valor.ToString();
                    valorTexto = puntoAlFinal ? valorStr + "." : valorStr;
                }
            }*/
            
            textoCalculadora.text = valorTexto;
        }
    }


    /// <summary>
    /// convierte los numeros al formato cientifico 
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private string mtdDarFormato(double num) {
        bool periodico;
        Debug.Log("");
        if (num > 9999999999)
        {
            Debug.Log("1");
            return num.ToString("0.######e+0", CultureInfo.InvariantCulture); 
        }

        if (num <-9999999999)
        {
            Debug.Log("2");
            return num.ToString("0.######e+0", CultureInfo.InvariantCulture);
        }
       
        if (num > 0)
        {
            Debug.Log("3");
            periodico = MtdDesbordeDecimal(num);
        }
        else
        {
            Debug.Log("4");

            periodico = MtdDesbordeDecimal(num*-1);
        }
        Debug.Log(periodico);
       /* if (periodico)
        {
            Debug.Log("5");

            return num.ToString("0.0000000000", CultureInfo.InvariantCulture);
        }*/
        return num.ToString();

    }


    /// <summary>
    /// comprueba si sobre pasa el numero de desimales para poner notacion cientifica 
    /// </summary>
    /// <param name="d"></param>
    /// <param name="positivo"></param>
    /// <returns></returns>
    private bool MtdDesbordeDecimal(double d)
    {

        var resultado=d * 1000000000000 - System.Math.Floor(d * 1000000000000);
        Debug.Log(resultado);
        if (resultado == 0)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    /// <summary>
    /// cambia la ultima operacion ingresada
    /// </summary>
    /// <param name="partes">numero de elementos que contiene la operacion ejemplo cos( dos elementos por la operacion cos y su (  </param>
    private void mtdCambiarOpEnLaPila( int partes)
    {
        var tam = backCalculadora.PilaDeEjecucion.Count;
        backCalculadora.PilaDeEjecucion.RemoveAt(tam);

    }

    #endregion

    #region public Methods

    /// <summary>
    /// Precion del boton 0
    /// </summary>
    public void Btn0()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "0";
        PantallaPrinsipal.text += "0";
        ActualizarValor();
    }

    public void Btn1()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "1";
        PantallaPrinsipal.text += "1";
        ActualizarValor();

    }

    public void Btn2()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "2";
        PantallaPrinsipal.text += "2";
        ActualizarValor();
    }

    public void Btn3()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "3";
        PantallaPrinsipal.text += "3";
        ActualizarValor();
    }

    public void Btn4()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "4";
        PantallaPrinsipal.text += "4";
        ActualizarValor();
    }

    public void Btn5()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "5";
        PantallaPrinsipal.text += "5";
        ActualizarValor();
    }

    public void Btn6()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "6";
        PantallaPrinsipal.text += "6";
        ActualizarValor();
    }

    public void Btn7()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "7";
        PantallaPrinsipal.text += "7";
        ActualizarValor();
    }

    public void Btn8()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "8";
        PantallaPrinsipal.text += "8";
        ActualizarValor();
    }

    public void Btn9()
    {
        opINGResada = opIngresada.numero;
        mtdReIniciarNumero();
        valorTexto += "9";
        PantallaPrinsipal.text += "9";
        ActualizarValor();

    }

    public void BtnPunto()
    {
        if (opINGResada == opIngresada.numero&& opPasada!=OpAnterior.ANS)
        {
            if (!tienePunto)
            {
                tienePunto = true;
                PantallaPrinsipal.text += ".";
                valorTexto += ".";
                ActualizarValor();
            }
        }

    }


    public void btnigual()
    {
       if (PantallaPrinsipal.text!=""&& (opPasada== OpAnterior.nuemor|| opPasada == OpAnterior.parenDer||opINGResada==opIngresada.numero))
       {
            if (opPasada != OpAnterior.parenDer && opPasada != OpAnterior.ANS) {
                // Debug.Log("Este es el problema" +opPasada);
                Operacion num = new Operacion();

                num.valorNumerico = System.Convert.ToDouble(textoCalculadora.text);
                num.tipOperacion = Operacion.TiposDeOperacion.numero;
                num.jerarquía = Operacion.JerarquíaOpreacion.numero;
                num.DelOperacion = backCalculadora.mtdNumero;
                backCalculadora.PilaDeEjecucion.Add(num);
            }

            textoValorGuardado.text = textoCalculadora.text;

            textoValorGuardado.text = PantallaPrinsipal.text;
            textoCalculadora.text = "";
            valorTexto = "";

            var resultado = backCalculadora.MtdIgual();
            if (backCalculadora.numeroDeErrores == 0)
            {
                tienePunto = false;
                opPasada = OpAnterior.igual;
                PantallaPrinsipal.text = mtdDarFormato(resultado);
                valorGuardado = resultado;
                backCalculadora.MtdReIniciarPila();
                opINGResada = opIngresada.igual;
            }
            else {
                tienePunto = false;
                opPasada = OpAnterior.error;
                PantallaPrinsipal.text = "!#*Error#!";
                backCalculadora.MtdReIniciarPila();
                opINGResada = opIngresada.igual;
            }
       }

    }


    public void Btnsuma() {

        if (opINGResada == opIngresada.numero||opINGResada == opIngresada.igual)
        {
            if (opPasada != OpAnterior.error) {
                PantallaPrinsipal.text += "+";
                mtdGuardarNumero();

                opPasada = OpAnterior.Operacion;

                textoCalculadora.text = "";
                valorTexto = "";

                Operacion op = new Operacion();
                op.valorNumerico = 0;
                op.tipOperacion = Operacion.TiposDeOperacion.operacion;
                op.jerarquía = Operacion.JerarquíaOpreacion.suma;
                op.DelOperacion = backCalculadora.mtdSuma;
                backCalculadora.PilaDeEjecucion.Add(op);

                opINGResada = opIngresada.suma;
            }
        }


    }

    public void BtnResta()
    {
        Debug.Log(opINGResada);
        if (opINGResada != opIngresada.resta)
        {
           

            if (opPasada != OpAnterior.error)
            {
                PantallaPrinsipal.text += "-";

                if (opPasada == OpAnterior.igual)
                {
                    Operacion opt = new Operacion();
                    opt.valorNumerico = valorGuardado;
                    opt.tipOperacion = Operacion.TiposDeOperacion.numero;
                    opt.jerarquía = Operacion.JerarquíaOpreacion.numero;
                    opt.DelOperacion = backCalculadora.mtdNumero;
                    backCalculadora.PilaDeEjecucion.Add(opt);
                    tienePunto = false;
                }
                else
                {
                    /*  if(opPasada!=OpAnterior.ANS&& opPasada != OpAnterior.Ninguna && opPasada != OpAnterior.parenDer && opPasada!= OpAnterior.pi && opPasada != OpAnterior.Euler)
                      {*/
                    if (textoCalculadora.text != "" && (opPasada == OpAnterior.parentesis || opPasada == OpAnterior.Operacion || opPasada != OpAnterior.ANS))
                    {

                        tienePunto = false;
                        //mtdGuardarNumero();
                        Operacion num = new Operacion();
                        num.valorNumerico = System.Convert.ToDouble(textoCalculadora.text);
                        num.tipOperacion = Operacion.TiposDeOperacion.numero;
                        num.jerarquía = Operacion.JerarquíaOpreacion.numero;
                        num.DelOperacion = backCalculadora.mtdNumero;
                        backCalculadora.PilaDeEjecucion.Add(num);
                    }

                    //}

                }

                textoCalculadora.text = "";
                valorTexto = "";
               
                opPasada = OpAnterior.Operacion;
                Operacion op = new Operacion();
                op.valorNumerico = 0;
                op.tipOperacion = Operacion.TiposDeOperacion.menos;
                op.jerarquía = Operacion.JerarquíaOpreacion.resta;
                op.DelOperacion = backCalculadora.mtdRestar;
                backCalculadora.PilaDeEjecucion.Add(op);
                opINGResada = opIngresada.resta;
            }
        }
    }

    public void mtdMulti()
    {
        if (opINGResada == opIngresada.numero || opINGResada == opIngresada.igual)
        {
            if (opPasada != OpAnterior.error)
            {
                PantallaPrinsipal.text += "X";
                mtdGuardarNumero();

                opPasada = OpAnterior.Operacion;

                textoCalculadora.text = "";
                valorTexto = "";

                Operacion op = new Operacion();
                op.valorNumerico = 0;
                op.tipOperacion = Operacion.TiposDeOperacion.operacion;
                op.jerarquía = Operacion.JerarquíaOpreacion.multiplicacion;
                op.DelOperacion = backCalculadora.mtdMultiplicacion;
                backCalculadora.PilaDeEjecucion.Add(op);
                opINGResada = opIngresada.multiplicacion;
            }
        }

    }

    public void mtdDivision()
    {
        if (opINGResada == opIngresada.numero || opINGResada == opIngresada.igual)
        {
            if (opPasada != OpAnterior.error)
            {
                mtdGuardarNumero();

                opPasada = OpAnterior.Operacion;


                PantallaPrinsipal.text += "/";

                textoCalculadora.text = "";
                valorTexto = "";

                Operacion op = new Operacion();
                op.valorNumerico = 0;
                op.tipOperacion = Operacion.TiposDeOperacion.operacion;
                op.jerarquía = Operacion.JerarquíaOpreacion.division;
                op.DelOperacion = backCalculadora.mtdDivision;
                backCalculadora.PilaDeEjecucion.Add(op);
                opINGResada = opIngresada.division;
            }
        }
    }

    /// <summary>
    /// borra todo en al pantalla principal y re inicia la pila de solucion de la calculadora
    /// </summary>
    public void mtdBotonCE() {

        opINGResada = opIngresada.ninguna;
        opPasada = OpAnterior.Ninguna;

        PantallaPrinsipal.text = "";

        textoCalculadora.text = "";
        valorTexto = "";
        valor = 0;
        backCalculadora.MtdReIniciarPila();
        tienePunto = false;
    }


    public void mtdBotonANS() {

        if (opINGResada != opIngresada.numero&&opINGResada != opIngresada.igual) {
            opPasada = OpAnterior.ANS;
            opINGResada = opIngresada.numero;
            valorTexto = "";
            if (!backCalculadora.lnv)
            {
                PantallaPrinsipal.text += mtdDarFormato(valorGuardado);

                Operacion opt = new Operacion();
                opt.valorNumerico = valorGuardado;
                opt.tipOperacion = Operacion.TiposDeOperacion.numero;
                opt.jerarquía = Operacion.JerarquíaOpreacion.numero;
                opt.DelOperacion = backCalculadora.mtdNumero;
                backCalculadora.PilaDeEjecucion.Add(opt);
            }
            else {

                var Rnd = UnityEngine.Random.Range(0f, 1f);
                var RndTxt = Rnd.ToString("0.###", CultureInfo.InvariantCulture);

                PantallaPrinsipal.text += RndTxt;
                Operacion opt = new Operacion();
                opt.valorNumerico = Convert.ToDouble(RndTxt);
                opt.tipOperacion = Operacion.TiposDeOperacion.numero;
                opt.jerarquía = Operacion.JerarquíaOpreacion.numero;
                opt.DelOperacion = backCalculadora.mtdNumero;
                backCalculadora.PilaDeEjecucion.Add(opt);
            }
        }
    }


    public void btnParentesisDerecho() {

        if (opINGResada == opIngresada.numero || opINGResada == opIngresada.igual)
        {
            if (opPasada == OpAnterior.parentesis || opPasada == OpAnterior.Operacion)
            {
                Debug.Log("ingrese el numero");

                Operacion num = new Operacion();
                num.valorNumerico = System.Convert.ToDouble(textoCalculadora.text);
                num.tipOperacion = Operacion.TiposDeOperacion.numero;
                num.jerarquía = Operacion.JerarquíaOpreacion.numero;
                num.DelOperacion = backCalculadora.mtdNumero;
                backCalculadora.PilaDeEjecucion.Add(num);
                
                textoCalculadora.text = "";
                valorTexto = "";

            }

            opPasada = OpAnterior.parenDer;
            PantallaPrinsipal.text += ")";
            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisDerecho;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisDe;
            auxv.DelOperacion = backCalculadora.mtdParentesisDerecho;
            backCalculadora.PilaDeEjecucion.Add(auxv);

        }

    }


    public void btnParentesisIzquierdo() {

        if (opINGResada != opIngresada.numero)
        {
            mtdReIniciarNumero();
            PantallaPrinsipal.text += "(";
            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
            backCalculadora.PilaDeEjecucion.Add(auxv);
            opPasada = OpAnterior.parentesis;
            opINGResada = opIngresada.parenIzquierdo;
        }

    }


    public void btnSEN() {

        if (opINGResada != opIngresada.numero)
        {
            mtdReIniciarNumero();

            if (backCalculadora.lnv)
            {
                PantallaPrinsipal.text += "arcSin(";
            }
            else
            {
                PantallaPrinsipal.text += "sin(";
            }

            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxvt.jerarquía = Operacion.JerarquíaOpreacion.OpUnitaria;
            auxvt.DelOperacion = backCalculadora.mtdSen;
            backCalculadora.PilaDeEjecucion.Add(auxvt);


            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
            auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
            backCalculadora.PilaDeEjecucion.Add(auxv);

            opPasada = OpAnterior.parentesis;
            textoCalculadora.text = "";
            valorTexto = "";
            tienePunto = false;
        }

    }



    public void btnCos() {

        if (opINGResada != opIngresada.numero)
        {
            mtdReIniciarNumero();
            if (backCalculadora.lnv)
            {
                PantallaPrinsipal.text += "arccos(";
            }
            else
            {
                PantallaPrinsipal.text += "cos(";
            }

            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxvt.DelOperacion = backCalculadora.mtdcos;
            auxvt.jerarquía= Operacion.JerarquíaOpreacion.OpUnitaria;
            backCalculadora.PilaDeEjecucion.Add(auxvt);


            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
            backCalculadora.PilaDeEjecucion.Add(auxv);

            opPasada = OpAnterior.parentesis;
            textoCalculadora.text = "";
            valorTexto = "";
            tienePunto = false;
        }


    }


    public void btnTan()
    {
        if (opINGResada != opIngresada.numero)
        {
            mtdReIniciarNumero();
            if (backCalculadora.lnv)
            {
                PantallaPrinsipal.text += "arctan(";
            }
            else
            {
                PantallaPrinsipal.text += "tan(";
            }

            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxvt.DelOperacion = backCalculadora.mtdtan;
            auxvt.jerarquía = Operacion.JerarquíaOpreacion.OpUnitaria;
            backCalculadora.PilaDeEjecucion.Add(auxvt);


            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
            backCalculadora.PilaDeEjecucion.Add(auxv);

            opPasada = OpAnterior.parentesis;
            textoCalculadora.text = "";
            valorTexto = "";
            tienePunto = false;
        }
    }


    public void btnPI() {
        if (opINGResada != opIngresada.numero)
        {
            mtdReIniciarNumero();
            opPasada = OpAnterior.pi;
            valorTexto = "";
            PantallaPrinsipal.text += "P";
            opINGResada = opIngresada.numero;
            Operacion opt = new Operacion();
            opt.valorNumerico = 3.14159265359;
            opt.tipOperacion = Operacion.TiposDeOperacion.numero;
            opt.jerarquía = Operacion.JerarquíaOpreacion.numero;
            opt.DelOperacion = backCalculadora.mtdNumero;
            backCalculadora.PilaDeEjecucion.Add(opt);
            tienePunto = false;
            opINGResada = opIngresada.numero;
        }
    }

    public void btnEuler()
    {
        if (opINGResada != opIngresada.numero)//&& opINGResada != opIngresada.igual)
        {
            mtdReIniciarNumero();
            opPasada = OpAnterior.Euler;
            valorTexto = "";
            PantallaPrinsipal.text += 2.71828182846;
            opINGResada = opIngresada.numero;
            Operacion opt = new Operacion();
            opt.valorNumerico = 2.71828182846;
            opt.tipOperacion = Operacion.TiposDeOperacion.numero;
            opt.jerarquía = Operacion.JerarquíaOpreacion.numero;
            opt.DelOperacion = backCalculadora.mtdNumero;
            backCalculadora.PilaDeEjecucion.Add(opt);
            tienePunto = false;
            opINGResada = opIngresada.numero;
        }
    }


    public void BtnRad() {
        backCalculadora.Rad = true;
    }

    public void btnDeg() {
        backCalculadora.Rad = false;
    }

    public void btnInv() {
        if (backCalculadora.lnv)
            backCalculadora.lnv = false;
        else
            backCalculadora.lnv = true;
    }

    public void btnLog()
    {
        if (opINGResada != opIngresada.numero)
        {
            mtdReIniciarNumero();
            if (backCalculadora.lnv)
            {
                PantallaPrinsipal.text += "10^(";
            }
            else
            {
                PantallaPrinsipal.text += "log(";
            }

            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxvt.jerarquía = Operacion.JerarquíaOpreacion.OpUnitaria;
            auxvt.DelOperacion = backCalculadora.mtdlog;
            backCalculadora.PilaDeEjecucion.Add(auxvt);


            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
            backCalculadora.PilaDeEjecucion.Add(auxv);

            opPasada = OpAnterior.parentesis;
            textoCalculadora.text = "";
            valorTexto = "";
            tienePunto = false;
        }
    }

    public void btnLn() {
        if (opINGResada != opIngresada.numero)
        {
            mtdReIniciarNumero();
            if (backCalculadora.lnv)
            {
                PantallaPrinsipal.text += "e^(";
            }
            else
            {
                PantallaPrinsipal.text += "ln(";
            }


            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxvt.jerarquía = Operacion.JerarquíaOpreacion.OpUnitaria;
            auxvt.DelOperacion = backCalculadora.mtdln;
            backCalculadora.PilaDeEjecucion.Add(auxvt);


            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
            auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
            backCalculadora.PilaDeEjecucion.Add(auxv);

            opPasada = OpAnterior.parentesis;
            textoCalculadora.text = "";
            valorTexto = "";
            tienePunto = false;
        }
    }

    public void btnPorsentaje()
    {

        if (opINGResada == opIngresada.numero||opINGResada == opIngresada.igual)
        {
            mtdGuardarNumero();
            PantallaPrinsipal.text += "%";

            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.porsentaje;
            auxvt.jerarquía = Operacion.JerarquíaOpreacion.OpMod;
            auxvt.DelOperacion = backCalculadora.mtdPorSentaje;
            backCalculadora.PilaDeEjecucion.Add(auxvt);

            opPasada = OpAnterior.parenDer;

            textoCalculadora.text = "";
            valorTexto = "";
        }


    }

    public void btnRaiz()
    {
       
        if (backCalculadora.lnv)
        {
            if (opINGResada == opIngresada.numero|| opINGResada == opIngresada.igual)
            {
                PantallaPrinsipal.text += "^2";

                mtdGuardarNumero();
                opPasada = OpAnterior.parenDer;
                Operacion auxvt = new Operacion();
                auxvt.valorNumerico = 0;
                auxvt.tipOperacion = Operacion.TiposDeOperacion.alCuadrado;
                auxvt.jerarquía = Operacion.JerarquíaOpreacion.OpMod;
                auxvt.DelOperacion = backCalculadora.mtdRaiz;
                backCalculadora.PilaDeEjecucion.Add(auxvt);

                textoCalculadora.text = "";
                valorTexto = "";
                tienePunto = false;
            }

        }
        else
        {
            if (opINGResada != opIngresada.numero )
            {
                mtdReIniciarNumero();
                PantallaPrinsipal.text += "R(";

                Operacion auxvt = new Operacion();
                auxvt.valorNumerico = 0;
                auxvt.tipOperacion = Operacion.TiposDeOperacion.Unitarias;
                auxvt.jerarquía = Operacion.JerarquíaOpreacion.OpUnitaria;
                auxvt.DelOperacion = backCalculadora.mtdRaiz;
                backCalculadora.PilaDeEjecucion.Add(auxvt);


                Operacion auxv = new Operacion();
                auxv.valorNumerico = 0;
                auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
                auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
                auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
                backCalculadora.PilaDeEjecucion.Add(auxv);
                opPasada = OpAnterior.parentesis;

                textoCalculadora.text = "";
                valorTexto = "";
                tienePunto = false;
            }

        }

    }

    public void btnFactorial()
    {
        if (opINGResada == opIngresada.numero||opINGResada == opIngresada.igual)
        {
            mtdGuardarNumero();

            PantallaPrinsipal.text += "!";

            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.factorial;
            auxvt.jerarquía = Operacion.JerarquíaOpreacion.OpMod;
            auxvt.DelOperacion = backCalculadora.mtdFactorial;
            backCalculadora.PilaDeEjecucion.Add(auxvt);
            opPasada = OpAnterior.parenDer;
            textoCalculadora.text = "";
            valorTexto = "";
        }
        tienePunto = false;

    }


    public void BtnElevado() {

        if (opINGResada == opIngresada.numero|| opINGResada == opIngresada.igual)
        {
            mtdGuardarNumero();

            PantallaPrinsipal.text += "^(";

            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.elevado;
            auxvt.jerarquía = Operacion.JerarquíaOpreacion.opdoble;
            auxvt.DelOperacion = backCalculadora.mtdElevar;
            backCalculadora.PilaDeEjecucion.Add(auxvt);


            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
            auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
            backCalculadora.PilaDeEjecucion.Add(auxv);

            opPasada = OpAnterior.parentesis;
            textoCalculadora.text = "";
            valorTexto = "";
            tienePunto = false;
        }

    }


    public void btnEXp(){

        if (opINGResada == opIngresada.numero || opINGResada == opIngresada.igual)
        {
            mtdGuardarNumero();
            PantallaPrinsipal.text += "E(";

            Operacion auxvt = new Operacion();
            auxvt.valorNumerico = 0;
            auxvt.tipOperacion = Operacion.TiposDeOperacion.Exp;
            auxvt.jerarquía = Operacion.JerarquíaOpreacion.opdoble;
            auxvt.DelOperacion = backCalculadora.mtdEXP;
            backCalculadora.PilaDeEjecucion.Add(auxvt);

            Operacion auxv = new Operacion();
            auxv.valorNumerico = 0;
            auxv.tipOperacion = Operacion.TiposDeOperacion.parentesisIzquierdo;
            auxv.jerarquía = Operacion.JerarquíaOpreacion.parentesisIz;
            auxv.DelOperacion = backCalculadora.mtdParentesisIzquierdo;
            backCalculadora.PilaDeEjecucion.Add(auxv);

            opPasada = OpAnterior.Operacion;

            textoCalculadora.text = "";
            valorTexto = "";
            tienePunto = false;
        }

    }

    #endregion



}

