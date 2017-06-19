#include <iostream>
#include <cmath>
#include <vector>
#include <list>

using std::cin;
using std::cout;
using std::endl;
using std::vector;
using std::list;

// https://www.khanacademy.org/computing/computer-science/cryptography/comp-number-theory/v/sieve-of-eratosthenes-prime-adventure-part-4
void markAllPrimesInArray(vector<int>& primes) {
	// Init
	int size = primes.size();
	primes[0] = 0;
	primes[1] = 0; // 0 & 1 are not primes

	for (size_t i = 2; i < size; i++) {
		// We can encounter values of i that have already been marked as not prime
		if (!primes[i]) {
			continue;
		}

		// p = i * i because we start from the square of i (see Khan Academy video)
		// http://stackoverflow.com/questions/5811151/why-do-we-check-up-to-the-square-root-of-a-prime-number-to-determine-if-it-is-pr
		for (size_t p = i * i; p < size; p += i) {
			primes[p] = false;
		}
	}
}

int getStartIndex(const int& p, const int& m) {
	int result;
	if (m > p) {
		// Get the first number divisible by p where p <= m.
		int firstNumberDivisibleByPBeforeM = (m / p) * p;

		if (firstNumberDivisibleByPBeforeM == m) {
			// If the number is equal to m, we can start the Sieve from m, e.g. Sieve[m] == false;
			result = firstNumberDivisibleByPBeforeM - m;
		}
		else {
			// Otherwise we add p to that number (so that it is >= m)
			// We subtract m to map to the range of m to n in the Sieve array
			// e.g. when p = 2, m = 99, firstNumberDivisibleByPBeforeM == 98.
			// Therefore, result = 98 + 2 - 99 == 1, whereby 
			// Sieve[1] represents the value 100 in the range from m to n.
			result = firstNumberDivisibleByPBeforeM + p - m;	
		}
	}	
	else {
		// We can just start sieve from Sieve[2p - m].
		// For all cases where p >= m, the first time we process p, p will be a prime number.
		// E.g.: p == 5, m == 4, then 5 is a prime.
		// Likewise, p == 71, m == any number less than p (i.e. 61), then p is a prime 
		// E.g. 2: p == 61, m == 61, then 61 is a prime. 
		// So, we can start marking Sieve[(p * 2) - m] is NOT A PRIME.
		result = (p * 2) - m;
	}

	return result;	
}

// Eratosthenes segmentation.
void getPrimesForRange(const vector<int>& primes, const int& m, const int& n) {	
	int size = n - m + 1;
	vector<int> sieve(size, true);		

	if (m == 1) {	// 1 is not a prime
		sieve[0] = 0;
	}

	// Get primes starting from the smallest value divisible by p within range (where p <= m ) up until p == n
	for (size_t i = 2; i < primes.size(); i++) {
		if (primes[i]) {
			int index = getStartIndex(i, m);
			if (index > size) { break; }

			// Perform Sieve of Eratosthenes
			for (size_t p = index; p < size; p += i) {
				sieve[p] = false;
			}
		}		
	}

	// print result
	for (size_t i = 0; i < size; i++) {
		if (sieve[i]) {
			cout << m + i << endl;
		}
	}
}

int main() {
	int t;
	cin >> t;

	int m, n;
	const int size = 31623;	// sqrt(1000000000) + 1	(see video of example for n = 100. We still need to process for i == 11).
	vector<int> primes(size, true);
	markAllPrimesInArray(primes);

	for (size_t p = 0; p < t; p++) {
		cin >> m >> n;	
		getPrimesForRange(primes, m, n);
	}
}