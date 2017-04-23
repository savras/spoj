#include <iostream>
#include <string>
#include <cmath>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::ceil;

string getNextPalindrome(string& expr) {
	int left, mid;
	int size;
	const int offset = 1;	

	size = expr.length();
	mid = ceil(size / 2) - offset;	// Always pointing to mid - 1 regardless if expr has odd or even length.
	left = mid;

	bool isAlreadyPalindrome = false;	// Handles the case where this algorithm doesn't work if the number given is already a palindrome.

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
	return expr;
}

int main() {
	int t;
	cin >> t;
	string expr;	
	while (--t >= 0) {
		cin >> expr;
		cout << getNextPalindrome(expr) << endl;
	}
}