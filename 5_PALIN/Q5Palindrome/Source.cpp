#include <iostream>
#include <string>
#include <vector>
#include <cmath>
#include <sstream>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::vector;
using std::ceil;
using std::to_string;

/* Utility functions */
void AddOne(string& expr, const int& size) {
	const int offset = 1;
	int startIndex = size - offset;

	while (expr[startIndex] == '9') {
		startIndex--;
	}
	expr[startIndex]++;

	// Set all digits between left to startIndex == 0
	for (size_t i = startIndex + 1; i <= startIndex; i++) {
		expr[i] = '0';
	}
}

bool allNines(const string& expr, const int& size) {	
	bool result = true;
	for (size_t i = 0; i < size; i++) {
		if (expr[i] != '9') {
			result = false;
			break;
		}
	}
	return result;
}

int getMid(const string& expr, const int& size) {
	// If size is odd, mid is the central value	
	int mid = size / 2;
	if (size % 2 == 0) {
		mid--;
	}

	return mid;
}

void printResult(const string& expr) {int startIndex = 0;
	while (expr[startIndex] == '0') {
		startIndex++;
	}

	int size = expr.size();
	for (size_t i = startIndex; i < size; i++) {
		cout << expr[i];
	}
	cout << endl;
}

/* Solution */
void getNextPalindrome(string& expr, const int& size, const int& mid, const bool& isEven) {	
	const int offset = 1;		

	// If is odd. left == right == central value.
	int left, right;
	left = right = mid;
	if (isEven) {
		right++;
	}

	while (right < size) {
		if (expr[left] < expr[right]) {
			left = mid;

			while (expr[left] == '9') {
				left--;
			}
			expr[left]++;

			// Set all digits between left to mid == 0
			for (size_t i = left + 1; i <= mid; i++) {
				expr[i] = '0';
			}
			break;
		}
		left--;
		right++;
	}

	// Duplicate right side of the expression making it a palindrome
	left = right = mid;
	if (isEven) {
		right++;
	}
	while(right < size) {
		expr[right] = expr[left];
		left--;
		right++;
	}
}

int main() {
	int t;
	cin >> t;

	string expr;
	while (--t >= 0) {
		cin >> expr;
		
		int size = expr.length();
		if (allNines(expr, size)) {			
			cout << "1";
			for (size_t i = 0; i < size - 1; i++) {
				cout << "0";
			}
			cout << "1" << endl;
			continue;
		} else {
			AddOne(expr, expr.length());
			size = expr.length();
			int mid = getMid(expr, size);

			bool isEven = size % 2 == 0;
			getNextPalindrome(expr, size, mid, isEven);
		}
		printResult(expr);
	}
}