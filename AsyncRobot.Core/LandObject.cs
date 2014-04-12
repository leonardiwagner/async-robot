namespace AsyncRobot.Core
{
    public class LandObject
    {
        public static readonly LandObject WALL = new LandObject('#');
        public static readonly LandObject SPACE = new LandObject(' ');

        private readonly char Name;

        private LandObject(char name)
        {
            this.Name = name;
        }

        public static implicit operator char(LandObject obj)
        {
            return obj.Name;
        }
    }
}
