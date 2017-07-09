#include <iostream>
#include <vector>
#include <algorithm>
#include <string>

using std::vector;
using std::endl;
using std::cin;
using std::cout;
using std::max;
using std::to_string;

int main() {

	int t;
	cin >> t;

	for (size_t i = 0; i < t; i++)
	{
		int n;
		cin >> n;

		vector<int> coins;;

		int value;
		for (size_t j = 0; j < n; j++)
		{
			cin >> value;
			coins.push_back(value);
		}

		if (n == 0)
		{
			cout << "Case " + to_string((i + 1)) + ": 0" << endl;
			continue;
		}

		if (n == 1)
		{
			cout << "Case " + to_string((i + 1)) + ": " + to_string(coins[0]) << endl;
			continue;
		}

		// Solve
		vector<long long> dpArr;
		for (size_t j = 0; j < n; j++)
		{
			long long iMinusTwo = j > 1 ? dpArr[j - 2] : 0;
			long long iMinusThree = j > 2 ? dpArr[j - 3] : 0;

			long long maxValue = max(iMinusTwo, iMinusThree);
			dpArr.push_back(coins[j] + maxValue);
		}

		int arrayOffset = 1;
		cout << "Case " + to_string((i + 1)) + ": " + to_string(max(dpArr[n - arrayOffset], dpArr[n - arrayOffset - 1])) << endl;
	}
}
