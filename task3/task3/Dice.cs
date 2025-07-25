namespace task3
{
    internal class Dice
    {
        public List<int> Faces { get; }
        public Dice(List<int> faces)
        {
            this.Faces = faces.ToList();
        }

        public int Roll(int index)
        {
            return Faces[index];
        }

        public override string ToString() 
        {
            return ($"[{string.Join(",", this.Faces)}]");
        }
    }
}
