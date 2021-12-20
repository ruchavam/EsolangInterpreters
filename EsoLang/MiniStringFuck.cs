namespace EsoLang
{
    public class MiniStringFuck
    {
        public static string MyFirstInterpreter(string code) =>
            new MiniStringFuck().Parse(code).Execute();

        private MiniStringFuck Parse(string code)
        {
            foreach (var instruction in code)
                Instructions.Add(instruction switch
                {
                    '+' => Instruction.Increase,
                    '.' => Instruction.Output,
                    _ => throw new InvalidInstruction()
                });
            return this;    
        }

        private string Execute()
        {
            byte memory = 0;
            string result = "";
            foreach (var instruction in Instructions)
                if (instruction == Instruction.Increase)
                    memory++;
                else if (instruction == Instruction.Output)
                    result += (char)memory;
            return result;
        }

        List<Instruction> Instructions = new List<Instruction>();

        public class InvalidInstruction : Exception{}

        public enum Instruction
        {
            Increase,
            Output
        }
    }
}