using System.Globalization;
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

        [Test]
        public void Problem11()
        {
            int[] numbers = new int[400] 
            { 08, 02, 22, 97, 38, 15, 00, 40, 00, 75, 04, 05, 07, 78, 52, 12, 50, 77, 91, 08,
              49, 49, 99, 40, 17, 81, 18, 57, 60, 87, 17, 40, 98, 43, 69, 48, 04, 56, 62, 00,
              81, 49, 31, 73, 55, 79, 14, 29, 93, 71, 40, 67, 53, 88, 30, 03, 49, 13, 36, 65,
              52, 70, 95, 23, 04, 60, 11, 42, 69, 24, 68, 56, 01, 32, 56, 71, 37, 02, 36, 91,
              22, 31, 16, 71, 51, 67, 63, 89, 41, 92, 36, 54, 22, 40, 40, 28, 66, 33, 13, 80,
              24, 47, 32, 60, 99, 03, 45, 02, 44, 75, 33, 53, 78, 36, 84, 20, 35, 17, 12, 50,
              32, 98, 81, 28, 64, 23, 67, 10, 26, 38, 40, 67, 59, 54, 70, 66, 18, 38, 64, 70,
              67, 26, 20, 68, 02, 62, 12, 20, 95, 63, 94, 39, 63, 08, 40, 91, 66, 49, 94, 21,
              24, 55, 58, 05, 66, 73, 99, 26, 97, 17, 78, 78, 96, 83, 14, 88, 34, 89, 63, 72,
              21, 36, 23, 09, 75, 00, 76, 44, 20, 45, 35, 14, 00, 61, 33, 97, 34, 31, 33, 95,
              78, 17, 53, 28, 22, 75, 31, 67, 15, 94, 03, 80, 04, 62, 16, 14, 09, 53, 56, 92,
              16, 39, 05, 42, 96, 35, 31, 47, 55, 58, 88, 24, 00, 17, 54, 24, 36, 29, 85, 57,
              86, 56, 00, 48, 35, 71, 89, 07, 05, 44, 44, 37, 44, 60, 21, 58, 51, 54, 17, 58,
              19, 80, 81, 68, 05, 94, 47, 69, 28, 73, 92, 13, 86, 52, 17, 77, 04, 89, 55, 40,
              04, 52, 08, 83, 97, 35, 99, 16, 07, 97, 57, 32, 16, 26, 26, 79, 33, 27, 98, 66,
              88, 36, 68, 87, 57, 62, 20, 72, 03, 46, 33, 67, 46, 55, 12, 32, 63, 93, 53, 69,
              04, 42, 16, 73, 38, 25, 39, 11, 24, 94, 72, 18, 08, 46, 29, 32, 40, 62, 76, 36,
              20, 69, 36, 41, 72, 30, 23, 88, 34, 62, 99, 69, 82, 67, 59, 85, 74, 04, 36, 16,
              20, 73, 35, 29, 78, 31, 90, 01, 74, 31, 49, 71, 48, 86, 81, 16, 23, 57, 05, 54,
              01, 70, 54, 71, 83, 51, 54, 69, 16, 92, 33, 48, 61, 43, 52, 01, 89, 19, 67, 48
            };
            int greatestProduct = 1;
            int pos = 0;
            // diag right 
            while(pos + 63 < 400)
            {
                if (pos % 20 > 16)
                {
                    pos++;
                    continue;
                }
                int product = numbers[pos] * numbers[pos + 21] * numbers[pos + 42] * numbers[pos + 63];
                greatestProduct = Math.Max(product, greatestProduct);
                pos++;
            }

            pos = 0;
            // diag left 
            while (pos + 57 < 400)
            {
                if (pos % 20 < 4)
                {
                    pos++;
                    continue;
                }
                int product = numbers[pos] * numbers[pos + 19] * numbers[pos + 38] * numbers[pos + 57];
                greatestProduct = Math.Max(product, greatestProduct);
                pos++;
            }

            pos = 0;
            // vert 
            while(pos + 60 < 400)
            {
                int product = numbers[pos] * numbers[pos + 20] * numbers[pos + 40] * numbers[pos + 60];
                greatestProduct = Math.Max(product, greatestProduct);
                pos++;
            }

            pos = 0;
            // right 
            while (pos + 4 < 400)
            {
                if (pos % 20 > 16)
                {
                    pos++;
                    continue;
                }
                int product = numbers[pos] * numbers[pos + 1] * numbers[pos + 2] * numbers[pos + 3];
                greatestProduct = Math.Max(product, greatestProduct);
                pos++;
            }

            pos = 0;
            // left 
            while (pos != 400)
            {
                if (pos % 20 != 0 || pos == 0)
                {
                    pos++;
                    continue;
                }
                int product = numbers[pos] * numbers[pos - 1] * numbers[pos - 2] * numbers[pos - 3];
                greatestProduct = Math.Max(product, greatestProduct);
                pos++;
            }

            Assert.That(greatestProduct, Is.EqualTo(70600674));
        }
    }
}