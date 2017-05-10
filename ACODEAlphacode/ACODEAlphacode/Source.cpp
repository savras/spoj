#include <iostream>
#include <string>
#include <vector>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::vector;

void main() {
	int hasInput;
	cin >> hasInput;

	string code;	
	while (hasInput) {
		cin >> code;
		int size = code.length();
		vector<char> arr(size, 0);
		arr[0] = 1;
		string numCode = "";

		// ToDo:: Validate that code has no 0's;		
		for (size_t i = 1; i < size; i++) {
			numCode = "";
			numCode.push_back(code[i - 1]);
			numCode.push_back(code[i]);

			int value = stoi(numCode);
			if (value > 26) {
				arr[i] = arr[i - 1];
			}
			else {
				arr[i] = arr[i - 1] + arr[i - 2];
			}
		}
	}
}