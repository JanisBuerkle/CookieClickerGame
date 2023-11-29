using System.Collections.Generic;

namespace RPG;

public class Prop
{
    public class Person
    {
        public double Strength { get; set; }
        public double Luck { get; set; }
        public double Health { get; set; }
    }

    public Person Peter { get; set; }
    public Person Hans { get; set; }
    public Person Max { get; set; }

    public Prop()
    {
        Peter = new Person();
        Hans = new Person();
        Max = new Person();
    }
}