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
void buildPrimes(vector<int>& primes) {
	// Init
	int size = primes.size();
	primes[0] = 0;
	primes[1] = 0; // 0 & 1 are not primes

	for (size_t i = 2; i < size; i++) {
		if (!primes[i]) {
			continue;
		}

		// i * i is because we start from the square of i (see Khan Academy video)
		// http://stackoverflow.com/questions/5811151/why-do-we-check-up-to-the-square-root-of-a-prime-number-to-determine-if-it-is-pr
		for (size_t p = i * i; p < size; p += i) {
			primes[p] = false;
		}
	}
}

int getStartIndex(const int& p, const int& m) {
	int result;
	if (m > p) {
		int firstDivisibleNumber = (m / p) * p;		

		if (firstDivisibleNumber == m) {
			result = firstDivisibleNumber - m;
		}
		else {
			result = firstDivisibleNumber + p - m;	// -m to map start of value, m, to index 0 of sieve array				
		}
	}	
	else {
		result = (p * 2) - m;	// start from the next p index after p
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

	// Get primes starting from the smallest value divisible by p (from <= m ) until == n	
	for (size_t i = 2; i < primes.size(); i++) {
		if (primes[i]) {
			int index = getStartIndex(i, m);
			if (index > size) { break; }
			for (size_t p = index; p < size; p += i) {	// p = i because we are not starting from integer 2
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
	buildPrimes(primes);

	for (size_t p = 0; p < t; p++) {
		cin >> m >> n;	
		getPrimesForRange(primes, m, n);
	}
}