using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TomasBlencioTpFinal.BL;

namespace TomasBlencioTpFinal.DL
{
    public class RepositorioTriangulosEquilatero
    {
        public List<TrianguloEquilatero> listaTriangulosEquilateros { get; set; }
            = new List<TrianguloEquilatero>();

        private readonly string _archivo = Environment.CurrentDirectory + "//TriangulosEquilatero.txt";
        private readonly string _archivoBak = Environment.CurrentDirectory + "//TriangulosEquilatero.bak";
        public RepositorioTriangulosEquilatero()
        {
            LeerDatosDelArchivo();
        }

        private void LeerDatosDelArchivo()
        {
            if (File.Exists(_archivo))
            {
                StreamReader lector = new StreamReader(_archivo);
                while (!lector.EndOfStream)
                {
                    string linea = lector.ReadLine();
                    TrianguloEquilatero trianguloEquilatero = ConstruirTriangulo(linea);
                    listaTriangulosEquilateros.Add(trianguloEquilatero);
                }
                lector.Close();
            }
            else
            {
                Console.WriteLine("Error no se pudo encontrar el _archivo");
            }
        }

        private TrianguloEquilatero ConstruirTriangulo(string linea)
        {
            var campos = linea.Split(';');
            return new TrianguloEquilatero
            {
                Lado = double.Parse(campos[0]),
                Borde = campos[1]
            };
        }

        public List<TrianguloEquilatero> GetLista()
        {
            return listaTriangulosEquilateros;
        }
        public void Agregar(TrianguloEquilatero trianguloEquilatero)
        {
            listaTriangulosEquilateros.Add(trianguloEquilatero);
            GuardarDatosEnArchivo(trianguloEquilatero);
        }

        private void GuardarDatosEnArchivo(TrianguloEquilatero trianguloEquilatero)
        {
            StreamWriter escritor = new StreamWriter(_archivo, true);
            string linea = ConstruirLinea(trianguloEquilatero);
            escritor.WriteLine(linea);
            escritor.Close();
        }

        private string ConstruirLinea(TrianguloEquilatero trianguloEquilatero)
        {
            return $"{trianguloEquilatero.Lado};{trianguloEquilatero.Borde}";
        }

        public void Borrar(TrianguloEquilatero trianguloEquilatero)
        {
            listaTriangulosEquilateros.Remove(trianguloEquilatero);
            BorrarDatosDelArchivo(trianguloEquilatero);
        }

        private void BorrarDatosDelArchivo(TrianguloEquilatero trianguloEquilatero)
        {
            StreamReader lector = new StreamReader(_archivo);
            StreamWriter escritor = new StreamWriter(_archivoBak);
            while (!lector.EndOfStream)
            {
                string linea = lector.ReadLine();
                TrianguloEquilatero trianguloDelArchivo = ConstruirTriangulo(linea);
                //una ves q tengo el triangulo tengo que comparar
                if (!trianguloDelArchivo.Equals(trianguloEquilatero))
                {
                    escritor.WriteLine(linea);
                }
            }
            lector.Close();
            escritor.Close();
            File.Delete(_archivo);
            File.Move(_archivoBak, _archivo);
            
        }
       
        public List<TrianguloEquilatero> OrdenarAscendente()
        {
            return listaTriangulosEquilateros.OrderBy(t => t.Lado).ToList();
        }

        public List<TrianguloEquilatero> OrdenarDescendente()
        {
            return listaTriangulosEquilateros.OrderByDescending(t => t.Lado).ToList();
        }

        public void Editar(TrianguloEquilatero trianguloEditado, TrianguloEquilatero trianguloCopia)
        {
            StreamReader lector = new StreamReader(_archivo);
            StreamWriter escritor = new StreamWriter(_archivoBak);
            while (!lector.EndOfStream)
            {
                string linea = lector.ReadLine();
                TrianguloEquilatero triangulo = ConstruirTriangulo(linea);
                if (triangulo.Equals(trianguloCopia))
                {
                    linea = ConstruirLinea(trianguloEditado);
                }
                escritor.WriteLine(linea);
            }
            lector.Close();
            escritor.Close();
            File.Delete(_archivo);
            File.Move(_archivoBak, _archivo);
        }
    }
}
