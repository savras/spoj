#include <iostream>
#include <string>
#include <vector>
#include <cmath>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::vector;
using std::stoi;
using std::ceil;
using std::to_string;

int getMid(int paddingSize, const int exprSize) {
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

bool isInputSameAsResult(string expr, const vector<char>& nextPalindrome) {
	bool result = true;
	const int maxLength = 7;

	for (size_t i = 0; i < maxLength; i++) {
		if (expr[i] != nextPalindrome[i]) {
			result = false;
			break;
		}
	}

	return result;
}

void getNextPalindrome(vector<char>& expr, const int mid) {	
	const int offset = 1;		
	const int size = 7;
	int left = mid;
	int right = left + 1;

	while (right < size) {
		if (expr[left] != expr[right]) {
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
			bool isEven = ((size - mid - offset) * 2) % 2 == 0;
			left = mid;
			right = mid + 1;
			if (!isEven) {
				right++;
				left--;
			}

			while (right < size) {
				expr[right] = expr[left];
				left--;
				right++;
			}

			break;
		}
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
		int paddingSize = padZeroesAndReturnPaddingSize(expr, zeroPaddedExpression);
		int mid = getMid(paddingSize, expr.length());

		getNextPalindrome(zeroPaddedExpression, mid);

		if (isInputSameAsResult(expr, zeroPaddedExpression)) {	// This algorithm doesn't handle inputs that are already a palindrome.
			int number = stoi(expr);
			number++;
			expr = to_string(number);
			paddingSize = padZeroesAndReturnPaddingSize(expr, zeroPaddedExpression);
			mid = getMid(paddingSize, expr.length());

			getNextPalindrome(zeroPaddedExpression, mid);
		}

		for (size_t i = paddingSize; i < zeroPaddedExpression.size(); i++) {
			cout << zeroPaddedExpression[i];
		}
		cout << endl;
	}
}