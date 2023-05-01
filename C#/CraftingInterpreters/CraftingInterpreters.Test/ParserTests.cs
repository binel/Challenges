using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingInterpreters.Test
{
    internal class ParserTests
    {
        [Test]
        public void SimpleAdditionTest()
        {

            Scanner s = new Scanner("1 + 1");
            Parser p = new Parser(s.Scan());

            Assert.DoesNotThrow(() =>
            {
                p.Parse();
            });
        }

        [Test]
        public void BracketTest()
        {
            Scanner s = new Scanner("1 + [1 + 3]");
            Parser p = new Parser(s.Scan());

            Assert.That(p.Parse(), Is.Not.Null);
        }
    }
}
