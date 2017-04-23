#include <iostream>
#include <string>
#include <cmath>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::stoi;
using std::ceil;
using std::to_string;

string getNextPalindrome(string& expr) {
	int left, mid;
	int size;
	const int offset = 1;

	size = expr.length();
	mid = size / 2;
	if (size % 2 == 0) {
		mid--;
	}

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

			break;
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
		int number = stoi(expr);

		string nextPalindrome;
		nextPalindrome = getNextPalindrome(expr);

		int result = stoi(nextPalindrome);
		if (number == result) {	// This algorithm doesn't handle inputs that are already a palindrome.
			number++;
			nextPalindrome = getNextPalindrome(to_string(number));
		}
		cout << nextPalindrome << endl;
	}
}