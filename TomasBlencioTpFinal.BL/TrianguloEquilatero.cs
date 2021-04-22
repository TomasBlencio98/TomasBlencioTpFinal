using System;


namespace TomasBlencioTpFinal.BL
{
    public class TrianguloEquilatero:ICloneable
    {
        //Atributos
        public double Lado { get; set; }
        public string Borde { get; set; }
        public TrianguloEquilatero()
        {

        }
        public TrianguloEquilatero(double lado,string borde)
        {
            Lado = lado;
            Borde = borde;
        }
        public double GetPerimetro()
        {
            return Lado * 3;
        }
        public double GetSuperficie()
        {
            return Math.Sqrt(3) / 3 * Lado;
        }

        public override bool Equals(object obj)
        {
            if (obj==null || !(obj is TrianguloEquilatero))
            {
                return false;
            }
            return this.Lado == ((TrianguloEquilatero)obj).Lado &&
                this.Borde == ((TrianguloEquilatero)obj).Borde;
        }

        public override int GetHashCode()
        {
            return this.Lado.GetHashCode() +
                this.Borde.GetHashCode();
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
