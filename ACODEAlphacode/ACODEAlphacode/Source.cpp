#include <iostream>
#include <string>
#include <vector>

using std::cin;
using std::cout;
using std::endl;
using std::string;
using std::vector;

void main() {
	string input;

	cin >> input;
	while (input != "0") {
		int size = input.length();
		vector<long long> arr(size, 0);
		arr[0] = 1;
		string numCode = "";

		for (size_t i = 1; i < size; i++) {
			numCode = "";
			numCode.push_back(input[i - 1]);
			numCode.push_back(input[i]);
			
			int value = stoi(numCode);			
			if (input[i] == '0' || input[i - 1] == '0' || ((i < size - 1) && input[i + 1] == '0')) {
				arr[i] = arr[i - 1];
			}
			else if (value > 26) {
				arr[i] = arr[i - 1];
			}
			else {
				int previous = i - 1;
				int prevPrevious = i - 2;

				if (prevPrevious >= 0)
				{
					arr[i] = arr[i - 1] + arr[i - 2];
				}
				else {
					arr[i] = arr[i - 1] + 1;
				}
			}
		}
		cout << arr[size - 1] << endl;

		arr.clear();
		cin >> input;
	} 
}