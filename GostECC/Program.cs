using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace EllipticMath {
    
    static class Program {

        static EllipticCurvePoint point1, point2;
        static BigInteger k;

        [STAThread]
        static void Main() {
            string option = parseInput();
            EllipticCurvePoint result = null;
            switch (option)
            {
                case "+":
                    result = point1 + point2;
                    break;
                case "-":
                    result = point1 - point2;
                    break;
                case "^":
                    result = EllipticCurvePoint.multiply(k, point1);
                    break;
            }

            StreamWriter writer = new StreamWriter("result.txt");
            if (point1.check() && point2.check()) {            
                 writer.WriteLine(result.toString());
            }
            else {
                if (!point1.check()) { writer.WriteLine("Точка 1 не принадлежит эллиптической кривой"); }
                if (!point2.check()) { writer.WriteLine("Точка 2 не принадлежит эллиптической кривой"); }
            }
            writer.Close();
        }

        static string parseInput() {
            StreamReader reader = new StreamReader("pm.txt");
            
            BigInteger p = new BigInteger(reader.ReadLine(), 10);
            
            reader = new StreamReader("E(a,b).txt");
            
            BigInteger a = new BigInteger(reader.ReadLine(), 10);
            BigInteger b = new BigInteger(reader.ReadLine(), 10);

            reader = new StreamReader("operate.txt");

            string operation = reader.ReadLine();
            string[] items = operation.Split(new char[] {' ', ',', '(', ')'}, StringSplitOptions.RemoveEmptyEntries);
            BigInteger x1 = new BigInteger(items[0], 10);
            BigInteger y1 = new BigInteger(items[1], 10);
            point1 = new EllipticCurvePoint(x1, y1, a, b, p);           
            
            BigInteger x2;
            BigInteger y2;
            BigInteger k = null;
            string option = items[2];
            switch (option) {
                case "+":
                case "-":
                    x2 = new BigInteger(items[3], 10);
                    y2 = new BigInteger(items[4], 10);
                    point2 = new EllipticCurvePoint(x2, y2, a, b, p);
                    break;
                case "^":
                    k = new BigInteger(items[3], 10);
                    break;
                default:
                    Console.WriteLine("Нет операции " + option);
                    return null;
            }
            return option;
        }

    }
}
