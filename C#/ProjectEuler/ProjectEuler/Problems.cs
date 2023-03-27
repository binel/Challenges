using System.Numerics;

namespace ProjectEuler
{
    public class Problems
    {
        [Test]
        public void Problem001()
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
        public void Problem002()
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
        public void Problem003()
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
        public void Problem004()
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

        [Test]
        public void Problem005()
        {
            int smallest = 21;
            bool found = false;
            while (true)
            {
                smallest++;
                for (int i = 2; i < 21; i++)
                {
                    if (smallest % i != 0)
                    {
                        break;
                    }

                    if (i == 20)
                    {
                        found = true;
                    }
                }

                if (found)
                {
                    break;
                }
            }
            Assert.That(smallest, Is.EqualTo(232792560));
        }

        [Test]
        public void Problem006()
        {
            int sumOfSquares = 0;
            int sumSquared = 0;
            Enumerable.Range(1, 100).ToList().ForEach(s => sumOfSquares += s * s);
            Enumerable.Range(1, 100).ToList().ForEach(s => sumSquared += s);
            sumSquared *= sumSquared;
            var result = sumSquared - sumOfSquares;

            Assert.That(result, Is.EqualTo(25164150));
        }

        [Test]
        public void Problem007()
        {
            int primeCount = 1;
            List<int> primes = new List<int> { 2 };
            int lastPrime = 2;
            int num = 2;
            while (primeCount != 10_001)
            {
                num++;
                if (primes.Find(p => num % p == 0) == 0)
                {
                    primes.Add(num);
                    primeCount++;
                    lastPrime = num;
                }
            }
            Assert.That(lastPrime, Is.EqualTo(104743));
        }

        [Test]
        public void Problem008()
        {
            var input = "7316717653133062491922511967442657474235534919493496983520312774506326239578318016984801869478851843858615607891129494954595017379583319528532088055111254069874715852386305071569329096329522744304355766896648950445244523161731856403098711121722383113622298934233803081353362766142828064444866452387493035890729629049156044077239071381051585930796086670172427121883998797908792274921901699720888093776657273330010533678812202354218097512545405947522435258490771167055601360483958644670632441572215539753697817977846174064955149290862569321978468622482839722413756570560574902614079729686524145351004748216637048440319989000889524345065854122758866688116427171479924442928230863465674813919123162824586178664583591245665294765456828489128831426076900422421902267105562632111110937054421750694165896040807198403850962455444362981230987879927244284909188845801561660979191338754992005240636899125607176060588611646710940507754100225698315520005593572972571636269561882670428252483600823257530420752963450";

            long greatestProduct = 1;
            int ADJ_SIZE = 13;
            for (int i = ADJ_SIZE - 1; i < input.Length; i++)
            {
                long product = 1;
                for (int j = 0; j < ADJ_SIZE; j++)
                {
                    char c = input[i - j];
                    product *= (int) char.GetNumericValue(c);
                }

                if (product > greatestProduct)
                {
                    greatestProduct = product;
                }
            }

            Assert.That(greatestProduct, Is.EqualTo(23514624000));
        }

        [Test]
        public void Problem009()
        {
            int val = 0;
            for (int a = 1; a < 1000 - 2; a++)
            {
                for (int b = 1; b < 1000 - 2; b++)
                {
                    int c = 1000 - b - a;
                    if (a * a + b * b == c * c)
                    {
                        Console.WriteLine($"{a} {b} {c}");
                        val = a * b * c;
                        break;
                    }
                }
                if (val != 0)
                {
                    break;
                }
            }
            Assert.That(val, Is.EqualTo(31875000));
        }

        [Test]
        public void Problem010()
        {
            List<int> primes = new List<int> { 2 };
            long primeSum = 2;
            int num = 2;
            while (num < 2_000_000)
            {
                num++;
                if (primes.Find(p => num % p == 0) == 0)
                {
                    primes.Add(num);
                    primeSum += num;
                }
            }
            Assert.That(primeSum, Is.EqualTo(142913828922));
        }
    }
}