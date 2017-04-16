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
	for (size_t i = 2; i < size; i++) {
		if (!primes[i]) {
			continue;
		}

		for (size_t p = i * i; p < size; p += i) {
			primes[p] = false;
		}
	}
}

// Eratosthenes segmentation.
void getPrimes(const vector<int>& primes, const int& m, const int& n) {
	int start = m / 2 * 2;
	int size = n - start + 1;
	vector<int> sieve(size, true);
	vector<int> sieveMap;

	//// push smallest value before m that is di]visible by 2 to n
	//for (size_t i = start; i < size; i++) {
	//	sieveMap.push_back(i);
	//}

	// Get primes starting from the smallest value divisible by 2 (from <= m ) until n
	// i*i is because we start from the square of i (see Khan Academy video)
	for (size_t i = 2; i < primes.size; i++) {
		if (primes[i]) {
			for (size_t p = i; p < size; p += i) {	// p = i because we are not starting from integer 2
				sieve[p] = false;
			}
		}
		
	}

	// print result
	for (size_t i = m - start; i < size; i++) {
		if (sieve[i])
			cout << start + i << endl;
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
		getPrimes(primes, m, n);
	}
}