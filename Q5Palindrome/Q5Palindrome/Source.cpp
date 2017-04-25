#include <iostream>
#include <string>
#include <vector>
#include <cmath>
#define MAX_SIZE 7

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::vector;
using std::stoi;
using std::ceil;
using std::to_string;

int getMid(int paddingSize, const int exprSize) {
	// If size is odd, mid is the central value	
	int mid = exprSize / 2;
	if (exprSize % 2 == 0) {
		mid--;
	}

	return mid + paddingSize;
}

int padZeroesAndReturnPaddingSize(string expr, vector<char>& arr) {
	const int maxLength = 7;
	arr.clear();
	int size = expr.length();
	int paddingSize = maxLength - size;

	for (size_t i = 0; i < paddingSize; i++) {
		arr.push_back('0');
	}

	for (size_t i = 0; i < size; i++) {
		arr.push_back(expr[i]);
	}

	return paddingSize;
}

void printResult(const vector<char>& expr) {
	int startIndex = 0;
	while (expr[startIndex] == '0') {
		startIndex++;
	}

	for (size_t i = startIndex; i < MAX_SIZE; i++) {
		cout << expr[i];
	}
	cout << endl;
}

void getNextPalindrome(vector<char>& expr, const int& mid, const bool& isEven) {	
	const int offset = 1;		

	// If is odd. left == right == central value.
	int left = mid;
	int right = left;
	if (isEven) {
		right++;
	}	

	while (right < MAX_SIZE) {
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
	while(right < MAX_SIZE) {
		expr[right] = expr[left];
		left--;
		right++;
	}
}

int main() {
	int t;
	cin >> t;

	vector<char> zeroPaddedExpression;	// Max digits is 1000000

	string expr;
	while (--t >= 0) {
		cin >> expr;
		int number = stoi(expr);
		
		number++;
		expr = to_string(number);
		int sizeBeforePadding = expr.length();

		int paddingSize = padZeroesAndReturnPaddingSize(expr, zeroPaddedExpression);
		int mid = getMid(paddingSize, sizeBeforePadding);

		bool isEven = sizeBeforePadding % 2 == 0;
		getNextPalindrome(zeroPaddedExpression, mid, isEven);
		printResult(zeroPaddedExpression);
	}
}