using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples._02_IntroducingObjectMother
{
  class A
  {
    public static PersonBuilder Person => new PersonBuilder();
  }
}
