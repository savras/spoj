#include <iostream>
#include <string>
#include <cmath>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::ceil;

int main() {
	int t;
	cin >> t;

	string expr;
	int left, mid;
	int size;
	const int offset = 1;

	while (--t >= 0) {
		cin >> expr;
		size = expr.length();

		mid = ceil(size / 2) - offset;	// Always pointing to mid - 1 regardless if expr has odd or even length.
		left = mid;

		while (left >= 0) {
			if (expr[left] != expr[size - offset - left]) {
				left = mid;
				while (expr[left] == '9') {
					left--;
				}
				expr[left]++;

				// Set all digits between left to mid == 0
				for (size_t i = left + 1; i <= mid; i++) {
					expr[i] = '0';
				}

				// Set everything to the right side of mid to become a palindrome.
				for (size_t i = 0; i <= mid; i++) {
					expr[size - offset - i] = expr[i];
				}
			}

			left--;
		}
		cout << expr << endl;;
	}
}