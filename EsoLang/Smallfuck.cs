using System.Collections.Generic;
using System;
using System.Linq;
namespace EsoLang
{
    public class Smallfuck
    {
        public static string Interpreter(string code, string tape) =>
            new Smallfuck(tape).Parse(code).Execute();
        private Smallfuck(string tape)
        {
            memory = new bool[tape.Length];
            for (int index = 0; index < memory.Length; index++)
                memory[index] = tape[index] == '1';

        }

        private Smallfuck Parse(string code)
        {
            foreach (var instruction in code)
                Instructions.Add(instruction switch
                {
                    '>' => Instruction.MovePointerRight,
                    '<' => Instruction.MovePointerLeft,
                    '*' => Instruction.Flip,
                    '[' => Instruction.JumpOver,
                    ']' => Instruction.JumpBack,
                    _ => Instruction.None
                });
            return this;
        }

        private string Execute()
        {
            for (InstructionPointer = 0; InstructionPointer < Instructions.Count; InstructionPointer++)
            {
                ExecuteInstruction(Instructions[InstructionPointer]);
                if (pointerIndex >= memory.Length  || pointerIndex < 0)
                    break;
            }
            return new string(memory.Select(bit => bit ? '1' : '0').ToArray());
        }

        private void ExecuteInstruction(Instruction instruction)
        {
            switch (instruction)
            {
                case Instruction.MovePointerRight:
                    pointerIndex++;
                    break;
                case Instruction.MovePointerLeft:
                    pointerIndex--;
                    break;
                case Instruction.Flip:
                    if (pointerIndex < memory.Length)
                        memory[pointerIndex] = !memory[pointerIndex];
                    break;
                case Instruction.JumpOver:
                    if (!memory[pointerIndex])
                        JumpOver();
                    break;
                case Instruction.JumpBack:
                    if (memory[pointerIndex])
                        JumpBack();
                    break;
                case Instruction.None:
                    break;
                default:
                    break;
            }
        }
        private void JumpBack()
        {
            JumpToMatchingBracket(-1);
        }

        private void JumpOver()
        {
            JumpToMatchingBracket(1);
        }

        private void JumpToMatchingBracket(int direction)
        {
            int numberOfBrackets = direction;
            for (InstructionPointer += direction; InstructionPointer >= 0; InstructionPointer += direction)
            {
                if (Instructions[InstructionPointer] == Instruction.JumpBack)
                    numberOfBrackets--; 
                if (Instructions[InstructionPointer] == Instruction.JumpOver)
                    numberOfBrackets++;
                if (numberOfBrackets == 0)
                    break;
            }
        }

        List<Instruction> Instructions = new List<Instruction>();
        private int pointerIndex;
        private readonly bool[] memory;

        private int InstructionPointer;

        public enum Instruction
        {
            MovePointerRight,
            MovePointerLeft,
            Flip,
            JumpOver,
            JumpBack,
            None
        }
    }
}
