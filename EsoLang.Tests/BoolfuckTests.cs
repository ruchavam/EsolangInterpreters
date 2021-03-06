using NUnit.Framework;
using System;
using System.Text;

namespace EsoLang.Tests
{
    class BoolfuckTests
    {
        [TestCase("", "", "")]
        [TestCase("<", "", "")]
        [TestCase(">", "", "")]
        [TestCase("+", "", "")]
        [TestCase(",", "", "")]
        [TestCase(";", "", "\u0000")]
        [TestCase(";;;+;+;;+;+;+;+;+;+;;+;;+;;;+;;+;+;;+;;;+;;+;+;;+;+;;;;+;+;;+;;;+;;+;+;+;;;;;;;+;+;;+;;;+;+;;;+;+;;;;+;+;;+;;+;+;;+;;;+;;;+;;+;+;;+;;;+;+;;+;;+;+;+;;;;+;+;;;+;+;+;", "a", "Hello, world!\n")]
        [TestCase(">,>,>,>,>,>,>,>,<<<<<<<[>] +<[+<] >>>>>>>>>[+] +<<<<<<<< +[> +] <[<] >>>>>>>>>[+<<<<<<<<[>] +<[+<] >>>>>>>>> +<<<<<<<< +[> +] <[<] >>>>>>>>>[+] <<<<<<<<;>;>;>;>;>;>;>;<<<<<<<,>,>,>,>,>,>,>,<<<<<<<[>] +<[+<] >>>>>>>>>[+] +<<<<<<<< +[> +] <[<] >>>>>>>>>]<[+<]", "Codewars\u00ff", "Codewars")]
        [TestCase(">,>,>,>,>,>,>,>,>+<<<<<<<<+[>+]<[<]>>>>>>>>>[+<<<<<<<<[>]+<[+<]>;>;>;>;>;>;>;>;>+<<<<<<<<+[>+]<[<]>>>>>>>>>[+<<<<<<<<[>]+<[+<]>>>>>>>>>+<<<<<<<<+[>+]<[<]>>>>>>>>>[+]+<<<<<<<<+[>+]<[<]>>>>>>>>>]<[+<]>,>,>,>,>,>,>,>,>+<<<<<<<<+[>+]<[<]>>>>>>>>>]<[+<]", "Codewars", "Codewars")]
        [TestCase(">,>,>,>,>,>,>,>,>>,>,>,>,>,>,>,>,<<<<<<<<+<<<<<<<<+[>+]<[<]>>>>>>>>>[+<<<<<<<<[>]+<[+<]>>>>>>>>>>>>>>>>>>+<<<<<<<<+[>+]<[<]>>>>>>>>>[+<<<<<<<<[>]+<[+<]>>>>>>>>>+<<<<<<<<+[>+]<[<]>>>>>>>>>[+]>[>]+<[+<]>>>>>>>>>[+]>[>]+<[+<]>>>>>>>>>[+]<<<<<<<<<<<<<<<<<<+<<<<<<<<+[>+]<[<]>>>>>>>>>]<[+<]>>>>>>>>>>>>>>>>>>>>>>>>>>>+<<<<<<<<+[>+]<[<]>>>>>>>>>[+<<<<<<<<[>]+<[+<]>>>>>>>>>+<<<<<<<<+[>+]<[<]>>>>>>>>>[+]<<<<<<<<<<<<<<<<<<<<<<<<<<[>]+<[+<]>>>>>>>>>[+]>>>>>>>>>>>>>>>>>>+<<<<<<<<+[>+]<[<]>>>>>>>>>]<[+<]<<<<<<<<<<<<<<<<<<+<<<<<<<<+[>+]<[<]>>>>>>>>>[+]+<<<<<<<<+[>+]<[<]>>>>>>>>>]<[+<]>>>>>>>>>>>>>>>>>>>;>;>;>;>;>;>;>;<<<<<<<<", "\u0008\u0009", "\u0048")]
        [TestCase(",.", "*", "")]
        public void BoolFuckTets(string code, string input, string expected)
        {
            Assert.AreEqual(Boolfuck.Interpret(code, input), (expected));
        }
    }
}
