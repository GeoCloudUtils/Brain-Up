using SimpleExpressionEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;

namespace Assets.Scripts.Other.ExpressionTrees
{
    class ExpressionTEST
    {
        public class MyLibrary
        {
            public MyLibrary()
            {
                pi = Math.PI;
            }

            public double pi { get; private set; }
            public double r { get; set; }

            public double rectArea(double width, double height)
            {
                return width * height;
            }

            public double rectPerimeter(double width, double height)
            {
                return (width + height) * 2;
            }
        }

        public static void Test()
        {
            // Create a library of helper function
            var lib = new MyLibrary();
            lib.r = 10;

            // Create a context that uses the library
            var ctx = new ReflectionContext(lib);

            // Test
            Assert.AreEqual(Parser.Parse("rectArea(10,20)").Eval(ctx), 200);
            Assert.AreEqual(Parser.Parse("rectPerimeter(10,20)").Eval(ctx), 60);
            Assert.AreEqual(Parser.Parse("2 * pi * r").Eval(ctx), 2 * Math.PI * 10);
        }
    }
}
