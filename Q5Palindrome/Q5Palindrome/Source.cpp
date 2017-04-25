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
using std::stringstream;
using std::ceil;
using std::to_string;

bool AllNines(const string& expr, const int& size) {
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

//int padZeroesAndReturnPaddingSize(string expr, vector<char>& arr) {
//	arr.clear();
//	int size = expr.length();
//	int paddingSize = MAX_SIZE - size;
//
//	for (size_t i = 0; i < paddingSize; i++) {
//		arr.push_back('0');
//	}
//
//	for (size_t i = 0; i < size; i++) {
//		arr.push_back(expr[i]);
//	}
//
//	return paddingSize;
//}

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
		if (AllNines(expr, size)) {			
			expr = "1";
			for (size_t i = 0; i < size - 1; i++) {
				expr += "0";
			}
			expr += "1";
		} else {
			stringstream convert(expr);
			int number;
			convert >> number;
			number++;

			expr = to_string(number);
			size = expr.length();
			int mid = getMid(expr, size);

			bool isEven = size % 2 == 0;
			getNextPalindrome(expr, size, mid, isEven);
		}
		printResult(expr);
	}
}