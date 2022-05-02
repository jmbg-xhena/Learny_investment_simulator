using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace calculadora
{
    public class Operacion
    {
        // public bool ItisNumuero;

        /// <summary>
        /// campo que se llenan en si es un nuemro de lo contrario llena con 0
        /// </summary>
        public double valorNumerico;
        public TiposDeOperacion tipOperacion;
        public delegate double DelegatetOperacion();
        public DelegatetOperacion DelOperacion;
        /// <summary>
        /// se utiliza paraevaluar la gerarquia 
        /// </summary>
        public JerarquíaOpreacion jerarquía;
        /// <summary>
        /// es la longitud de la cadena que representa al objeto anexado en al pantalla de la calculadora por ejemplo cos = 3
        /// </summary>
       // public int longitudCaracteres;


        public enum JerarquíaOpreacion
        {
            suma,
            resta,
            multiplicacion,
            division,
            parentesisIz,
            parentesisDe,
            OpMod,
            numero,
            opdoble,
            fin,
            OpUnitaria
            
        }

        public enum TiposDeOperacion
        {
            numero,
            operacion,
            parentesisDerecho,
            parentesisIzquierdo,
            menos,
            porsentaje,
            Unitarias,
            factorial,
            elevado,
            alCuadrado,
            Exp,
            fin

        }




    }
}