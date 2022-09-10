namespace Kujou_Karen_Bot.Classes.Commands.Helpers
{
    class Die
    {
        public int count, size, addition, subtraction, multiplication, division;

        public Die(int count, int size, int addition, int subtraction, int multiplication, int division)
        {
            this.count = count;
            this.size = size;
            this.addition = addition;
            this.subtraction = subtraction;
            this.multiplication = multiplication;
            this.division = division;
        }

        override
        public string ToString()
        {
            string baseO = count + " " + size + " ",
                add = addition > 0 ? "+" + addition + " " : "",
                sub = (subtraction > 0) ? "-" + subtraction + " " : "",
                multi = (multiplication > 0) ? "*" + multiplication + " " : "",
                div = (division > 0) ? "/" + division + " " : "",
                output = baseO + add + sub + multi + div;

            return output;

        }
    }
}
