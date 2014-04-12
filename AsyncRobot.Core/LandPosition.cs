namespace AsyncRobot.Core
{
    public class LandPosition
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public LandObject Value { get; private set; }

        public LandPosition(int x, int y) : this(x, y, null){}

        public LandPosition(int x, int y, LandObject value)
        {
            this.X = x;
            this.Y = y;
            this.Value = value;
        }

        public void SetValue(LandObject value)
        {
            this.Value = value;
        }
    }
}
