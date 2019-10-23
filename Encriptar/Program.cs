using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encriptar
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("Ingrese cadena a encriptar: ");
            String cadena = Console.ReadLine();
            String value = Encryptar.Encrypt(cadena);
            Console.WriteLine(value);

            Console.ReadLine();
        }
    }
}
