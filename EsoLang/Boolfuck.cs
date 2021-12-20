using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EsoLang
{
    public class Boolfuck
    {

        public static string Interpret(string code, string input) =>
            new Boolfuck(input).Parse(code).Execute();

        private Boolfuck(string tape)
        {
            var inputBytes = tape.ToCharArray().Select(letter => (byte)letter).ToArray();
            foreach (var asciiValues in inputBytes)
                Console.WriteLine(asciiValues + " , ");
            input = new BitArray(inputBytes);
            foreach (var bits in input)
                Console.WriteLine("bits in input :" + bits);
            memory = new BitArray(MemorySize);
            pointerIndex = memory.Length / 2;
            output = new BitArray(MemorySize);
        }

        private readonly BitArray input;
        private const int MemorySize = 10000;
        private readonly BitArray memory;
        private int pointerIndex;
        private readonly BitArray output;

        private Boolfuck Parse(string code)
        {
            foreach (var instruction in code)
            {
                Console.Write(instruction);
                instructions.Add(instruction switch
                {
                    '>' => Instruction.MovePointerRight,
                    '<' => Instruction.MovePointerLeft,
                    '+' => Instruction.Flip,
                    '[' => Instruction.JumpOver,
                    ']' => Instruction.JumpBack,
                    ',' => Instruction.ReadBit,
                    ';' => Instruction.OutputBit,
                    _ => Instruction.None
                });
            }
            return this;
        }

        private readonly List<Instruction> instructions = new List<Instruction>();

        public enum Instruction
        {
            MovePointerRight,
            MovePointerLeft,
            Flip,
            JumpOver,
            JumpBack,
            ReadBit,
            OutputBit,
            None
        }

        private string Execute()
        {
            for (instructionIndex = 0; instructionIndex < instructions.Count; instructionIndex++)
            {
                Console.WriteLine("Execute instruction ");
                ExecuteInstruction(instructions[instructionIndex]);
            }
            //Reversing
            var outputBytes = new byte[output.Length / 8];
            foreach (var item in outputBytes)
                Console.WriteLine(" item in outputbytes : " + item);
            output.CopyTo(outputBytes, 0);
            string result = new string(outputBytes.Select(letter => (char)letter).Take((outputLength + 7) / 8)
                              .ToArray());
            Console.WriteLine("result "+ result);
            return result;
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            Console.WriteLine("instruction : " + instruction);
            switch (instruction)
            {
                case Instruction.Flip:
                    Console.WriteLine("flip");
                    memory[pointerIndex] = !memory[pointerIndex];
                    break;
                case Instruction.ReadBit:
                    Console.WriteLine("ReadBit");
                    memory[pointerIndex] = inputPosition < input.Length && input[inputPosition++];
                    break;
                case Instruction.OutputBit:
                    Console.WriteLine("outputbit");
                    output[outputLength++] = memory[pointerIndex];
                    break;
                case Instruction.MovePointerRight:
                    Console.WriteLine("Move to right");
                    pointerIndex++;
                    break;
                case Instruction.MovePointerLeft:
                    Console.WriteLine("Move to left");
                    pointerIndex--;
                    break;
                case Instruction.JumpOver:
                    Console.WriteLine("Jumpover");
                    if (!memory[pointerIndex])
                        JumpToMatchingBracket(1);
                    break;
                case Instruction.JumpBack:
                    Console.WriteLine("Jumpback");
                    if (memory[pointerIndex])
                        JumpToMatchingBracket(-1);
                    break;
            }
        }

        private int inputPosition;
        private int instructionIndex;
        private int outputLength;

        private void JumpToMatchingBracket(int direction)
        {
            int numberOfBrackets = direction;
            for (instructionIndex += direction; instructionIndex >= 0; instructionIndex += direction)
            {
                if (instructions[instructionIndex] == Instruction.JumpBack)
                    numberOfBrackets--;
                if (instructions[instructionIndex] == Instruction.JumpOver)
                    numberOfBrackets++;
                if (numberOfBrackets == 0)
                    break;
            }
        }
    }
}

