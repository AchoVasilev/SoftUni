using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    class TomCat : Cat
    {
        private const string defaultGender = "Male";
        public TomCat(string name, int age) : base(name, age, defaultGender)
        {
        }

        public override string ProduceSound()
        {
            return "MEOW";
        }
    }
}
