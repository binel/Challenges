using System.Numerics;

namespace ProjectEuler
{
    public class Problems
    {
        [Test]
        public void Problem1()
        {
            int sum = 0;
            for (int i = 0; i < 1000; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }
            Assert.That(sum, Is.EqualTo(233168));
        }

        [Test]
        public void Problem2()
        {
            int sum = 0;
            for (int f1 = 1, f2 = 2; f2 < 4_000_000; f2 += f1, f1 = f2 - f1)
            {
                if (f2 % 2 == 0)
                {
                    sum += f2;
                }
            }
            Assert.That(sum, Is.EqualTo(4613732));
        }

        [Test]
        public void Problem3()
        {
            BigInteger num = 600851475143;
            int lpf = 0;
            int i = 2;
            while (num != 1)
            {
                i++;
                if (num % i == 0)
                {
                    lpf = i;
                    num /= i;
                }
            }

            Assert.That(lpf, Is.EqualTo(6857));
        }

        [Test]
        public void Problem4()
        {
            int largest = 0;
            for (int lhs = 100; lhs < 1000; lhs++)
            {
                for (int rhs = 100; rhs < 1000; rhs++)
                {
                    int product = lhs * rhs;
                    string productStr = product.ToString();
                    string lhsub, rhsub;
                    if (productStr.Length % 2 != 0) {
                        lhsub = productStr.Substring(0, productStr.Length / 2);
                        rhsub = productStr.Substring(lhsub.Length + 1);

                    }
                    else {
                        lhsub = productStr.Substring(0, productStr.Length / 2);
                        rhsub = productStr.Substring(lhsub.Length);
                    }

                    var ar = rhsub.ToCharArray();
                    Array.Reverse(ar);
                    var rhsubflip = new string(ar);
                    if (lhsub == rhsubflip && product > largest)
                    {
                        largest = product;
                    }
                }
            }

            Assert.That(largest, Is.EqualTo(906609));
        }
    }
}